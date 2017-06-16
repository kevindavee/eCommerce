using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using eCommerce.DAL;
using eCommerce.DAL.Repositories.Stocks;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans;
using eCommerce.DAL.Repositories.Transactions.ShippingDetailss;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eCommerce.Logic.Services
{
    public class TransactionService
    {
        private CommerceContext context;
        private KonfirmasiPembayaranRepo konfirmasiPembayaranRepo;
        private TransactionHeaderRepo transactionHeaderRepo;
        private TransactionDetailsRepo transactionDetailRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ShippingDetailsRepo shippingDetailsRepo;
        private StockRepo stockRepo;

        public TransactionService(CommerceContext _context, TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailRepo, 
                                  ProductInstanceRepo _productInstanceRepo, ShippingDetailsRepo _shippingDetailsRepo, KonfirmasiPembayaranRepo _konfirmasiPembayaranRepo,
                                  StockRepo _stockRepo)
        {
            context = _context;
            transactionHeaderRepo = _transactionHeaderRepo;
            transactionDetailRepo = _transactionDetailRepo;
            productInstanceRepo = _productInstanceRepo;
            shippingDetailsRepo = _shippingDetailsRepo;
            konfirmasiPembayaranRepo = _konfirmasiPembayaranRepo;
            stockRepo = _stockRepo;
        }

        /// <summary>
        /// Create new transaction
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ProductInstanceId"></param>
        /// <returns></returns>
        public string CreateTransaction(long CustomerId, long ProductInstanceId, int Quantity, string Username)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Update stock
                    var stockResult = stockRepo.MoveStockToCart(ProductInstanceId, Quantity);
                    if (!stockResult)
                    {
                        contextTransaction.Rollback();
                        return FunctionResult.OutOfStock;
                    }

                    //Crete new header object
                    TransactionHeader transactionHeader = new TransactionHeader();
                    transactionHeader.CreatedBy = transactionHeader.UpdatedBy = Username;
                    transactionHeader.Code = transactionHeaderRepo.GenerateTransactionCode();
                    transactionHeader.CustomerId = CustomerId;
                    transactionHeader.LastStatus = transactionHeader.CurrentStatus = TransactionStatus.OnCart;

                    transactionHeaderRepo.Save(transactionHeader);

                    //Create new detail object
                    TransactionDetails transactionDetail = new TransactionDetails();
                    transactionDetail.TransactionHeaderId = transactionHeader.Id;
                    transactionDetail.CreatedBy = transactionDetail.UpdatedBy= Username;
                    transactionDetail.ProductInstanceId = ProductInstanceId;
                    transactionDetail.Price = productInstanceRepo.GetById(ProductInstanceId).Price;
                    transactionDetail.Quantity = Quantity;

                    transactionDetailRepo.Save(transactionDetail);

                    //Update header total price
                    UpdateTotalPrice(transactionHeader.Id, Username);

                    contextTransaction.Commit();
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                    return FunctionResult.Error;
                }
            }
            return FunctionResult.Success;
        }

        /// <summary>
        /// Add item to active cart
        /// </summary>
        /// <param name="transactionHeader"></param>
        /// <param name="ProductInstanceId"></param>
        /// <returns></returns>
        public string AddItemToCart(TransactionHeader transactionHeader, long ProductInstanceId, int Quantity, string Username)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                var stockResult = stockRepo.MoveStockToCart(ProductInstanceId, Quantity);
                if (!stockResult)
                {
                    contextTransaction.Rollback();
                    return FunctionResult.OutOfStock;
                }

                TransactionDetails transactionDetail = new TransactionDetails();

                try
                {
                    //Check apakah ada item yang sama di dalam cart
                    var existedDetailItem = CheckExistingItemInCart(transactionHeader.Id, ProductInstanceId);
                    
                    //tidak ada item yang sama, buat object baru
                    if (existedDetailItem == null)
                    {
                        transactionDetail.TransactionHeaderId = transactionHeader.Id;
                        transactionDetail.CreatedBy = transactionDetail.UpdatedBy = Username;
                        transactionDetail.ProductInstanceId = ProductInstanceId;
                        transactionDetail.Price = productInstanceRepo.GetById(ProductInstanceId).Price;
                        transactionDetail.Quantity = Quantity;

                    }
                    //Ada item yang sama. Tambah Quantity nya
                    else
                    {
                        transactionDetail = existedDetailItem;

                        transactionDetail.UpdatedBy = Username;
                        transactionDetail.UpdatedDate = DateTime.Today;
                        transactionDetail.Quantity += Quantity;
                    }

                    transactionDetailRepo.Save(transactionDetail);

                    transactionHeader.UpdatedBy = Username;
                    transactionHeader.UpdatedDate = DateTime.Today;
                    transactionHeader.TotalPrice = transactionDetailRepo.CalculateTotalPrice(transactionHeader.Id);

                    transactionHeaderRepo.Save(transactionHeader);

                    contextTransaction.Commit();
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                    return FunctionResult.Error;
                }
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// Update quantity of an item
        /// </summary>
        /// <param name="Quantity"></param>
        /// <param name="TransactionDetailId"></param>
        /// <returns></returns>
        public string UpdateQuantity(int Quantity, long TransactionDetailId, string Username)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                int diffrence = 0;
                try
                {
                    var item = transactionDetailRepo.GetById(TransactionDetailId);
                    diffrence = Quantity - item.Quantity;

                    var stockResult = stockRepo.MoveStockToCart(item.ProductInstanceId, diffrence);
                    if (!stockResult)
                    {
                        contextTransaction.Rollback();
                        return FunctionResult.OutOfStock;
                    }


                    if (item != null)
                    {
                        var oldQuantity = item.Quantity;
                        item.Quantity = Quantity;
                        item.UpdatedBy = Username;
                        item.UpdatedDate = DateTime.Today;

                        transactionDetailRepo.Save(item);
                        UpdateTotalPrice(item.TransactionHeaderId, Username);
                    }

                    

                    contextTransaction.Commit();
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                    return FunctionResult.Error;
                }
            }
            return FunctionResult.Success;
        }

        /// <summary>
        /// Delete item from cart
        /// </summary>
        /// <param name="TransactionDetailId"></param>
        /// <param name="TransactionHeaderId"></param>
        /// <returns></returns>
        public bool DeleteItem(long TransactionDetailId, long TransactionHeaderId, string Username)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var detail = transactionDetailRepo.GetById(TransactionDetailId);
                    var result = stockRepo.MoveStockToCart(detail.ProductInstanceId, -detail.Quantity);

                    transactionDetailRepo.Delete(TransactionDetailId);

                    UpdateTotalPrice(TransactionHeaderId, Username);
                    contextTransaction.Commit();

                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Confirm transaction. After saved, this transaction is waiting for it's payment confirmation
        /// </summary>
        /// <param name="transactionHeaderId"></param>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        public bool ConfirmTransaction(long transactionHeaderId, ShippingDetails shippingDetails, string Username)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    shippingDetailsRepo.Save(shippingDetails);

                    //Ganti customer dengan username
                    transactionHeaderRepo.ChangeStatus(transactionHeaderId, TransactionStatus.PaymentConfirmation, Username);

                    contextTransaction.Commit();
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Proccess Confirmation. If payment confirmation is valid and the amount transfered has match the total price of the transaction, transaction will be flag as process and stock will be subtract.
        /// If confirmation is rejected, this method only flag the payment confirmation that it has been validated
        /// </summary>
        /// <returns></returns>
        public bool ProcessConfirmation(long TransactionHeaderId, string Remarks, string Username, long KonfirmasiPembayaranId, bool Validation)
        {
            using (var contextTrans = context.Database.BeginTransaction())
            {
                try
                {
                    //Validate payment confirmation
                    konfirmasiPembayaranRepo.Validate(KonfirmasiPembayaranId, Validation, Username);

                    //If admin approve the payment confirmation
                    if (Validation != false)
                    {
                        //Count transfered amount whether user had transfered requried payment amount
                        var validTransfer = CheckTransferAmount(TransactionHeaderId);

                        //If user transfered enough amount of payment. Flag transaction to process, flag shipping to processed ,and subtract stock
                        if (validTransfer)
                        {
                            transactionHeaderRepo.ChangeStatus(TransactionHeaderId, TransactionStatus.ProcessTransaction, Username);
                            shippingDetailsRepo.ProcessOrder(TransactionHeaderId, Username);

                            var detail = transactionDetailRepo.GetByHeaderId(TransactionHeaderId);

                            foreach (var item in detail)
                            {
                                stockRepo.SoldItem(item.ProductInstanceId, item.Quantity);
                            }
                        }

                    }

                    contextTrans.Commit();
                }
                catch (Exception)
                {
                    contextTrans.Rollback();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Recalculate Total Price and save it
        /// </summary>
        /// <param name="TransactionHeaderId"></param>
        private void UpdateTotalPrice(long TransactionHeaderId, string Username)
        {
            var totalPrice = transactionDetailRepo.CalculateTotalPrice(TransactionHeaderId);
            var header = transactionHeaderRepo.GetById(TransactionHeaderId);
            header.TotalPrice = totalPrice;
            //TODO: Ganti dengan username
            header.UpdatedBy = Username;
            header.UpdatedDate = DateTime.Today;

            transactionHeaderRepo.Save(header);
        }

        private TransactionDetails CheckExistingItemInCart(long TransactionHeaderId, long ProductInstanceId)
        {
            var transactionItems = transactionDetailRepo.GetByHeaderId(TransactionHeaderId);
            if (transactionItems != null)
            {
                foreach (var item in transactionItems)
                {
                    if (item.ProductInstanceId == ProductInstanceId)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        private bool CheckTransferAmount(long TransactionHeaderId)
        {
            var totalPrice = transactionHeaderRepo.GetById(TransactionHeaderId).TotalPrice;
            var transferedAmount = konfirmasiPembayaranRepo.GetAllTransferedAmountByHeaderId(TransactionHeaderId);

            
            if (transferedAmount < totalPrice)
            {
                return false;
            }
            

            return true;
        }
    }
}

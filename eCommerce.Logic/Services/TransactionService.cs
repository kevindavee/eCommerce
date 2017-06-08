using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using eCommerce.DAL;
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

        public TransactionService(CommerceContext _context, TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailRepo, 
                                  ProductInstanceRepo _productInstanceRepo, ShippingDetailsRepo _shippingDetailsRepo, KonfirmasiPembayaranRepo _konfirmasiPembayaranRepo)
        {
            context = _context;
            transactionHeaderRepo = _transactionHeaderRepo;
            transactionDetailRepo = _transactionDetailRepo;
            productInstanceRepo = _productInstanceRepo;
            shippingDetailsRepo = _shippingDetailsRepo;
            konfirmasiPembayaranRepo = _konfirmasiPembayaranRepo;
        }

        /// <summary>
        /// Create new transaction
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ProductInstanceId"></param>
        /// <returns></returns>
        public bool CreateTransaction(long CustomerId, long ProductInstanceId, int Quantity)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    TransactionHeader transactionHeader = new TransactionHeader();
                    //TODO: Ganti Customer dengan username
                    transactionHeader.CreatedBy = transactionHeader.UpdatedBy = "Customer";
                    transactionHeader.Code = transactionHeaderRepo.GenerateTransactionCode();
                    transactionHeader.CustomerId = CustomerId;
                    transactionHeader.LastStatus = transactionHeader.CurrentStatus = TransactionStatus.OnCart;

                    TransactionDetails transactionDetail = new TransactionDetails();
                    //TODO: Ganti customer dengan username
                    transactionDetail.CreatedBy = transactionDetail.UpdatedBy=  "Customer";
                    transactionDetail.ProductInstanceId = ProductInstanceId;
                    transactionDetail.Price = productInstanceRepo.GetById(ProductInstanceId).Price;
                    transactionDetail.Quantity = Quantity;

                    transactionHeaderRepo.Save(transactionHeader);
                    transactionDetailRepo.Save(transactionDetail);

                    transactionHeader.TotalPrice = transactionDetailRepo.CalculateTotalPrice(transactionHeader.Id);

                    transactionHeaderRepo.Save(transactionHeader);

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
        /// Add item to active cart
        /// </summary>
        /// <param name="transactionHeader"></param>
        /// <param name="ProductInstanceId"></param>
        /// <returns></returns>
        public bool AddItemToCart(TransactionHeader transactionHeader, long ProductInstanceId, int Quantity)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                TransactionDetails transactionDetail = new TransactionDetails();

                try
                {
                    //Check apakah ada item yang sama di dalam cart
                    var existedDetailItem = CheckExistingItemInCart(transactionHeader.Id, ProductInstanceId);
                    
                    //tidak ada item yang sama, buat object baru
                    if (existedDetailItem != null)
                    {
                        transactionDetail.CreatedBy = transactionDetail.UpdatedBy = "Customer";
                        transactionDetail.ProductInstanceId = ProductInstanceId;
                        transactionDetail.Price = productInstanceRepo.GetById(ProductInstanceId).Price;
                        transactionDetail.Quantity = Quantity;

                    }
                    //Ada item yang sama. Tambah Quantity nya
                    else
                    {
                        //TODO: Ganti customer dengan username
                        transactionDetail.UpdatedBy = "Customer";
                        transactionDetail.UpdatedDate = DateTime.Today;
                        transactionDetail = existedDetailItem;
                        transactionDetail.Quantity += Quantity;
                    }

                    transactionDetailRepo.Save(transactionDetail);

                    //TODO: Ganti customer dengan username
                    transactionHeader.UpdatedBy = "Customer";
                    transactionHeader.UpdatedDate = DateTime.Today;
                    transactionHeader.TotalPrice = transactionDetailRepo.CalculateTotalPrice(transactionHeader.Id);

                    transactionHeaderRepo.Save(transactionHeader);

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
        /// Update quantity of an item
        /// </summary>
        /// <param name="Quantity"></param>
        /// <param name="TransactionDetailId"></param>
        /// <returns></returns>
        public bool UpdateQuantity(int Quantity, long TransactionDetailId)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var item = transactionDetailRepo.GetById(TransactionDetailId);

                    if (item != null)
                    {
                        var oldQuantity = item.Quantity;
                        item.Quantity = Quantity;
                        item.Price = (item.Price / oldQuantity) * Quantity;
                        //TODO: Ganti Customer dengan username
                        item.UpdatedBy = "Customer";
                        item.UpdatedDate = DateTime.Today;

                        transactionDetailRepo.Save(item);
                        UpdateTotalPrice(item.TransactionHeaderId);
                    }
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
        /// Delete item from cart
        /// </summary>
        /// <param name="TransactionDetailId"></param>
        /// <param name="TransactionHeaderId"></param>
        /// <returns></returns>
        public bool DeleteItem(long TransactionDetailId, long TransactionHeaderId)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    transactionDetailRepo.Delete(TransactionDetailId);

                    UpdateTotalPrice(TransactionHeaderId);
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
        public bool ConfirmTransaction(long transactionHeaderId, ShippingDetails shippingDetails)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    shippingDetailsRepo.Save(shippingDetails);

                    //Ganti customer dengan username
                    transactionHeaderRepo.ChangeStatus(transactionHeaderId, TransactionStatus.PaymentConfirmation, "Customer");

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
        /// Recalculate Total Price
        /// </summary>
        /// <param name="TransactionHeaderId"></param>
        private void UpdateTotalPrice(long TransactionHeaderId)
        {
            var totalPrice = transactionDetailRepo.CalculateTotalPrice(TransactionHeaderId);
            var header = transactionHeaderRepo.GetById(TransactionHeaderId);
            header.TotalPrice = totalPrice;
            //TODO: Ganti dengan username
            header.UpdatedBy = "Customer";
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
    }
}

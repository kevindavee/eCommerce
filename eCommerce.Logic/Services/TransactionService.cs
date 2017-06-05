﻿using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using eCommerce.DAL;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Logic.Services
{
    public class TransactionService
    {
        private CommerceContext context;
        private TransactionHeaderRepo transactionHeaderRepo;
        private TransactionDetailsRepo transactionDetailRepo;
        private ProductInstanceRepo productInstanceRepo;

        public TransactionService(CommerceContext _context, TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailRepo, 
                                  ProductInstanceRepo _productInstanceRepo)
        {
            context = _context;
            transactionHeaderRepo = _transactionHeaderRepo;
            transactionDetailRepo = _transactionDetailRepo;
            productInstanceRepo = _productInstanceRepo;
        }

        /// <summary>
        /// Create new transaction
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ProductInstanceId"></param>
        /// <returns></returns>
        public bool CreateTransaction(long CustomerId, long ProductInstanceId)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    TransactionHeader transactionHeader = new TransactionHeader();
                    transactionHeader.CreatedBy = transactionHeader.UpdatedBy = "Customer";
                    transactionHeader.Code = transactionHeaderRepo.GenerateTransactionCode();
                    transactionHeader.CustomerId = CustomerId;
                    transactionHeader.LastStatus = transactionHeader.CurrentStatus = TransactionStatus.OnCart;

                    TransactionDetails transactionDetail = new TransactionDetails();
                    transactionDetail.ProductInstanceId = ProductInstanceId;
                    transactionDetail.Price = productInstanceRepo.GetById(ProductInstanceId).Price;
                    transactionDetail.Quantity = 1;

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
        public bool AddItemToCart(TransactionHeader transactionHeader, long ProductInstanceId)
        {
            using (var contextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    TransactionDetails transactionDetail = new TransactionDetails();
                    transactionDetail.ProductInstanceId = ProductInstanceId;
                    transactionDetail.Price = productInstanceRepo.GetById(ProductInstanceId).Price;
                    transactionDetail.Quantity = 1;

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
    }
}

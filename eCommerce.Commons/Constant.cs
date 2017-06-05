using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Commons
{
    public static class UserRoles
    {
        public const string ProductAdmin = "ProductAdmin";
        public const string SuperAdmin = "SuperAdmin";
        public const string FinanceAdmin = "FinanceAdmin";
        public const string Customer = "Customer";
    }

    public static class TransactionStatus
    {
        public const string OnCart = "On Cart";
        public const string CheckedOut = "Checked Out";
        public const string PaymentConfirmation = "Payment Confirmation";
        public const string Expired = "Expired";
        public const string Canceled = "Canceled";
        public const string OrderProcessed = "Order Processed";
        public const string Shipped = "Order shipped";
        public const string Finished = "Finished";
    }

}

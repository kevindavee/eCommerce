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
        public const string ProcessTransaction = "Process Transaction";
        public const string Finished = "Finished";
        public const string Expired = "Expired";
    }

    public static class ShippingStatus
    {
        public const string OrderProcessed = "Order Processed";
        public const string Shipped = "Order shipped";
    }

    public static class FunctionResult
    {
        public const string Success = "Success";
        public const string Error = "Error";
        public const string OutOfStock = "Out of Stock";
    }

}

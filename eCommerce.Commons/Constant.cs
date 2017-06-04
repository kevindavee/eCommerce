using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Commons
{
    public class Constant
    {
    }
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
        public const string PaymentConfirmation = "Payment Confirmation";
        public const string Shipping = "Shipping";
        public const string Finished = "Finished";
    }

}

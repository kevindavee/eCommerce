using eCommerce.Core.CommerceClasses.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Stocks
{
    public class StockRepo : RepoBase<Stock>
    {
        public StockRepo(CommerceContext _context) : base(_context)
        {
        }

        /// <summary>
        /// Toggle quantity movement between cart and stock
        /// </summary>
        /// <param name="ProductInstanceId"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public bool MoveStockToCart(long ProductInstanceId, int Quantity)
        {
            var stock = dbSet.Where(s => s.ProductInstanceId == ProductInstanceId).SingleOrDefault();
            stock.Quantity = stock.Quantity - Quantity;
            if (stock.Quantity < 0)
            {
                return false;
            }
            stock.OnCart = stock.OnCart + Quantity;
            Save(stock);

            return true;
        }

        /// <summary>
        /// Cut quantity from cart for sold items, and save
        /// </summary>
        /// <param name="ProductInstanceId"></param>
        /// <param name="Quantity"></param>
        public void SoldItem(long ProductInstanceId, int Quantity)
        {
            var stock = dbSet.Where(s => s.ProductInstanceId == ProductInstanceId).FirstOrDefault();
            stock.OnCart = stock.OnCart - Quantity;
            Save(stock);
        }
    }
}

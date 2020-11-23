using OrderApplication.Domain.Abstract;
using OrderApplication.Domain.Entities;
using OrderProcessingApp.Domain.Abstract;
using OrderProcessingApp.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Domain.Context
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products => context.Products;

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            {
                if (dbEntry != null)
                {
                    context.Products.Remove(dbEntry);
                    context.SaveChanges();
                }
                return dbEntry;
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.Product_ID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.Product_ID);
                if (dbEntry != null)
                {
                    dbEntry.Product_Name = product.Product_Name;
                    dbEntry.Product_Price = product.Product_Price;
                    dbEntry.Product_Category = product.Product_Category;
                }
            }
            context.SaveChanges();
        }
    }
}

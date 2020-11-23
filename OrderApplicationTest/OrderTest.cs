using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderApplication.Domain.Entities;
namespace OrderApplicationTest
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void RunShouldNoShippingforInvalidItem()
        {
            Product product = new Product { Product_Category = "Services", Product_ID = 1, Product_Name = "Laptop", Product_Price = 300 };
            int Quantity = 1;
            Cart cart = new Cart();
            cart.AddItem(product, Quantity);
            ShippingDetails shippingDetails = new ShippingDetails { Name = "TestName", Email = "test@mail.com", Line1 = "line1", Line2 = "line2", City = "Delhi", Country = "India", State = "ND", Zip = "11003" };
            var packing = new PackingSlip();
            packing.GeneratePackingSlip(cart, shippingDetails, "Services", "No");
            string ActualValue = packing.packingSlips[0].OrderNumber.ToString();
            Assert.IsFalse(String.IsNullOrEmpty(ActualValue));

        }

        [TestMethod]
        public void RunShouldShippingforValidItem()
        {
            Product product = new Product { Product_Category = "Physical", Product_ID = 1, Product_Name = "Laptop", Product_Price = 300 };
            int Quantity = 1;
            Cart cart = new Cart();
            cart.AddItem(product, Quantity);
            ShippingDetails shippingDetails = new ShippingDetails { Name = "TestName", Email = "test@mail.com", Line1 = "line1", Line2 = "line2", City = "Delhi", Country = "India", State = "ND", Zip = "11003" };
            var packing = new PackingSlip();
            packing.GeneratePackingSlip(cart, shippingDetails, "Physical", "No");
            string ActualValue = packing.packingSlips[0].OrderNumber.ToString();
            Assert.IsFalse(String.IsNullOrEmpty(ActualValue));
        }

        [TestMethod]
        public void RunShouldDuplicateShippingforValidItem()
        {
            Product product = new Product { Product_Category = "Book", Product_ID = 1, Product_Name = "Laptop", Product_Price = 300 };
            int Quantity = 1;
            Cart cart = new Cart();
            cart.AddItem(product, Quantity);

            ShippingDetails shippingDetails = new ShippingDetails { Name = "TestName", Email = "test@mail.com", Line1 = "line1", Line2 = "line2", City = "Delhi", Country = "India", State = "ND", Zip = "11003" };

            //Rules rules = new Rules();
            var packing = new PackingSlip();
            packing.GeneratePackingSlip(cart, shippingDetails, "Book", "No");

            string ActualValue = packing.packingSlips_DuplicateforLoyalti[0].pupose.ToString();
            Assert.AreEqual("Loyalti Deparment", ActualValue);
        }

    }
}

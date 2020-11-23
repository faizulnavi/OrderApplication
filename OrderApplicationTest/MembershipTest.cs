using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderApplication.Domain.Context;
using OrderApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplicationTest
{
    [TestClass]
    public class MembershipTest
    {
        [TestMethod]
        public void RunShouldCreateMembershipforvalidItem()
        {
            var memberTest = new Membership(); ;
            memberTest.CreateMembership("Member_New");
            string ActualValue = memberTest.member[0].IsActive.ToString();
            Assert.AreEqual("Yes", ActualValue);
        }

        [TestMethod]
        public void RunShouldUpdateMembershipforvalidItem()
        {
            var memberTest = new Membership(); ;
            memberTest.CreateMembership("Member_Upgrade");
            string ActualValue = memberTest.member[0].MembershipType.ToString();
            Assert.AreEqual("Member_Upgrade", ActualValue);
        }

        [TestMethod]
        public void RunShouldEmailnotificationforvalidItem()
        {
            Product product = new Product { Product_Category = "Membership", Product_ID = 1, Product_Name = "Laptop", Product_Price = 300 };
            int Quantity = 1;
            Cart cart = new Cart();
            cart.AddItem(product, Quantity);

            ShippingDetails shippingDetails = new ShippingDetails { Name = "TestName", Email = "test@mail.com", Line1 = "line1", Line2 = "line2", City = "Delhi", Country = "India", State = "ND", Zip = "11003" };
            EmailSettings settings = new EmailSettings
            {
                MailFromAddress = "faiznavi@gmail.com",
                UseSsl = true,
                Username = "test@gmail.com",
                Password = "*******",
                ServerName = "smtp.gmail.com",
                ServerPort = 587
            };
            var emailOrder = new EmailOrderProcessor(settings);
            string Umsg = "Membership has been activated/upgraded";

            emailOrder.ProcessOrder(cart, shippingDetails, Umsg);
            string ActualValue = emailOrder.Fstatus.ToString();

            Assert.AreEqual("Successfull", ActualValue);

        }
    }
}

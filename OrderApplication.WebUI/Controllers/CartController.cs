using OrderApplication.Domain.Entities;
using OrderApplication.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderApplication.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        // GET: Cart
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(
            new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }


        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "No Item added!");
            }
            //Rules rule = new Rules();
            PackingSlip slip = new PackingSlip();
            Membership membership = new Membership();
            string cat = cart.Lines.Select(s => s.Product.Product_Category).FirstOrDefault();

            string strView = "";
            if (ModelState.IsValid)
            {
                if (cat == "Physical")
                {
                    slip.GeneratePackingSlip(cart, shippingDetails, cat, "No");
                    cart.Clear();
                    strView = "PhysicalView";
                }

                if (cat == "Book")
                {
                    slip.GeneratePackingSlip(cart, shippingDetails, cat, "No");
                    cart.Clear();
                    strView = "BookView";
                }

                if (cat == "Membership")
                {
                    string prod = cart.Lines.Select(s => s.Product.Product_Name).FirstOrDefault();
                    string strMsg = "";
                    if (prod == "Member_New")
                    {
                        membership.CreateMembership(prod);
                        strMsg = "Congratulation: Your Membership has been activated";
                        cart.Clear();
                    }
                    else
                    {
                        membership.UpdaradMembership(prod, 1);
                        strMsg = "Congratulation: Your Membership has been updraded";
                        cart.Clear();
                    }
                    orderProcessor.ProcessOrder(cart, shippingDetails, strMsg);
                    strView = "Completed";
                }



                return View(strView);
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
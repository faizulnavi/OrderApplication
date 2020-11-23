using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrderApplication.Domain.Entities
{
    public class PackingSlip
    {
        [Key]
        public int SlipID { get; set; }
        public string OrderNumber { get; set; }
        public string Product_Name { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string zip { get; set; }
        public string Country { get; set; }
        public string pupose { get; set; }
        public string gift { get; set; }

        public List<PackingSlip> packingSlips = new List<PackingSlip>();
        public List<PackingSlip> packingSlips_DuplicateforLoyalti = new List<PackingSlip>();
        AgentCommission agentcom = new AgentCommission();

        private static int _ordernumber = 0;
        private static int _slipID = 0;
        public static string GenerateOrdertNumber()
        {
            _ordernumber++;
            return "Order-" + _ordernumber.ToString();
        }

        public static int GenerateSlipID()
        {
            _slipID++;
            return _slipID;
        }


        public void GeneratePackingSlip(Cart cart, ShippingDetails shippingDetails, string cat, string gift)
        {
            string orderID = GenerateOrdertNumber();//Rules.GenerateOrdertNumber();
           
            string FreeGift = "";
            if (gift == "Yes")
            {
                FreeGift = "First Aid Video - Free gift";
            }
            else
            {
                FreeGift = "N/A";
            }
            try { 
            packingSlips.Add(
            new PackingSlip
             {
              SlipID = GenerateSlipID(),
              OrderNumber = orderID,
              Product_Name = cart.Lines.Select(s => s.Product.Product_Name).FirstOrDefault(),
              Quantity = cart.Lines.Select(s => s.Quantity).FirstOrDefault(),
              Address = shippingDetails.Line1 + shippingDetails.Line2 + shippingDetails,
              City = shippingDetails.City,
              State = shippingDetails.State,
              zip = shippingDetails.Zip,
              Country = shippingDetails.Country,
              pupose = "shipping",
              gift = FreeGift
             }
            );
                if (cat == "Book") // Adding duplicate receipt for Book
                {
                    packingSlips_DuplicateforLoyalti.Add(
                        new PackingSlip
                        {
                            SlipID = GenerateSlipID(),
                            OrderNumber = orderID, // keep the same order number to make a duplicate copy
                            Product_Name = cart.Lines.Select(s => s.Product.Product_Name).FirstOrDefault(),
                            Quantity = cart.Lines.Select(s => s.Quantity).FirstOrDefault(),
                            Address = shippingDetails.Line1 + shippingDetails.Line2 + shippingDetails,
                            City = shippingDetails.City,
                            State = shippingDetails.State,
                            zip = shippingDetails.Zip,
                            Country = shippingDetails.Country,
                            pupose = "Layalti Deparment"
                        }
                        );

                    if (cat == "Book" || cat == "Physical")
                    {
                        agentcom.Generate_AgentCommision(orderID);
                    }
                }
                
            }
            catch { }


        }
    }
}

﻿using OrderApplication.Domain.Entities;
using OrderApplication.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderApplication.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
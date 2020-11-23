using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderApplication.Domain.Abstract;
using OrderApplication.WebUI.Models;

namespace OrderApplication.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public int PageSize = 4;
        private readonly IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult ProducList(string category, int page = 1)
        {

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Product_Category == category)
                .OrderBy(p => p.Product_ID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //TotalItems = repository.Products.Count()
                    TotalItems = category == null ?
                                repository.Products.Count() :
                                repository.Products.Where(p => p.Product_Category == category).Count()
                },
                CurrentCategory = category

            };
            return View(model);

        }
    }
}
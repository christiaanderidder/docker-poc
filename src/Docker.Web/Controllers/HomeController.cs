using Docker.Core.Events;
using Docker.Data;
using Docker.Data.Repositories;
using Docker.Web.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();

            _publishEndpoint.Publish(new OfferUpdatedEvent() { Price = 10, Stock = 15 });

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

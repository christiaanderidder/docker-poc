using Dcoker.Web.Validation;
using Docker.Core.Events;
using Docker.Data.Entities;
using Docker.Data.Repositories;
using Docker.Web.Models;
using Docker.Web.Validation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductController(IProductRepository productRepository, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        [ImportModelState]
        public IActionResult Edit(int? id)
        {
            var product = id.HasValue ? _productRepository.GetById(id.Value) : new Product();

            return View(new ProductEditViewModel(product));
        }

        [HttpPost]
        [ExportModelState]
        public IActionResult Edit(int? id, ProductEditViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit), new { id });

            var product = id.HasValue ? _productRepository.GetById(id.Value) : new Product();
            if(product == null)
            {
                this.SetNotice($"Product #{id} was not found");
                return RedirectToAction(nameof(Index));
            }

            var offerChanged = (product.Stock != model.Stock || product.Price != model.Price);

            model.ApplyToEntity(product);

            if (_productRepository.Persist(product))
            {
                if(offerChanged) _publishEndpoint.Publish(new OfferUpdatedEvent() { Price = model.Price, Stock = model.Stock });

                this.SetNotice($"Saved product #{product.Id}");
                return RedirectToAction(nameof(Edit), new { id = id ?? product.Id });
            }
            else
            {
                this.SetNotice($"Failed to save product");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

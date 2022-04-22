using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Dtos;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        [Route("Products")]
        public IActionResult Index()
        {
            return View(_mapper.Map<List<ProductDto>>(_unitOfWork.Product.GetAll().ToList()));
        }

        [Route("Products/Add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Products/Add")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(_mapper.Map<Product>(product));
                _unitOfWork.Save();
                // changes for tempData starts here
                TempData["Message"] = "Product added succesfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
            //ends here
            
        }

        [Route("Products/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(_unitOfWork.Product.GetById(id.Value));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [Route("Products/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductDto product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Product.Update(_mapper.Map<Product>(product));
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Product.ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Route("Products/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(_unitOfWork.Product
                .GetById(id.Value));
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Products/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _unitOfWork.Product.GetById(id);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}

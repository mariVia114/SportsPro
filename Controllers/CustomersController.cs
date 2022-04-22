using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Dtos;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class CustomersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Route("Customers")]
        public IActionResult Index()
        {
            return View(_mapper.Map<List<CustomerDto>>(_unitOfWork.Customer.GetAll()));
        }

        [Route("Customers/Add")]
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_unitOfWork.Country.GetAll(), "CountryId", "Name");
            return View();
        }

        [HttpPost]
        [Route("Customers/Add")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerDto customer)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customer.Add(_mapper.Map<Customer>(customer));
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_unitOfWork.Country.GetAll(), "CountryId", "Name", customer.CountryId);
            return View(customer);
        }

        [Route("Customers/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _mapper.Map<CustomerDto>(_unitOfWork.Customer.GetById(id.Value));
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_unitOfWork.Country.GetAll(), "CountryId", "Name", customer.CountryId);
            return View(customer);
        }

        [HttpPost]
        [Route("Customers/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,CustomerDto customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Customer.Update(_mapper.Map<Customer>(customer));
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Customer.CustomerExists(customer.CustomerId))
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
            ViewData["CountryId"] = new SelectList(_unitOfWork.Country.GetAll(), "CountryId", "Name", customer.CountryId);
            return View(customer);
        }

        [Route("Customers/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _mapper.Map<CustomerDto>(_unitOfWork.Customer.GetById(id.Value));
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Customers/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _unitOfWork.Customer.GetById(id);
            _unitOfWork.Customer.Remove(customer);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
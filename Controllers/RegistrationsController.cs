using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;
using SportsPro.Models.ManageViewModels;
using SportsPro.Models.RegistrationViewModels;

namespace SportsPro.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistrationsController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = Role.ADMIN)]
        [Route("Registration")]
        public IActionResult Index()
        {
            return View(_unitOfWork.Registration.GetAll());
        }

        [Route("Registrations/customer")]
        public IActionResult Customers()
        {
            var model = new CustomerProductViewModel();
            var items = new List<SelectListItem>();
            var customers = _unitOfWork.Customer.GetAll();
            foreach (var customer in customers)
            {
                items.Add(new SelectListItem { Text = customer.FullName, Value = customer.CustomerId.ToString() });
            }
            model.Customers = items;
            return View("Index", model);
        }

        [Route("Registrations")]
        public ActionResult Registrations(int customerId)
        {
            if (customerId != 0)
            {
                var vm = new RegistrationViewModel();
                HttpContext.Session.SetString("CurrentCustomerId", customerId.ToString());
                var customer = _unitOfWork.Customer.GetById(customerId);
                ViewBag.CustomerName = customer?.FullName;
                vm.Registrations = _unitOfWork.Registration.GetAllByCustomer(customerId);
                var items = new List<SelectListItem>();
                var products = _unitOfWork.Product.GetAllByCustomer(customerId);
                foreach (var product in products)
                {
                    items.Add(new SelectListItem { Text = product.Name, Value = product.ProductId.ToString() });
                }
                vm.Products = items;
                return View("Registrations", vm);
            }
            return RedirectToAction("Customers");
        }


        [Route("Registrations/delete/{customerId}/{productId}")]
        public IActionResult Delete(int customerId, int productId)
        {
            var registration = _unitOfWork.Registration.Get(customerId,productId);
            if (registration == null)
            {
                return NotFound();
            }
            _unitOfWork.Registration.Remove(registration);
            _unitOfWork.Save();
            return RedirectToAction("Registrations", new { customerId = Convert.ToInt32(HttpContext.Session.GetString("CurrentCustomerId")) });
        }


        [Authorize(Roles = Role.ADMIN)]
        [Route("Registrations/add")]
        [HttpPost]
        public ActionResult AddProduct(int productId)
        {
            var customerId = Convert.ToInt32(HttpContext.Session.GetString("CurrentCustomerId"));
            if (productId != 0)
            {
                var product = _unitOfWork.Product.GetById(productId);
                if (product != null)
                {
                    var customer = _unitOfWork.Customer.GetById(customerId);
                    customer.Registrations.Add(new Registration { Product = product });
                    _unitOfWork.Customer.Update(customer);
                    _unitOfWork.Save();  
                }
            }
            return RedirectToAction("Registrations", new { customerId = customerId });
        }
    }


}

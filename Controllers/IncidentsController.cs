using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Dtos;
using SportsPro.Infrastructure.Interfaces;
using SportsPro.Models.IncidentModels;
using SportsPro.Models.ManageViewModels;

namespace SportsPro.Controllers
{
    [Authorize]
    public class IncidentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncidentsController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [Authorize(Roles = Role.ADMIN)]
        [Route("Incidents")]
        public IActionResult Index()
        {
            var model = new IncidentViewModel();
            model.IncidentList = _mapper.Map<List<IncidentDto>>(_unitOfWork.Incident.GetAll());
            return View(model);
        }



        [Authorize(Roles = Role.ADMIN)]
        [Route("Incidents/Add")]
        public IActionResult Create()
        {
            var vm = new IncidentViewModel();
            var Customers = _mapper.Map<List<CustomerDto>>(_unitOfWork.Customer.GetAll());
            if (Customers.Any())
            {
                vm.CustomerList.AddRange(Customers.Select(item => new SelectListItem { Text = item.FirstName, Value = item.CustomerId.ToString() }));
            }
            var Products = _mapper.Map<List<ProductDto>>(_unitOfWork.Product.GetAll());
            if (Products.Any())
            {
                vm.ProductList.AddRange(Products.Select(item => new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() }));
            }
            var Technicians = _mapper.Map<List<TechnicianDto>>(_unitOfWork.Technician.GetAll());
            if (Technicians.Any())
            {
                vm.TechnicianList.AddRange(Technicians.Select(item => new SelectListItem { Text = item.Name, Value = item.TechnicianId.ToString() }));
            }
            return View(vm);
        }


        [HttpPost]
        [Route("Incidents/Add")]
        [Authorize(Roles = Role.ADMIN)]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IncidentDto incident)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Incident.Add(_mapper.Map<Incident>(incident));
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            var vm = new IncidentViewModel();
            var Customers = _mapper.Map<List<CustomerDto>>(_unitOfWork.Customer.GetAll());
            if (Customers.Any())
            {
                vm.CustomerList.AddRange(Customers.Select(item => new SelectListItem { Text = item.FirstName, Value = item.CustomerId.ToString() }));
            }
            var Products = _mapper.Map<List<ProductDto>>(_unitOfWork.Product.GetAll());
            if (Products.Any())
            {
                vm.ProductList.AddRange(Products.Select(item => new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() }));
            }
            var Technicians = _mapper.Map<List<TechnicianDto>>(_unitOfWork.Technician.GetAll());
            if (Technicians.Any())
            {
                vm.TechnicianList.AddRange(Technicians.Select(item => new SelectListItem { Text = item.Name, Value = item.TechnicianId.ToString() }));
            }
            vm.Incident = incident;
            return View(vm);
        }


        [Route("Incidents/Edit/{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = _mapper.Map<IncidentDto>(_unitOfWork.Incident.Get(id.Value));
            if (incident == null)
            {
                return NotFound();
            }
            var vm = new IncidentViewModel();
            var Customers = _mapper.Map<List<CustomerDto>>(_unitOfWork.Customer.GetAll());
            if (Customers.Any())
            {
                vm.CustomerList.AddRange(Customers.Select(item => new SelectListItem { Text = item.FirstName, Value = item.CustomerId.ToString() }));
            }
            var Products = _mapper.Map<List<ProductDto>>(_unitOfWork.Product.GetAll());
            if (Products.Any())
            {
                vm.ProductList.AddRange(Products.Select(item => new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() }));
            }
            var Technicians = _mapper.Map<List<TechnicianDto>>(_unitOfWork.Technician.GetAll());
            if (Technicians.Any())
            {
                vm.TechnicianList.AddRange(Technicians.Select(item => new SelectListItem { Text = item.Name, Value = item.TechnicianId.ToString() }));
            }
            vm.Incident = incident;
            return View(vm);
        }




        [HttpPost]
        [Route("Incidents/Edit/{id}")]
        [Authorize(Roles = Role.ADMIN)]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IncidentDto incident)
        {
            if (id != incident.IncidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Incident.Update(_mapper.Map<Incident>(incident));
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Incident.IncidentExists(incident.IncidentId))
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
            var vm = new IncidentViewModel();
            var Customers = _mapper.Map<List<CustomerDto>>(_unitOfWork.Customer.GetAll());
            if (Customers.Any())
            {
                vm.CustomerList.AddRange(Customers.Select(item => new SelectListItem { Text = item.FirstName, Value = item.CustomerId.ToString() }));
            }
            var Products = _mapper.Map<List<ProductDto>>(_unitOfWork.Product.GetAll());
            if (Products.Any())
            {
                vm.ProductList.AddRange(Products.Select(item => new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() }));
            }
            var Technicians = _mapper.Map<List<TechnicianDto>>(_unitOfWork.Technician.GetAll());
            if (Technicians.Any())
            {
                vm.TechnicianList.AddRange(Technicians.Select(item => new SelectListItem { Text = item.Name, Value = item.TechnicianId.ToString() }));
            }
            vm.Incident = incident;
            return View(vm);
        }



        [Authorize(Roles = Role.ADMIN)]
        [Route("Incidents/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = _mapper.Map<IncidentDto>(_unitOfWork.Incident.GetById(id.Value));
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }


        [HttpPost, ActionName("Delete")]
        [Route("Incidents/Delete/{id}")]
        [Authorize(Roles = Role.ADMIN)]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var incident = _unitOfWork.Incident.GetById(id);
            _unitOfWork.Incident.Remove(incident);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        [Route("~/Techincident/get")]
        public IActionResult Techinicians()
        {
            var model = new TechnicianIncidentViewModel();
            var items = new List<SelectListItem>();
            var technicians = _mapper.Map<List<TechnicianDto>>(_unitOfWork.Technician.GetAll());
            foreach (var technician in technicians)
            {
                items.Add(new SelectListItem { Text = technician.Name, Value = technician.TechnicianId.ToString() });
            }
            model.Technicians = items;
            return View("Technicians", model);
        }

        [Route("~/Techincident/list")]
        [HttpPost]
        public ActionResult Incidents(int technicianId)
        {
            if (technicianId != 0)
            {
                return Redirect($"/techincident/list/{technicianId}");
                //var technician = await _context.Technicians.SingleOrDefaultAsync(t => t.TechnicianId == technicianId);
                //ViewBag.TechnicianName = technician?.Name;
                //var incidents = await _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Where(i => i.TechnicianId == technicianId).ToListAsync();
                //return View("Incidents", incidents);
            }
            return RedirectToAction("Techinicians");
        }

        [Route("~/Techincident/list/{id}")]
        [HttpGet]
        public ActionResult GetIncidents(int id)
        {
            if (id != 0)
            {
                HttpContext.Session.SetString("CurrentTechnicianId", id.ToString());
                var technician = _unitOfWork.Technician.GetById(id);
                ViewBag.TechnicianName = technician?.Name;
                var incidents = _mapper.Map<List<IncidentDto>>(_unitOfWork.Incident.GetAllByTechnician(id));
                return View("Incidents", incidents);
            }
            return RedirectToAction("Incidents");
        }


        [Route("Techincident/update/{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = _mapper.Map<IncidentDto>(_unitOfWork.Incident.Get(id.Value));
            if (incident == null)
            {
                return NotFound();
            }
            return View(incident);
        }



        [Route("Techincident/update/{id}")]
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        public IActionResult UpdateIncident(UpdateIncidentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var incident = _unitOfWork.Incident.Get(model.IncidentId);
                if (incident == null)
                {
                    return NotFound();
                }
                incident.Description = model.Description;
                incident.DateClosed = model.DateClosed;
                _unitOfWork.Incident.Update(incident);
                _unitOfWork.Save();
                return RedirectToAction("GetIncidents", new { id = Convert.ToInt32(HttpContext.Session.GetString("CurrentTechnicianId")) });
            }
            return RedirectToAction("Update", new { id = model.IncidentId });
        }


        #region Commented this code, because we have separate controller for the technician


        //[Route("Technicians/edit/{id}")]
        //[Authorize(Roles = Role.TECHNICIAN)]
        //public IActionResult TechnicianEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var incident = _mapper.Map<IncidentDto>(_unitOfWork.Incident.GetById(id.Value));
        //    if (incident == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CustomerId"] = new SelectList(_unitOfWork.Customer.GetAll(), "CustomerId", "Address", incident.CustomerId);
        //    ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "ProductId", "Code", incident.ProductId);
        //    ViewData["TechnicianId"] = new SelectList(_unitOfWork.Technician.GetAll(), "TechnicianId", "Email", incident.TechnicianId);
        //    return View(incident);
        //}

        //[HttpPost]
        //[Route("Technicians/edit/{id}")]
        //[Authorize(Roles = Role.TECHNICIAN)]
        //[ValidateAntiForgeryToken]
        //public IActionResult TechnicianEdit(int id,IncidentDto incident)
        //{
        //    if (id != incident.IncidentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _unitOfWork.Incident.Update(_mapper.Map<Incident>(incident));
        //            _unitOfWork.Save();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!_unitOfWork.Incident.IncidentExists(incident.IncidentId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        //return RedirectToAction(nameof(TechnicianIndex));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_unitOfWork.Customer.GetAll(), "CustomerId", "Address", incident.CustomerId);
        //    ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "ProductId", "Code", incident.ProductId);
        //    ViewData["TechnicianId"] = new SelectList(_unitOfWork.Technician.GetAll(), "TechnicianId", "Email", incident.TechnicianId);
        //    return View(incident);
        //}

        #endregion
    }
}

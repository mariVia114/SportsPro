using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Dtos;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class TechniciansController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public TechniciansController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [Route("Techinicians")]
        public IActionResult Index()
        {
            return View(_mapper.Map<List<TechnicianDto>>(_unitOfWork.Technician.GetAll().ToList()));
        }

        [Route("Technicians/Add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Technicians/Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TechnicianDto technician)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(technician.Email);
                var existingTechnician = _unitOfWork.Technician.FindByEmail(technician.Email);
                if (!(existingTechnician == null && existingUser == null))
                {
                    ModelState.AddModelError(string.Empty, "Techinician with the email already exist!");
                    return View(technician);
                }

                _unitOfWork.Technician.Add(_mapper.Map<Technician>(technician));
                _unitOfWork.Save();

                var username = technician.Email.Substring(0, technician.Email.IndexOf("@"));
                var user = new ApplicationUser { UserName = username, Email = technician.Email };
                var result = await _userManager.CreateAsync(user, username);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.TECHNICIAN);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(technician);
        }

        [Route("Technicians/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = _mapper.Map<TechnicianDto>(_unitOfWork.Technician.GetById(id.Value));
            if (technician == null)
            {
                return NotFound();
            }
            return View(technician);
        }

        [HttpPost]
        [Route("Technicians/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,TechnicianDto technician)
        {
            if (id != technician.TechnicianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Technician.Update(_mapper.Map<Technician>(technician));
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Technician.TechnicianExists(technician.TechnicianId))
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
            return View(technician);
        }

        [Route("Technicians/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = _mapper.Map<TechnicianDto>(_unitOfWork.Technician.GetById(id.Value));
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Technicians/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var technician = _unitOfWork.Technician.GetById(id);
            _unitOfWork.Technician.Remove(technician);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}

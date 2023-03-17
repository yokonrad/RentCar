using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Locations.GetAllAsync();

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await _unitOfWork.Locations.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Location location)
        {
            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Locations.AddAsync(location);

                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _unitOfWork.Locations.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Locations.UpdateAsync(location);

                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Locations.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _unitOfWork.Locations.GetByIdAsync(id);

            if (data != null)
            {
                _ = await _unitOfWork.Locations.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
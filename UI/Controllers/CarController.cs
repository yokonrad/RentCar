using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Cars.GetAllAsync();

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await _unitOfWork.Cars.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Cars.AddAsync(car);

                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _unitOfWork.Cars.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Cars.UpdateAsync(car);

                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Cars.GetByIdAsync(id);

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
            var data = await _unitOfWork.Cars.GetByIdAsync(id);

            if (data != null)
            {
                _ = await _unitOfWork.Cars.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
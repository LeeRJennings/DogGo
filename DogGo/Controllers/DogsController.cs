using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        private readonly IDogRepository _walkerRepo;

        // ASP.NET will give us an instance of our Dog Repository. This is called "Dependency Injection"
        public DogsController(IDogRepository walkerRepository)
        {
            _walkerRepo = walkerRepository;
        }

        // GET: DogsController
        public ActionResult Index()
        {
            List<Dog> walkers = _walkerRepo.GetAllDogs();

            return View(walkers);
        }

        // GET: DogsController/Details/5
        public ActionResult Details(int id)
        {
            Dog walker = _walkerRepo.GetDogById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // GET: DogsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
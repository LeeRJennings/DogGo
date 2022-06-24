using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using System.Collections.Generic;
using DogGo.Models;
using System;
using DogGo.Models.ViewModels;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalksRepository _walksRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IWalksRepository walksRepository, IOwnerRepository ownerRepository)
        {
            _walkerRepo = walkerRepository;
            _walksRepo = walksRepository;
            _ownerRepo = ownerRepository;
        }

        // GET: WalkersController
        public ActionResult Index()
        {
            int loggedInUserId = GetCurrentUserId();

            if (loggedInUserId == 0)
            {
                List<Walker> allWalkers = _walkerRepo.GetAllWalkers();

                return View(allWalkers);
            }
            else
            {
                Owner loggedInUser = _ownerRepo.GetOwnerById(loggedInUserId);
            
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(loggedInUser.NeighborhoodId);

                return View(walkers);
            }
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetWalksByWalker(walker.Id);

            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
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

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
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
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
            {
                return 0;
            }
            else
            {
                return int.Parse(id);
            }
        }
    }
}

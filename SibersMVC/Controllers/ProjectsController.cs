using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SibersDAL.EF;
using SibersDAL.Models;
using SibersDAL.Repos;
using System.Data.Entity.Infrastructure;

namespace SibersMVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectRepo projectRepo = new ProjectRepo();
        private readonly ContractorRepo contractorRepo = new ContractorRepo();
        private readonly CustomerRepo customerRepo = new CustomerRepo();
        private readonly EmployeeRepo employeeRepo = new EmployeeRepo();

        // GET: Projects
        public async Task<ActionResult> Index(string sortOrder, string startDate, string endDate)
        {
            IEnumerable<Project> project;
            ViewBag.StartDateParam = startDate;
            ViewBag.EndDateParam = endDate;
            ViewBag.PrioritySortParm = sortOrder == "Priority" ? "Priority desc" : "Priority";

            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                project = await projectRepo.GetAllAsync();
            }
            else
            {
                project = from p in await projectRepo.GetAllAsync()
                          where p.StartDate >= Convert.ToDateTime(startDate) &&
                                p.StartDate <= Convert.ToDateTime(endDate)
                          select p;
            }
            if (project == null)
            {
                return HttpNotFound();
            }

            switch (sortOrder)
            {
                case "Priority":
                    project = project.OrderBy(p => p.Priority).ToList();
                    break;
                case "Priority desc":
                    project = project.OrderByDescending(p => p.Priority).ToList();
                    break;
                default:
                    project = project.OrderBy(p => p.Id).ToList();
                    break;
            }
           
            return View(project);
        }
        
        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await projectRepo.GetOneAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.ContractorId = new SelectList(contractorRepo.GetAll(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(customerRepo.GetAll(), "Id", "Name");
            ViewBag.ManagerId = new SelectList(employeeRepo.GetAll(), "Id", "FullName");
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,CustomerId,ContractorId,ManagerId,StartDate,EndDate,Priority,Comment")] Project project)
        {
            ViewBag.ContractorId = new SelectList(contractorRepo.GetAll(), "Id", "Name", project.ContractorId);
            ViewBag.CustomerId = new SelectList(customerRepo.GetAll(), "Id", "Name", project.CustomerId);
            ViewBag.ManagerId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", project.ManagerId);
            if (!ModelState.IsValid) return View(project);
            try
            {
                await projectRepo.AddAsync(project);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
                return View(project);
            }
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await projectRepo.GetOneAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractorId = new SelectList(contractorRepo.GetAll(), "Id", "Name", project.ContractorId);
            ViewBag.CustomerId = new SelectList(customerRepo.GetAll(), "Id", "Name", project.CustomerId);
            ViewBag.ManagerId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", project.ManagerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CustomerId,ContractorId,ManagerId,StartDate,EndDate,Priority,Comment,Timestamp")] Project project)
        {
            ViewBag.ContractorId = new SelectList(contractorRepo.GetAll(), "Id", "Name", project.ContractorId);
            ViewBag.CustomerId = new SelectList(customerRepo.GetAll(), "Id", "Name", project.CustomerId);
            ViewBag.ManagerId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", project.ManagerId);
            if (!ModelState.IsValid) return View(project);
            try
            {
                await projectRepo.SaveAsync(project);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save record. Another user updated the record.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save record: {ex.Message}");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await projectRepo.GetOneAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Id,Timestamp")] Project project)
        {
            try
            {
                await projectRepo.DeleteAsync(project);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record. Another user updated the record.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record: {ex.Message}");
            }
            ViewBag.ContractorId = new SelectList(contractorRepo.GetAll(), "Id", "Name", project.ContractorId);
            ViewBag.CustomerId = new SelectList(customerRepo.GetAll(), "Id", "Name", project.CustomerId);
            ViewBag.ManagerId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", project.ManagerId);
            return View(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                projectRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class ProjectEmployeesController : Controller
    {
        private readonly ProjectEmployeesRepo projectEmployeeRepo = new ProjectEmployeesRepo();
        private readonly ProjectRepo projectRepo = new ProjectRepo();
        private readonly EmployeeRepo employeeRepo = new EmployeeRepo();

        // GET: Projects
        public async Task<ActionResult> Index()
        {
            return View(await projectEmployeeRepo.GetAllAsync());
        }

        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectEmployee = await projectEmployeeRepo.GetOneAsync(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(projectRepo.GetAll(), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(employeeRepo.GetAll(), "Id", "FullName");
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProjectId, EmployeeId")] ProjectEmployees projectEmployee)
        {
            ViewBag.ProjectId = new SelectList(projectRepo.GetAll(), "Id", "Name", projectEmployee.ProjectId);
            ViewBag.EmployeeId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", projectEmployee.EmployeeId);
            if (!ModelState.IsValid) return View(projectEmployee);
            try
            {
                await projectEmployeeRepo.AddAsync(projectEmployee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
                return View(projectEmployee);
            }
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectEmployee = await projectEmployeeRepo.GetOneAsync(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(projectRepo.GetAll(), "Id", "Name", projectEmployee.ProjectId);
            ViewBag.EmployeeId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", projectEmployee.EmployeeId);
            return View(projectEmployee);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, ProjectId, EmployeeId, Timestamp")] ProjectEmployees projectEmployee)
        {
            ViewBag.ProjectId = new SelectList(projectRepo.GetAll(), "Id", "Name", projectEmployee.ProjectId);
            ViewBag.EmployeeId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", projectEmployee.EmployeeId);
            if (!ModelState.IsValid) return View(projectEmployee);
            try
            {
                await projectEmployeeRepo.SaveAsync(projectEmployee);
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
            return View(projectEmployee);
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectEmployee = await projectEmployeeRepo.GetOneAsync(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Id, Timestamp")] ProjectEmployees projectEmployee)
        {
            try
            {
                await projectEmployeeRepo.DeleteAsync(projectEmployee);
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
            ViewBag.ProjectId = new SelectList(projectRepo.GetAll(), "Id", "Name", projectEmployee.ProjectId);
            ViewBag.EmployeeId = new SelectList(employeeRepo.GetAll(), "Id", "FullName", projectEmployee.EmployeeId);
            return View(projectEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                projectEmployeeRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

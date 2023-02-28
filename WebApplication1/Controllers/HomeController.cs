using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var myVM = new MyViewModel();
            myVM.DepartmentList = await _context.Departments.ToListAsync();
            return View(myVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentAsync(Guid guid)
        {
            var depList = await GetDepartmentListAsync();
            var dep = depList.FirstOrDefault(d => d.Id == guid);
            if (dep != null)
            {
                var myVM = new MyViewModel();
                myVM.DepartmentList = depList;
                myVM.Department = dep;
                myVM.EmployeeList = _context.Employees
                    .Where(e => e.DepartmentID == guid)
                    .OrderBy(e => e.FirstName).ThenBy(e => e.SurName).ThenBy(e => e.Patronymic)
                    .ToList();
                return View("../Department/ShowDepartment", myVM);
            }
            var msg = "Отдел с ID=" + guid + " не найден.";
            _logger.LogInformation(msg);
            return View("../Shared/Error", new ErrorViewModel(msg));

        }

        [Route("Home/AddDepartment")]
        public async Task<IActionResult> AddDepartmentAsync(Department department)
        {

            var myVM = new MyViewModel();
            myVM.DepartmentList = await GetDepartmentListAsync();
            return View("../Department/Department", myVM);
        }

        [HttpGet]
        [Route("Home/EditDepartment")]
        public async Task<IActionResult> EditDepartmentAsync(Guid guid)
        {
            var depList = await GetDepartmentListAsync();
            var dep = depList.FirstOrDefault(d => d.Id == guid);
            if (dep != null)
            {
                var myVM = new MyViewModel();
                myVM.Department = dep;
                myVM.DepartmentList = depList;
                return View("../Department/Department", myVM);
            }
            var msg = "Отдел с ID=" + guid + " не найден.";
            _logger.LogInformation(msg);
            return View("../Shared/Error", new ErrorViewModel(msg));
        }

        [HttpPost]
        public async Task<IActionResult> SaveDepartmentAsync(Department department)
        {
            try
            {
                var depInDb = await _context.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);
                if (department.ParentDepartmentID == Guid.Empty)
                {
                    department.ParentDepartmentID = null;
                }
                if (depInDb == null)
                {
                    await _context.Departments.AddAsync(department);
                }
                else
                {
                    depInDb.Name = department.Name;
                    depInDb.ParentDepartmentID = department.ParentDepartmentID;
                    depInDb.Code = department.Code;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("../Shared/Error", new ErrorViewModel(ex.Message));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartmentAsync(Guid guid, bool confirmed = false)
        {
            var dep = await _context.Departments.FirstOrDefaultAsync(d => d.Id == guid);
            if (dep == null)
            {
                var msg = "Отдел с ID=" + guid + " не найден.";
                _logger.LogInformation(msg);
                return View("../Shared/Error", new ErrorViewModel(msg));
            }
            if (confirmed && dep != null)
            {
                var emplInDep = await _context.Employees.CountAsync(e => e.DepartmentID == guid);
                var subDeps = await _context.Departments.CountAsync(sd => sd.ParentDepartmentID == guid);
                if (emplInDep > 0 || subDeps > 0)
                {
                    var msg = "Нельзя удалить отдел, в котором есть подотделы или сотрудники.";
                    return View("../Shared/Error", new ErrorViewModel(msg));
                }
                try
                {
                    _context.Remove(dep);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return View("../Shared/Error", new ErrorViewModel(ex.Message));
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return View("../Shared/DeleteConfirmation", new MyViewModel() { Department = dep });
        }
        private async Task<List<Department>> GetDepartmentListAsync()
        {
            var depList = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
            depList.Insert(0, new Department() { Name = "-" });
            return depList;
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
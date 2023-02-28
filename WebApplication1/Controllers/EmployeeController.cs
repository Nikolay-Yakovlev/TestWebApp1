using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public EmployeeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> GetEmployeeAsync(int? id)
        {
            if (id != null)
            {
                var myVM = new MyViewModel();
                myVM.Employee =  await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                myVM.DepartmentList = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
                return View("../Employee/Employee", myVM);
            }
            var msg = "Пользователь с ID="+ id +" не найден!";
            _logger.LogInformation(msg);
            return View("../Shared/Error", new ErrorViewModel(msg));
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployeeAsync()
        {
            var myVM = new MyViewModel();
            myVM.DepartmentList = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
            return View("../Employee/Employee", myVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                var emplInDb = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
                if (emplInDb == null)
                {
                    await _context.Employees.AddAsync(employee);
                }
                else
                {
                    emplInDb.SurName = employee.SurName;
                    emplInDb.FirstName = employee.FirstName;
                    emplInDb.Patronymic = employee.Patronymic;
                    emplInDb.DepartmentID = employee.DepartmentID;
                    emplInDb.DateOfBirth = employee.DateOfBirth;
                    emplInDb.DocSeries = employee.DocSeries;
                    emplInDb.DocNumber = employee.DocNumber;
                    emplInDb.Position = employee.Position;
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
        public async Task<IActionResult> DeleteEmployeeAsync(int id, bool confirmed = false)
        {
            var empl = await _context.Employees.FirstOrDefaultAsync(d => d.Id == id);
            if (empl == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (confirmed && empl != null)
            {
                try
                {
                    _context.Remove(empl);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    return View("../Shared/Error", new ErrorViewModel(ex.Message));
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return View("../Shared/DeleteConfirmation", new MyViewModel() { Employee = empl });
        }

    }
}
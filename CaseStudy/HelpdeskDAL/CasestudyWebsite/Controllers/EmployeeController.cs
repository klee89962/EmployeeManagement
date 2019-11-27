using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.Core.Logging;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CaseStudy1.Controllers
{
    // Method avaialbe at URL of api/...
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        // Constructor
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
        // HttpGet: GetByEmail, get employee by their email
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                // Declare Employee VM
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                viewmodel.Email = email;
                // Call GetByEmail
                viewmodel.GetByEmail();
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            } // try/catch
        }
        // HttpPut: Put(EmployeeViewModel), update employee in database
        [HttpPut]
        public IActionResult Put(EmployeeViewModel viewmodel)
        {
            try
            {
                // Call update function
                int retVal = viewmodel.Update();
                // Return the status
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "Employee " + viewmodel.Lastname + " updated!" });
                    case -1:
                        return Ok(new { msg = "Employee " + viewmodel.Lastname + " not updated!" });
                    case -2:
                        return Ok(new { msg = "Data is stale for " + viewmodel.Lastname + ", Employee not updated!" });
                    default:
                        return Ok(new { msg = "Employee " + viewmodel.Lastname + " not updated!" });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            } // try/catch
        }
        //HttpGet: GetAll(), declares Employee View Model, calls GetAll() to get
        //  employees lists
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // Declare Employee VM
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                // Call GetAll()
                List<EmployeeViewModel> allEmployees = viewmodel.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            } // try/catch
        }
        // HttpPost: Post(EmployeeViewModel), adds a new employee into database
        [HttpPost]
        public IActionResult Post(EmployeeViewModel viewmodel)
        {
            try
            {
                // Calls add function
                viewmodel.Add();
                // Returns status message after employee add function
                return viewmodel.Id > 1
                    ? Ok(new { msg = "Employee " + viewmodel.Lastname + " added!" })
                    : Ok(new { msg = "Employee " + viewmodel.Lastname + " not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            } // try/catch
        }
        // HttpDelete: Delete(int), deletes employee from database with matching id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Declares view model
                EmployeeViewModel viewmodel = new EmployeeViewModel();
                viewmodel.Id = id;
                // Returns status message after employee delete function
                return viewmodel.Delete() == 1
                    ? Ok(new { msg = "Student " + id + " deleted!" })
                    : Ok(new { msg = "Student " + id + " not deleted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            } // try/catch
        }
    }
}
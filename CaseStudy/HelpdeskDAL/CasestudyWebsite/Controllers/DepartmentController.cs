using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CaseStudy1.Controllers
{
    // Method avaialbe at URL of api/...
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger _logger;
        // Constructor
        public DepartmentController(ILogger<DepartmentController> logger)
        {
            _logger = logger;
        }
        
        // HttpGet: GetAll(), declares Department View Model, calls GetAll() to get
        //  department lists
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                DepartmentViewModel vm = new DepartmentViewModel();
                var allDivs = vm.GetAll();
                return Ok(allDivs);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

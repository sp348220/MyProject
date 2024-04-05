using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CppCompilerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CompileAndRun(string code)
        {
            string fileName = $"temp_{Guid.NewGuid().ToString().Substring(0, 8)}.cpp";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                // Save the code to a temporary file
                await System.IO.File.WriteAllTextAsync(filePath, code);

                // Compile the code
                Process compileProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = "g++",
                    Arguments = $"{filePath} -o {Path.ChangeExtension(filePath, ".exe")}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                });

                compileProcess.WaitForExit();

                // Check if compilation was successful
                if (compileProcess.ExitCode != 0)
                {
                    string compileError = compileProcess.StandardError.ReadToEnd();
                    return BadRequest($"Compilation error: {compileError}");
                }

                // Execute the compiled program
                Process executeProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = Path.ChangeExtension(filePath, ".exe"),
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                });

                string output = await executeProcess.StandardOutput.ReadToEndAsync();

                return Ok(output);
            }
            finally
            {
                // Clean up temporary files
                System.IO.File.Delete(filePath);
                System.IO.File.Delete(Path.ChangeExtension(filePath, ".exe"));
            }
        }
    }
}







//using DocumentManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using DomainObject.Employee;
//using Repository.Employee;
//using Models.Employee;
//using AutoMapper;
//using Newtonsoft.Json;
//using System.Text.Json.Nodes;

//namespace DocumentManagementSystem.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }

//        public ActionResult getemployeemasterdata()
//        {
//            GetEmployeeObjectFactory obj = new GetEmployeeObjectFactory();
//            List<EmployeeDto> empdto = obj.getEmplyoyeeMasterData();
//            List<EmployeeModel> emplist = new List<EmployeeModel>();

//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<EmployeeDto, EmployeeModel>();
//                // Add other mappings if needed
//            });

//            // Create mapper instance
//            IMapper mapper = config.CreateMapper();

//            // Assuming obj is an instance of some class containing getEmplyoyeeMasterData() method
//            //List<EmployeeDto> empdto = obj.getEmplyoyeeMasterData();

//            // Map EmployeeDto list to EmployeeModel list
//            emplist = mapper.Map<List<EmployeeModel>>(empdto);



//            return View(emplist);

//        }
//    }
//}

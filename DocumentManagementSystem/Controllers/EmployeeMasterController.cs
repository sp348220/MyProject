using AutoMapper;
using DomainObject.Employee;
using Microsoft.AspNetCore.Mvc;
using Models.Employee;
using Repository.Employee;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Models.Employee;



namespace DocumentManagementSystem.Controllers
{
    public class EmployeeMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult getdetails()
        {
            GetEmployeeObjectFactory obj = new GetEmployeeObjectFactory();
            List<EmployeeDto> empdto = obj.getEmplyoyeeMasterData();
            List<EmployeeModel> emplist = new List<EmployeeModel>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDto, EmployeeModel>();
                // Add other mappings if needed
            });

            // Create mapper instance
            IMapper mapper = config.CreateMapper();

            // Assuming obj is an instance of some class containing getEmplyoyeeMasterData() method
            //List<EmployeeDto> empdto = obj.getEmplyoyeeMasterData();

            // Map EmployeeDto list to EmployeeModel list
            emplist = mapper.Map<List<EmployeeModel>>(empdto);



            return View(emplist);

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeModel emp)
        {

            GetEmployeeObjectFactory obj = new GetEmployeeObjectFactory();
            EmployeeDto empdto = new EmployeeDto();
            //EmployeeModel Empobj=new EmployeeModel();  

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeModel, EmployeeDto>();
                // Add other mappings if needed
            });

            IMapper mapper = config.CreateMapper();


            empdto = mapper.Map<EmployeeDto>(emp);


            string name = empdto.Name;

            bool count = obj.AddEmployeeMasterData(empdto);
            if (count == true)
            {
                TempData["CreateMessage"] = "Data Inserted Successfully ";
                ModelState.Clear();
                return RedirectToAction("getdetails");
            }
            else
            {
                ViewBag.CreateErrorMessage = "Data Not Inserted........MO ";
                return View();

            }

        }

        //Return Json TO do AJAX CALL get Data to view from controller
        [HttpGet]
        public ActionResult Edit(int Id)
        {

            GetEmployeeObjectFactory obj = new GetEmployeeObjectFactory();
            var EmployeeDtoRow = obj.getEmplyoyeeMasterData().Find(model => model.Id == Id);
            //EmployeeModel row=new EmployeeModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDto, EmployeeModel>();
                // Add other mappings if needed
            });

            // Create mapper instance
            IMapper mapper = config.CreateMapper();

            var row = mapper.Map<EmployeeModel>(EmployeeDtoRow);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(int Id, EmployeeModel emp)
        {
            GetEmployeeObjectFactory obj = new GetEmployeeObjectFactory();
            EmployeeDto employeeDto = new EmployeeDto();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeModel, EmployeeDto>();
                // Add other mappings if needed
            });

            // Create mapper instance
            IMapper mapper = config.CreateMapper();

            employeeDto = mapper.Map<EmployeeDto>(emp);
            bool count = obj.UpdateEmployeeMasterData(employeeDto);
            if (count == true)
            {
                TempData["UpdateMessage"] = "Data Inserted Successfully ";
                ModelState.Clear();
                return RedirectToAction("getdetails");
            }
            else
            {
                ViewBag.CreateUpdateMessage = "Data Not Inserted........MO ";
                return View();

            }
        }
    }
}

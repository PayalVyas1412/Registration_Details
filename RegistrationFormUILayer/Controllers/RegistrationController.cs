using Microsoft.AspNetCore.Mvc;
using RegistrationFormBusinessLayer;
using RegistrationFormDataAccessLayer;

namespace RegistrationFormUILayer.Controllers
{

    public class RegistrationController : Controller
    {
        DLRegistrationDetails dLRegistrationDetails = new DLRegistrationDetails();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult getStateList()
        {
            List<StateMaster> stateMasters = new List<StateMaster>();
            stateMasters = dLRegistrationDetails.GetStateMasters();
            return Json(stateMasters);
        }
        public JsonResult getCitiesList(int StateID)
        {
            List<CityMaster> cityMasters = new List<CityMaster>();
            cityMasters = dLRegistrationDetails.GetCitiesMasters(StateID);
            return Json(cityMasters);
        }
        [HttpGet]
        public JsonResult getRegistrationById(int id)
        {
            List<Register> registers = new List<Register>();
            registers = dLRegistrationDetails.GetRegisterfromID(id);
            return Json(registers);

        }
        [HttpPost]
        public JsonResult getResgistrationDetails()
        {
            List<Register> registers = new List<Register>();
            registers = dLRegistrationDetails.getAllRegisteration();
            return Json(registers);
        } 

       [HttpPost]
        public JsonResult SaveRegistrationdetails(Register register)
        {
            string result = "";
            if (register.ID == 0)
            {
                result = dLRegistrationDetails.saveRegistrationDetails(register);
                
            }
            else
            {
                result = dLRegistrationDetails.updateRegistrationDetails(register);
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult deleteRegistrationDetails(int id)
        {
            string result = "";
            result = dLRegistrationDetails.deleteRegistrationDetails(id);
            return Json(result);
        }
       
    }
    
}

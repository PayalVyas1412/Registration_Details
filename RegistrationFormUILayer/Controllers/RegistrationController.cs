using Microsoft.AspNetCore.Mvc;
using RegistrationFormBusinessLayer;
using RegistrationFormDataAccessLayer;

namespace RegistrationFormUILayer.Controllers
{

    public class RegistrationController : Controller
    {
        BLRegistrationDetails bLRegistrationDetails = new BLRegistrationDetails();
        string result = "";
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// This method get the list of States which are stored in the database
        /// </summary>
        /// <returns></returns>
        public JsonResult getStateList()
        {
            try
            {
                List<StateMaster> stateMasters = new List<StateMaster>();
                stateMasters = bLRegistrationDetails.BLGetStateMasters();
                return Json(stateMasters);
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
                return Json(result);
            }
            
        }
        /// <summary>
        /// This method get the list of cities on the based of which state is selected from stored in the database
        /// </summary>
        /// <returns></returns>
        public JsonResult getCitiesList(int StateID)
        {
            try
            {
                List<CityMaster> cityMasters = new List<CityMaster>();
                cityMasters = bLRegistrationDetails.BLGetCitiesMasters(StateID);
                return Json(cityMasters);
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
                return Json(result);
            }
            
        }
        /// <summary>
        /// This method will get all the record according to the id passed from the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult getRegistrationById(int id)
        {
            try
            {
                List<Register> registers = new List<Register>();
                registers = bLRegistrationDetails.BLGetRegisterfromID(id);
                return Json(registers);
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
                return Json(result);
            }
        }
        /// <summary>
        /// get the list of registration details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getResgistrationDetails()
        {
            try
            {
                List<Register> registers = new List<Register>();
                registers = bLRegistrationDetails.BLgetAllRegisteration();
                return Json(registers);
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
                return Json(result);
            }
            
        } 
        /// <summary>
        /// Save  or update registration data in datatabase
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveRegistrationdetails(Register register)
        {
            try
            {
                if (register.ID == 0)
                {
                    result = bLRegistrationDetails.BLsaveRegistrationDetails(register);

                }
                else
                {
                    result = bLRegistrationDetails.BLupdateRegistrationDetails(register);
                }
            }
            catch(Exception ex)
            {
                result= ex.Message.ToString();
                
            }
            return Json(result);
        }
        /// <summary>
        /// To delete the record from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult deleteRegistrationDetails(int id)
        {
            try
            {
                result = bLRegistrationDetails.BLdeleteRegistrationDetails(id);
                return Json(result);
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
            }
            return Json(result);
        }      
    }
    
}

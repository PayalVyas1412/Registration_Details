using Microsoft.Win32;
using RegistrationFormDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormBusinessLayer
{
    public class BLRegistrationDetails
    {
        string result = "";
        DLRegistrationDetails registrationDetails = new DLRegistrationDetails();
        public string BLsaveRegistrationDetails(Register register)
        {
            try
            {
                result = registrationDetails.saveRegistrationDetails(register);
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
        public string BLupdateRegistrationDetails(Register register)
        {
            try
            {
                result = registrationDetails.updateRegistrationDetails(register);
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
        public string BLdeleteRegistrationDetails(int ID)
        {
            try
            {
                result = registrationDetails.deleteRegistrationDetails(ID);
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
        public List<Register> BLgetAllRegisteration()
        {
            List<Register> registers = new List<Register>();
            try
            {
                registers = registrationDetails.getAllRegisteration();               
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return registers;
        }
        public List<Register> BLGetRegisterfromID(int ID)
        {
            List<Register> registers = new List<Register>();
            try
            {
                registers = registrationDetails.GetRegisterfromID(ID);
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return registers;
        }
        public List<StateMaster> BLGetStateMasters()
        {
            List<StateMaster> stateMasters = new List<StateMaster>();
            try
            {
                stateMasters = registrationDetails.GetStateMasters();
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return stateMasters;
        }
        public List<CityMaster> BLGetCitiesMasters(int stateID)
        {
            List<CityMaster> cityMasters = new List<CityMaster>();
            try
            {
                cityMasters = registrationDetails.GetCitiesMasters(stateID);
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return cityMasters;
        }
    }
}

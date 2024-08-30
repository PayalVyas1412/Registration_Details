using RegistrationFormBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormDataAccessLayer
{
    public interface IDLResgistrationDetails
    {
        List<Register> getAllRegisteration();
        string saveRegistrationDetails(Register register);
        string updateRegistrationDetails(Register register);
        List<Register> GetRegisterfromID(int ID);
        string deleteRegistrationDetails(int ID);
        List<StateMaster> GetStateMasters();

    }
}

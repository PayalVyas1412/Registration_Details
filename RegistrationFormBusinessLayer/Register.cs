using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormBusinessLayer
{
    public class Register
    {
        public int ID { get; set; }
        public string ?Name { get; set; }
        public string ?Address { get; set; }
        public string ?Email { get; set; }
        public string ?phone { get; set; }
        public int stateID { get; set; }
        public string? stateName { get; set; }

        public int cityID { get; set; }
        public string ?cityName { get; set; }
    }
}

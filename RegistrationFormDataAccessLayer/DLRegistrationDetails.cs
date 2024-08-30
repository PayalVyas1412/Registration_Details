using Microsoft.Win32;
using RegistrationFormBusinessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormDataAccessLayer
{
    public class DLRegistrationDetails : IDLResgistrationDetails
    {
        string constr = "Server=DESKTOP-5BBKFBJ\\MSSQLSERVER16;Database=Registration;User Id=sa;Password=Payu$1402";

        public string saveRegistrationDetails(Register register)
        {
            string result = "";
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand("Sp_CrudOperation", _con);
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.Parameters.AddWithValue("@Type", "INSERT");
                Cmnd.Parameters.AddWithValue("@ID", "");
                Cmnd.Parameters.AddWithValue("@Name", Convert.ToString(register.Name));
                Cmnd.Parameters.AddWithValue("@Email", Convert.ToString(register.Email));
                Cmnd.Parameters.AddWithValue("@Phone", Convert.ToString(register.phone));
                Cmnd.Parameters.AddWithValue("@Address", Convert.ToString(register.Address));
                Cmnd.Parameters.AddWithValue("@StateID", Convert.ToString(register.stateID));
                Cmnd.Parameters.AddWithValue("@CityID", Convert.ToString(register.cityID));
                int success = Convert.ToInt32(Cmnd.ExecuteNonQuery());
                if (success > 0)
                {
                    result = "Inserted Successfully";
                }
                else
                {
                    result = "Data not inserted";
                }
                _con.Close();

            }

            return result;
        }

        public string deleteRegistrationDetails(int ID)
        {
            string result = "";
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand("Sp_DeleteRecord", _con);
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
                int success = Convert.ToInt32(Cmnd.ExecuteNonQuery());
                if (success > 0)
                {
                    result = "Deleted Successfully";
                }
                else
                {
                    result = "Data not deleted";
                }
                _con.Close();
            }
            return result;
        }

        public List<Register> getAllRegisteration()
        {
            List<Register> registers = new List<Register>();
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand("Sp_SelectRegDetails", _con);
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.Parameters.AddWithValue("@ID", "");
                Cmnd.Parameters.AddWithValue("@Type", "GetList");
                SqlDataReader reader=Cmnd.ExecuteReader();
                while (reader.Read())
                {
                    Register register = new Register
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Email= reader["Email"].ToString(),
                        phone = reader["phone"].ToString()
                    };
                    registers.Add(register);
                }
                _con.Close();
            }
            return registers;

        }

        public List<Register> GetRegisterfromID(int ID)
        {
            List<Register> registers = new List<Register>();
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand("Sp_SelectRegDetails", _con);
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
                Cmnd.Parameters.AddWithValue("@Type", "GetListById");
                SqlDataReader reader = Cmnd.ExecuteReader();
                while (reader.Read())
                {
                    Register register = new Register
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        phone = reader["phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        stateID= Convert.ToInt32(reader["StateID"]),
                        stateName= reader["StateName"].ToString(),
                        cityID = Convert.ToInt32(reader["CityID"]),
                        cityName = reader["CityName"].ToString()
                    };
                    registers.Add(register);
                }
                _con.Close();
            }
            return registers;
        }

        public string updateRegistrationDetails(Register register)
        {
            string result = "";
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand("Sp_CrudOperation", _con);
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.Parameters.AddWithValue("@Type", "UPDATE");
                Cmnd.Parameters.AddWithValue("@ID", Convert.ToInt32(register.ID));
                Cmnd.Parameters.AddWithValue("@Name", Convert.ToString(register.Name));
                Cmnd.Parameters.AddWithValue("@Email", Convert.ToString(register.Email));
                Cmnd.Parameters.AddWithValue("@Phone", Convert.ToString(register.phone));
                Cmnd.Parameters.AddWithValue("@Address", Convert.ToString(register.Address));
                Cmnd.Parameters.AddWithValue("@StateID", Convert.ToString(register.stateID));
                Cmnd.Parameters.AddWithValue("@CityID", Convert.ToString(register.cityID));
                int success = Convert.ToInt32(Cmnd.ExecuteNonQuery());
                if (success > 0)
                {
                    result = "Update Successfully";
                }
                else
                {
                    result = "Data not updated";
                }
                _con.Close();

            }
            return result;
        }
        public List<StateMaster> GetStateMasters()
        {
            List<StateMaster> states = new List<StateMaster>();
            string query = $"select [ID],[State Name] from [Registration].[dbo].[tblState]";
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand(query, _con);
                SqlDataReader rdr = Cmnd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        StateMaster smaster = new StateMaster();
                        smaster.ID = Convert.ToInt32(rdr["ID"]);
                        smaster.StateName = rdr["State Name"].ToString();
                        states.Add(smaster);
                    }
                }
                _con.Close();
            }
            return states;
        }
        public List<CityMaster> GetCitiesMasters(int stateID)
        {
            List<CityMaster> cities = new List<CityMaster>();
            string query = $"select [ID],[City Name] from  [Registration].[dbo].[tblCity] where StateID='"+ stateID + "'";
            using (SqlConnection _con = new SqlConnection(constr))
            {
                _con.Open();
                SqlCommand Cmnd = new SqlCommand(query, _con);
                SqlDataReader rdr = Cmnd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        CityMaster city = new CityMaster();
                        city.ID = Convert.ToInt32(rdr["ID"]);
                        city.CityName = rdr["City Name"].ToString();
                        cities.Add(city);
                    }
                }
                _con.Close();
            }
            return cities;
        }
    }
}

using Library.Clinic.Models;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
//using System.Data.SqlClient;
//using Microsoft.

namespace Api.Clinic.Database
{
    public class MySqlContext
    {
        //root - 3368Dogs
        private string connectionstring = "Server=localhost;Uid=root;Password=3368Dogs;Database=Clinic;";
        //MySqlConnection realconnection = new MySqlConnection("Server=127.0.0.1;Uid=root;Password=3368Dogs;Database=Clinic;");
        public MySqlContext() { }

        public IEnumerable<Patient> GetPatients()
        {
            var retval = new List<Patient>();
            using (var conn=new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    using (var command = new SqlCommand("GetPatients",conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader=command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                var patient = new Patient
                                {
                                    Id = (int)reader["PatientId"],
                                    Name = reader["Name"].ToString() ?? string.Empty,
                                    Gender = reader["Gender"].ToString() ?? string.Empty,
                                    Race = reader["Race"].ToString() ?? string.Empty,
                                    Address = reader["Address"].ToString() ?? string.Empty,
                                };
                                retval.Add(patient);
                            }
                        }
                    }
                }
                catch(Exception ex) 
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return retval;
        }

        public IEnumerable<Physician> GetPhysicians()
        {
            var retval = new List<Physician>();
            using (var conn = new SqlConnection(connectionstring))
            {
                var str = "select * from Physicians";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = str;

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var physician = new Physician
                            {
                                EmployeeId = (int)reader["EmployeeId"],
                                FName = reader["FirstName"].ToString() ?? string.Empty,
                                LName = reader["LastName"].ToString() ?? string.Empty,
                                LicenseNumber = reader["LicenseNumber"].ToString() ?? string.Empty,
                            };
                            retval.Add(physician);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            return retval;
        }
    }
}

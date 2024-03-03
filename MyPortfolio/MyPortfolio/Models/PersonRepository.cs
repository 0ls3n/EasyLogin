using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace MyPortfolio.Models
{
    public class PersonRepository
    {
        private List<Person> personList;

        string? ConnectionString;

        public PersonRepository()
        {
            personList = new List<Person>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void CreateNewPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO PERSON VALUES (@Username, @Password, @Email, @DisplayName)", con);
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = person.Username;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = person.Password;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = person.Email;
                cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = person.DisplayName;
                person.Id = Convert.ToInt32(cmd.ExecuteScalar());

                personList.Add(person);
            }
        }
    }
}

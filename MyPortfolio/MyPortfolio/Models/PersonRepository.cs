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
        private List<Person> peronList;

        string? ConnectionString;

        public PersonRepository()
        {
            peronList = new List<Person>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void CreateNewPerson(Person person)
        {
            peronList.Add(person);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
            }
        }
    }
}

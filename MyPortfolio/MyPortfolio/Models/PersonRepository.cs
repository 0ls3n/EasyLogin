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

            InitializeRepository();
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
        public void InitializeRepository()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT PersonId, Username, Password, Email, DisplayName FROM PERSON", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["PersonId"].ToString());
                        string username = reader["Username"].ToString();
                        string password = reader["Password"].ToString();
                        string email = reader["Email"].ToString();
                        string displayName = reader["DisplayName"].ToString();

                        Person personToAdd = new Person(username, password, email, displayName);
                        personToAdd.Id = id;

                        personList.Add(personToAdd);
                    }
                }
            }
        }

        public void DeletePerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM PERSON WHERE PersonId = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = person.Id;
                int rows = cmd.ExecuteNonQuery();

                personList.Remove(person);
            }
        }

        public Person FindPerson(string mail) => personList.Find(x => x.Email == mail);
        public Person FindPerson(int id) => personList.Find(x => x.Id == id);
        public List<Person> GetPersonList() => personList;
    }
}

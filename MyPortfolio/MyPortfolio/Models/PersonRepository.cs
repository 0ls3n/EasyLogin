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
        private List<MicrosoftUser> personList;

        string? ConnectionString;

        public PersonRepository()
        {
            personList = new List<MicrosoftUser>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");

            InitializeRepository();
        }

        public void CreateNewMicrosoftPerson(MicrosoftUser person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO PERSON VALUES(@MicrosoftId, @GivenName, @Surname, @Mail, @DisplayName)", con);
                cmd.Parameters.Add("@MicrosoftId", SqlDbType.NVarChar).Value = person.id;
                cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar).Value = person.givenName;
                cmd.Parameters.Add("@Surname", SqlDbType.NVarChar).Value = person.surname;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = person.mail;
                cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = person.displayName;

                person.PortfolioId = Convert.ToInt32(cmd.ExecuteScalar());

                personList.Add(person);
            }
        }
        public void InitializeRepository()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT PersonId, MicrosoftId, GivenName, Surname, Mail, DisplayName FROM PERSON", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string microsoftId = reader["MicrosoftId"].ToString();
                        string givenName = reader["GivenName"].ToString();
                        string surname = reader["Surname"].ToString();
                        string mail = reader["Mail"].ToString();
                        string displayName = reader["DisplayName"].ToString();

                        MicrosoftUser personToAdd = new MicrosoftUser(microsoftId, givenName, surname, mail, displayName);
                        personToAdd.PortfolioId = Convert.ToInt32(reader["PersonId"].ToString());

                        personList.Add(personToAdd);
                    }
                }
            }
        }

        public void DeleteMicrosoftUser(MicrosoftUser person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM PERSON WHERE PersonId = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = person.PortfolioId;
                int rows = cmd.ExecuteNonQuery();

                personList.Remove(person);
            }
        }

        public Person FindPerson(string id) => personList.Find(x => x.id == id);
        public List<MicrosoftUser> GetPersonList() => personList;
    }
}

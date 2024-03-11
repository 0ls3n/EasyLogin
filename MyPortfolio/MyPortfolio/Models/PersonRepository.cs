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

        public void CreateNewMicrosoftPerson(MicrosoftUser person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO PERSON(MicrosoftId, Mail, DisplayName) VALUES(@MicrosoftId, @Mail, @DisplayName)", con);
                cmd.Parameters.Add("@MicrosoftId", SqlDbType.NVarChar).Value = person.id;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = person.mail;
                cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = person.displayName;

                person.PortfolioId = Convert.ToInt32(cmd.ExecuteScalar());

                personList.Add(person);
            }
        }

        public void CreateNewPortfolioPerson(PortfolioPerson person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO PERSON(Mail, DisplayName, Username, Password) VALUES(@Mail, @DisplayName, @Username, @Password)", con);
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = person.Mail;
                cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = person.DisplayName;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = person.Username;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = person.Password;

                person.PortfolioId = Convert.ToInt32(cmd.ExecuteScalar());

                personList.Add(person);
            }
        }
        public void InitializeRepository()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT PersonId, MicrosoftId, Mail, DisplayName, Username, Password FROM PERSON", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["MicrosoftId"].ToString() != string.Empty)
                        {
                            string microsoftId = reader["MicrosoftId"].ToString();
                            string mail = reader["Mail"].ToString();
                            string displayName = reader["DisplayName"].ToString();

                            MicrosoftUser personToAdd = new MicrosoftUser(microsoftId, mail, displayName);
                            personToAdd.PortfolioId = Convert.ToInt32(reader["PersonId"].ToString());

                            personList.Add(personToAdd);
                        } else
                        {
                            string mail = reader["Mail"].ToString();
                            string displayName = reader["DisplayName"].ToString();
                            string username = reader["Username"].ToString();
                            string password = reader["Password"].ToString();

                            PortfolioPerson personToAdd = new PortfolioPerson(displayName, mail, username, password);
                            personToAdd.PortfolioId = Convert.ToInt32(reader["PersonId"].ToString());

                            personList.Add(personToAdd);
                        }
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

        public MicrosoftUser FindMicrosoftUser(string id)
        {
            MicrosoftUser personToReturn = null;
            foreach (Person person in personList)
            {
                if (person is MicrosoftUser)
                {
                    if ((person as MicrosoftUser).id == id)
                    {
                        personToReturn = (person as MicrosoftUser);
                    }
                }
            }

            return personToReturn;
        }

        public PortfolioPerson FindPortfolioUser(string username)
        {
            PortfolioPerson personToReturn = null;
            foreach (Person person in personList)
            {
                if (person is PortfolioPerson)
                {
                    if ((person as PortfolioPerson).Username == username)
                    {
                        personToReturn = (person as PortfolioPerson);
                    }
                }
            }

            return personToReturn;
        }

        public List<Person> GetMicrosoftPersonList() => personList;
    }
}

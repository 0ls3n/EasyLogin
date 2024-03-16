using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MyPortfolio.Models
{
    public class PortfolioRepository
    {

        string? ConnectionString;

        private List<Portfolio> portfolios;

        PersonRepository personRepo;
        public PortfolioRepository(PersonRepository personRepo) 
        {
            this.personRepo = personRepo;
            portfolios = new List<Portfolio>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");

            Initialize();
        }

        public void CreateNewPortfolio(string title, Person person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString)) 
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO PORTFOLIO(Title, PersonId) VALUES(@Title, @PersonId)", con);

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                cmd.Parameters.Add("@PersonId", SqlDbType.Int).Value = person.PortfolioId;

                Portfolio p = new Portfolio(title, person);
                p.Id = Convert.ToInt32(cmd.ExecuteScalar());

                portfolios.Add(p);
            }
        }

        public void Initialize()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT PortfolioId, Title, PersonId FROM PORTFOLIO", con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["PortfolioId"].ToString());
                        string title = reader["Title"].ToString();
                        int PersonId = Convert.ToInt32(reader["PersonId"].ToString());

                        Portfolio p = new Portfolio(title, personRepo.FindPerson(PersonId));
                        p.Id = id;

                        portfolios.Add(p);
                    }
                }
            }
        }
    }
}

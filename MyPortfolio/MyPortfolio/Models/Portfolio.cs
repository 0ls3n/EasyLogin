using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class Portfolio
    {
        public string Title { get; set; }
        public Person Person { get; set; }
        public List<Subject> SubjectList { get; set; }
        
        public Portfolio(string title, Person person)
        {
            this.Title = title;
            this.Person = person;
            SubjectList = new List<Subject>();
        }
    }
}

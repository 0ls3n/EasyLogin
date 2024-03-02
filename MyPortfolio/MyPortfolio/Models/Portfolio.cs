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

        private List<Subject> SubjectList;
        
        public Portfolio(string title, Person person)
        {
            this.Title = title;
            this.Person = person;
            SubjectList = new List<Subject>();
        }

        public void AddSubject(Subject subject)
        {
            SubjectList.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            SubjectList.Remove(subject);
        }

        public List<Subject> GetFullList() => SubjectList; 
    }
}

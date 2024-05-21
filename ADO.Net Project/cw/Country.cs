using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cw
{
    internal class Country
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public virtual List<City> Cities { get; set; }
        public virtual List<Member> Members { get; set; }
        public Country()
        {
            Cities = new List<City>();
            Members = new List<Member>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

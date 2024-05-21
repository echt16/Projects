using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cw
{
    internal class Olympiad
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool IsSummerly { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public virtual List<SportTypeOlympiad> SportTypeOlympiads { get; set; }
        public Olympiad()
        {
            SportTypeOlympiads = new List<SportTypeOlympiad>();
        }

        public override string ToString()
        {
            return Year.ToString();
        }
    }
}

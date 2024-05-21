using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw
{
    internal class City
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public virtual List<Olympiad> Olympiads { get; set; }
        public City()
        {
            Olympiads = new List<Olympiad>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

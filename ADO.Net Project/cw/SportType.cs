using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw
{
    internal class SportType
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public virtual List<SportTypeOlympiad> SportTypeOlympiads { get; set; }
        public virtual List<Member> Members { get; set; }
        public SportType()
        {
            SportTypeOlympiads = new List<SportTypeOlympiad>();
            Members = new List<Member>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

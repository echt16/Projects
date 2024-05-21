using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cw
{
    internal class Member
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        [MinLength(1)]
        public string Surname { get; set; }
        [MinLength(1)]
        public string Patronymic { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int SportTypeId { get; set; }
        public SportType SportType { get; set; }
        public DateTime Birthday { get; set; }
        public byte[] Image { get; set; }
        public int BusyPlace { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname} {Patronymic}";
        }
    }
}

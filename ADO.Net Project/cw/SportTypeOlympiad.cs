using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw
{
    internal class SportTypeOlympiad
    {
        public int Id { get; set; }
        public int SportTypeId { get; set; }
        public SportType SportType { get; set; }
        public int OlympiadId { get; set; }
        public Olympiad Olympiad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Data
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }        
        public string Abbr { get; set; }
    }
}

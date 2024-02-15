using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.ConsoleApp.Entities
{
    public class entities
    {
    }

    [Table("tbl_Country_MA")]
    public class Country
    {
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
    }
    [Table("tbl_State_MA")]
    public class State
    {
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public int CountryId { get; set; }
    }
    [Table("tbl_City_MA")]
    public class City
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int StateId { get; set; }
    }

}

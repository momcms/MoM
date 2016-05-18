using MoM.Module.Extensions;
using MoM.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Models
{
    [Table("Country", Schema = "Core")]
    public partial class Country : IDataEntity
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ISO31661Alpha2 { get; set; }
        public string ISO31661Alpha3 { get; set; }
        public int ISO31661Numeric { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public int CurrencyCodeNumeric { get; set; }
        public string CurrencyMajor { get; set; }
        public string CurrencyMinor { get; set; }
        public int CurrencyDecimals { get; set; }
        public string CurrencyFormat { get; set; }
        public string CultureName { get; set; }
        public string CultureLanguageName { get; set; }
        public string CultureCode { get; set; }
    }
}

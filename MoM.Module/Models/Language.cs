using MoM.Module.Extensions;
using MoM.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MoM.Module.Models
{
    [Table("Language", Schema = "Core")]
    public partial class Language : IDataEntity
    {
        public virtual int LanguageId { get; set; }
        public virtual Country Country { get; set; }
    }
}

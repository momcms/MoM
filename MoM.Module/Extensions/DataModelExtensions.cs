using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoM.Module.Extensions
{
    public class BinaryLengthMaxAttribute : StringLengthAttribute
    {
        public BinaryLengthMaxAttribute() : base(int.MaxValue) { }
    }

    //public class BinaryLengthConvention : AttributePropertyConvention<StringLengthAttribute>
    //{
    //    protected override void Apply(StringLengthAttribute attribute, IPropertyInstance instance)
    //    {
    //        instance.CustomSqlType("varbinary(MAX)");
    //        instance.Length(attribute.MaximumLength);
    //    }
    //}
}

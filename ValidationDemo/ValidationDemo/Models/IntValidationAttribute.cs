using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ValidationDemo.Models
{
    public class IntValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return int.TryParse(value.ToString(), out var _);
        }
    }
}

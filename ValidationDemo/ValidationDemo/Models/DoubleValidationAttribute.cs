using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ValidationDemo.Models
{
    public class DoubleValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return double.TryParse(value.ToString(), out var _);
        }
    }
}

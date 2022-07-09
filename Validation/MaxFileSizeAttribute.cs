using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MovieMVC.Validation
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;

            ErrorMessage = $"Maximum allowed file size is { _maxFileSize} MB.";
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is IFormFile file) && (file.Length > (_maxFileSize * 1024 * 1024)))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        
    }
}

namespace GameZone.Attributes
{
    public class MaximumFileSizeAttribute:ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaximumFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {

                if ((file.Length)/(1024*1024) > _maxFileSize)
                {
                    return new ValidationResult($"Maximum File Size is {FileSettings.MaxFileSizeInMB}MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}


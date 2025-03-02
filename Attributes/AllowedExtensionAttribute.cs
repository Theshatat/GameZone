namespace GameZone.Attributes
{
    public class AllowedExtensionAttribute: ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensionAttribute(string allowedExtensions) {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult IsValid(object? value,ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtensions.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase);

                if(!isAllowed)
                {
                    return new ValidationResult($"Only these extensions are allowed: {_allowedExtensions}");
                }
            }
            return ValidationResult.Success;
        }
    }
}

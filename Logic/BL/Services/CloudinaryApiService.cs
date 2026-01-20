using CloudinaryDotNet;
using System;

namespace CloudinaryDam.Logic.BL.Services
{
    public interface ICloudinaryApiService
    {
        string ValidateCredentials(CloudinarySettingsDto settings);
    }
    public class CloudinaryApiService : ICloudinaryApiService
    {
        public CloudinaryApiService() { }

        public string ValidateCredentials(CloudinarySettingsDto settings)
        {
            var errorMessage = string.Empty;
            try
            {
                var cloudinary = new Cloudinary(new Account
                {
                    Cloud = settings.CloudName,
                    ApiKey = settings.ApiKey,
                    ApiSecret = settings.ApiSecret,
                });
                cloudinary.Api.Secure = true;
                var validationResults = cloudinary.Ping();
                if (!string.IsNullOrEmpty(validationResults?.Error?.Message))
                {
                    errorMessage = $"Credentials are not valid: {validationResults.Error.Message}";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Credentials are not valid: {ex.Message}";
            }
            return errorMessage;
        }
    }
    public record CloudinarySettingsDto(
        string CloudName
        , string ApiKey
        , string ApiSecret
    );
}

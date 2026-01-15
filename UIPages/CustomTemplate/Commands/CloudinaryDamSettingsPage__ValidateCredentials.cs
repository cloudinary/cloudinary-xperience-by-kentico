using CloudinaryDam.Logic.BL.Services;
using Kentico.Xperience.Admin.Base;
using System.Threading.Tasks;

namespace CloudinaryDam
{
    internal partial class CloudinaryDamSettingsPage
    {
        [PageCommand]
        public async Task<ICommandResponse<ValidateCredentialsCommandResponse>> ValidateCredentials(ValidateCredentialsCommandData data)
        {
            var errorMessage = cloudinaryApiService.ValidateCredentials(new CloudinarySettingsDto(data.CloudName, data.ApiKey, data.ApiSecret));
            var response = new ValidateCredentialsCommandResponse(errorMessage);
            if (!response.IsValid)
            {
                return ResponseFrom(response).AddErrorMessage(response.Error);
            }
            return ResponseFrom(response).AddMessage("Credentials are valid.", CommandResponseMessageLevel.Success);
        }
    }

    internal record ValidateCredentialsCommandResponse(string Error)
    {
        public bool IsValid => string.IsNullOrEmpty(Error);
    }

    public class ValidateCredentialsCommandData
    {
        public ValidateCredentialsCommandData(string cloudName, string apiKey, string apiSecret)
        {
            CloudName = cloudName;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }

        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}

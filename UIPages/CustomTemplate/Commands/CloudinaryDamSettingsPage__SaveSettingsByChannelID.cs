using CloudinaryDam.Logic.BL.Services;
using CloudinaryDam.Logic.DAL.InfoObjects;
using Kentico.Xperience.Admin.Base;
using System.Threading.Tasks;

namespace CloudinaryDam
{
    internal partial class CloudinaryDamSettingsPage
    {
        [PageCommand]
        public async Task<ICommandResponse<SaveSettingsByChannelIDCommandResponse>> SaveSettingsByChannelID(SaveSettingsByChannelIDCommandData data)
        {
            // validate
            var errorMessage = cloudinaryApiService.ValidateCredentials(new CloudinarySettingsDto(data.CloudName, data.ApiKey, data.ApiSecret));
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return ResponseFrom(new SaveSettingsByChannelIDCommandResponse(errorMessage)).AddMessage(errorMessage, CommandResponseMessageLevel.Error);
            }

            cloudinaryDamSettingsPageRepository.UpsertSettings(data.ToInfoObject());

            return ResponseFrom(new SaveSettingsByChannelIDCommandResponse(errorMessage)).AddMessage("Settings was saved.", CommandResponseMessageLevel.Success);
        }
    }

    internal record SaveSettingsByChannelIDCommandResponse(string Error)
    {
        public bool IsValid => string.IsNullOrEmpty(Error);
    }

    public class SaveSettingsByChannelIDCommandData
    {
        public SaveSettingsByChannelIDCommandData(int websiteChannelID, string cloudName, string apiKey, string apiSecret, bool useSaml, bool useIframeSupportForSaml, string defaultTransformations)
        {
            WebsiteChannelID = websiteChannelID;
            CloudName = cloudName;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            UseSaml = useSaml;
            UseIframeSupportForSaml = useIframeSupportForSaml;
            DefaultTransformations = defaultTransformations;
        }
        public int WebsiteChannelID { get; set; }
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public bool UseSaml { get; set; }
        public bool UseIframeSupportForSaml { get; set; }
        public string DefaultTransformations { get; set; }

        public CloudinaryDamSettingsInfo ToInfoObject()
        {
            return new CloudinaryDamSettingsInfo
            {
                WebsiteChannelID = WebsiteChannelID,
                CloudName = CloudName,
                ApiKey = ApiKey,
                ApiSecret = ApiSecret,
                UseSaml = UseSaml,
                UseIframeSupportForSaml = UseIframeSupportForSaml,
                DefaultTransformations = DefaultTransformations,
            };
        }
    }
}

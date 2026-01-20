using Kentico.Xperience.Admin.Base;
using System.Threading.Tasks;

namespace CloudinaryDam
{
    internal partial class CloudinaryDamSettingsPage
    {
        [PageCommand]
        public async Task<ICommandResponse<GetSettingsByChannelIdCommandResponse>> GetSettingsByChannelID(GetSettingsByChannelIdCommandData data)
        {
            var cloudinaryDamSettings = cloudinaryDamSettingsPageRepository.GetSettingsByChannelID(data.WebsiteChannelID);

            return ResponseFrom(new GetSettingsByChannelIdCommandResponse(
                cloudinaryDamSettings?.ApiKey ?? string.Empty,
                cloudinaryDamSettings?.ApiSecret ?? string.Empty,
                cloudinaryDamSettings?.CloudName ?? string.Empty,
                cloudinaryDamSettings?.UseSaml ?? false,
                cloudinaryDamSettings?.UseIframeSupportForSaml ?? false,
                cloudinaryDamSettings?.DefaultTransformations ?? string.Empty
            ));
        }
    }

    internal record GetSettingsByChannelIdCommandResponse(string ApiKey, string ApiSecret, string CloudName, bool UseSaml, bool UseIframeSupportForSaml, string DefaultTransformations);

    public class GetSettingsByChannelIdCommandData
    {
        public GetSettingsByChannelIdCommandData(int websiteChannelID)
        {
            WebsiteChannelID = websiteChannelID;
        }
        public int WebsiteChannelID { get; set; }
    }
}

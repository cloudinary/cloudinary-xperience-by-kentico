using CloudinaryDam;
using CloudinaryDam.Logic.BL.Services;
using CloudinaryDam.Logic.DAL.Repositories;
using Kentico.Xperience.Admin.Base;
using System.Threading.Tasks;

[assembly: UIApplication("Cloudinary.DamSettingsTemplate", typeof(CloudinaryDamSettingsPage), "CloudinaryDamSettings", "Cloudinary DAM Settings", CloudinaryDamModule.CUSTOM_CATEGORY, Icons.Cloud, "@cloudinary/dam/CloudinaryDamSettings")]

namespace CloudinaryDam
{
    internal partial class CloudinaryDamSettingsPage(
        ICloudinaryDamSettingsPageRepository cloudinaryDamSettingsPageRepository,
        ICloudinaryApiService cloudinaryApiService)
        : Page<CloudinaryDamSettingsPageProperties>
    {
        public override Task<CloudinaryDamSettingsPageProperties> ConfigureTemplateProperties(CloudinaryDamSettingsPageProperties properties)
        {
            properties.WebsiteChannels = cloudinaryDamSettingsPageRepository.GetWebsiteChannels();
            return Task.FromResult(properties);
        }
    }

    public class CloudinaryDamSettingsPageProperties : TemplateClientProperties
    {
        public WebsiteChannel[] WebsiteChannels { get; set; }
    }
}

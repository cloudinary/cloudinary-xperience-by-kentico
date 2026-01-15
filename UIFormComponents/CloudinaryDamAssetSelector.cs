using CloudinaryDam.Logic.BL.Services;
using CloudinaryDam.Logic.DAL.InfoObjects;
using CloudinaryDam.Logic.DAL.Repositories;
using CMS.Membership;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Kentico.Xperience.Admin.Websites.Forms;
using System.Threading.Tasks;

namespace CloudinaryDam.UIFormComponents
{
    [ComponentAttribute(typeof(CloudinaryDamAssetSelectorComponentAttribute))]
    public class CloudinaryDamAssetSelectorComponent(
        ICloudinaryDamSettingsPageRepository cloudinaryDamSettingsPageRepository,
        ICloudinaryApiService cloudinaryApiService)
        : FormComponent<CloudinaryDamAssetSelectorComponentProperties, string>
    {
        public override string ClientComponentName => "@cloudinary/dam/AssetSelector";
        private IWebPageFormContext PageFormContext => (IWebPageFormContext)FormContext;
        protected override Task ConfigureClientProperties(CloudinaryDamAssetSelectorComponentProperties clientProperties)
        {
            base.ConfigureClientProperties(clientProperties);

            var settings = cloudinaryDamSettingsPageRepository.GetSettingsByChannelID(PageFormContext.WebsiteChannelId);
            if (settings == null)
            {
                clientProperties.ErrorMessage = "Cloudinary DAM is not configured. Please add Cloudinary settings for the current website channel.";
                return Task.CompletedTask;
            }

            var validationResult = cloudinaryApiService.ValidateCredentials(new CloudinarySettingsDto(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret));
            if (!string.IsNullOrEmpty(validationResult))
            {
                clientProperties.ErrorMessage = $"Cloudinary DAM settings are invalid: {validationResult}";
                return Task.CompletedTask;
            }

            clientProperties.InitFromInfoObject(settings);

            if (settings.UseSaml)
            {
                clientProperties.UserName = MembershipContext.AuthenticatedUser.Email;
            }

            return Task.CompletedTask;
        }
    }


    public class CloudinaryDamAssetSelectorComponentProperties : FormComponentClientProperties<string>
    {
        public string ErrorMessage { get; set; }

        public string CloudName { get; private set; }

        public string ApiKey { get; private set; }

        public bool UseSaml { get; set; }

        public bool UseIframeSupportForSaml { get; set; }

        public string DefaultTransformations { get; set; }

        public string UserName { get; set; }

        public void InitFromInfoObject(CloudinaryDamSettingsInfo infoObject)
        {
            CloudName = infoObject.CloudName;
            ApiKey = infoObject.ApiKey;
            UseSaml = infoObject.UseSaml;
            UseIframeSupportForSaml = infoObject.UseIframeSupportForSaml;
            DefaultTransformations = infoObject.DefaultTransformations;
        }
    }

    // The component doesn't define any custom configuration properties.
    // The default attribute definition is therefore sufficient.
    public class CloudinaryDamAssetSelectorComponentAttribute : FormComponentAttribute
    {
    }
}

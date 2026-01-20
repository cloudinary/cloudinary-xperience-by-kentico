using CloudinaryDam;
using CloudinaryDam.Logic.BL.Services;
using CloudinaryDam.Logic.DAL;
using CloudinaryDam.Logic.DAL.InfoObjects;
using CloudinaryDam.Logic.DAL.Repositories;
using CloudinaryDam.UIFormComponents;
using CMS.Base;
using CMS.Core;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: CMS.AssemblyDiscoverable]
[assembly: CMS.RegisterModule(typeof(CloudinaryDamModule))]

[assembly: UICategory(CloudinaryDamModule.CUSTOM_CATEGORY, "Cloudinary DAM", Icons.CustomElement, 100)]

[assembly: RegisterFormComponent("Cloudinary.Dam.AssetSelector", typeof(CloudinaryDamAssetSelectorComponent), "Cloudinary Asset Selector")]

namespace CloudinaryDam
{
    public class CloudinaryDamModule : AdminModule
    {
        public const string CUSTOM_CATEGORY = "cloudinary.dam.settings";

        public CloudinaryDamModule()
            : base("cloudinary.dam")
        {
        }

        protected override void OnInit(ModuleInitParameters parameters)
        {
            base.OnInit();

            // Makes the module accessible to the admin UI
            RegisterClientModule("cloudinary", "dam");

            var configuration = parameters.Services.GetRequiredService<IConfiguration>();
            var cloudinaryDamSettingsInfoRegistration = parameters.Services.GetRequiredService<CloudinaryDamSettingsInfoInstaller>();
            var databaseUpdater = parameters.Services.GetRequiredService<DatabaseUpdater>();

            ApplicationEvents.Initialized.Execute += (sender, eventAgr) =>
            {
                databaseUpdater.Update();
                cloudinaryDamSettingsInfoRegistration.Install();
            };
        }

        protected override void OnPreInit(ModulePreInitParameters parameters)
        {
            parameters.Services.AddScoped<CloudinaryDamSettingsInfoProvider>();
            parameters.Services.AddScoped<ICloudinaryApiService, CloudinaryApiService>();
            parameters.Services.AddScoped<ICloudinaryDamSettingsPageRepository, CloudinaryDamSettingsPageRepository>();
            parameters.Services.AddScoped<IEncryptionService, CloudinaryDamEncryptionService>();
            parameters.Services.AddSingleton<CloudinaryDamSettingsInfoInstaller>();
            parameters.Services.AddSingleton<DatabaseUpdater>();
        }
    }
}

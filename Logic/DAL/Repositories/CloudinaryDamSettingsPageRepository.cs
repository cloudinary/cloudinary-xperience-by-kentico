using CloudinaryDam.Logic.BL.Services;
using CloudinaryDam.Logic.DAL.InfoObjects;
using CMS.ContentEngine;
using CMS.DataEngine;
using CMS.Websites;
using System.Linq;

namespace CloudinaryDam.Logic.DAL.Repositories
{
    public interface ICloudinaryDamSettingsPageRepository
    {
        WebsiteChannel[] GetWebsiteChannels();
        CloudinaryDamSettingsInfo GetSettingsByChannelID(int websiteChannelID);
        void UpsertSettings(CloudinaryDamSettingsInfo settings);

    }

    public class CloudinaryDamSettingsPageRepository(
        IInfoProvider<ChannelInfo> channelInfoProvider,
        IInfoProvider<WebsiteChannelInfo> websiteChannelsProvider,
        CloudinaryDamSettingsInfoProvider cloudinaryDamSettingsProvider,
        IEncryptionService encryptionService) : ICloudinaryDamSettingsPageRepository
    {
        public CloudinaryDamSettingsInfo GetSettingsByChannelID(int websiteChannelID)
        {
            var entity = cloudinaryDamSettingsProvider
                .Get()
                .Where(settings => settings.WebsiteChannelID == websiteChannelID)
                .FirstOrDefault();

            if (entity != null)
            {
                entity.ApiKey = encryptionService.Decrypt(entity.ApiKey);
                entity.ApiSecret = encryptionService.Decrypt(entity.ApiSecret);
            }

            return entity;
        }

        public WebsiteChannel[] GetWebsiteChannels()
        {
            return websiteChannelsProvider
                .Get()
                .Join(
                    channelInfoProvider.Get(),
                    websiteChannel => websiteChannel.WebsiteChannelChannelID,
                    channel => channel.ChannelID,
                    (websiteChannel, channel) => new { websiteChannel, channel }
                )
                .ToList()
                .Select(infos => new WebsiteChannel
                {
                    WebsiteChannelID = infos.websiteChannel.WebsiteChannelID,
                    WebsiteChannelName = infos.channel.ChannelDisplayName,
                })
                .ToArray();
        }

        public void UpsertSettings(CloudinaryDamSettingsInfo settings)
        {
            // get existing
            var existedSettingsEntity = cloudinaryDamSettingsProvider
                .Get()
                .Where(nameof(CloudinaryDamSettingsInfo.WebsiteChannelID), QueryOperator.Equals, settings.WebsiteChannelID)
                .FirstOrDefault();
            // insert
            if (existedSettingsEntity == null)
            {
                var newSettingsEntity = new CloudinaryDamSettingsInfo
                {
                    WebsiteChannelID = settings.WebsiteChannelID,
                    CloudName = settings.CloudName,
                    ApiKey = encryptionService.Encrypt(settings.ApiKey),
                    ApiSecret = encryptionService.Encrypt(settings.ApiSecret),
                    UseSaml = settings.UseSaml,
                    UseIframeSupportForSaml = settings.UseIframeSupportForSaml,
                    DefaultTransformations = settings.DefaultTransformations,
                };
                cloudinaryDamSettingsProvider.Set(newSettingsEntity);
            }
            // update
            else
            {
                existedSettingsEntity.CloudName = settings.CloudName;
                existedSettingsEntity.ApiKey = encryptionService.Encrypt(settings.ApiKey);
                existedSettingsEntity.ApiSecret = encryptionService.Encrypt(settings.ApiSecret);
                existedSettingsEntity.UseSaml = settings.UseSaml;
                existedSettingsEntity.UseIframeSupportForSaml = settings.UseIframeSupportForSaml;
                existedSettingsEntity.DefaultTransformations = settings.DefaultTransformations;
                cloudinaryDamSettingsProvider.Set(existedSettingsEntity);
            }
        }
    }

    public class WebsiteChannel
    {
        public int WebsiteChannelID { get; set; }
        public string WebsiteChannelName { get; set; }
    }
}

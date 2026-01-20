using CloudinaryDam.Logic.DAL.InfoObjects;
using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using System;
using System.Data;

[assembly: RegisterObjectType(typeof(CloudinaryDamSettingsInfo), CloudinaryDamSettingsInfo.OBJECT_TYPE)]

namespace CloudinaryDam.Logic.DAL.InfoObjects
{
    public partial class CloudinaryDamSettingsInfo : AbstractInfo<CloudinaryDamSettingsInfo, CloudinaryDamSettingsInfoProvider>, IInfoWithId, IInfoWithGuid
    {
        public const string OBJECT_TYPE = "cloudinary.damsettings";
        public const string CLASS_NAME = "Cloudinary.DamSettings";

        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(CloudinaryDamSettingsInfoProvider), OBJECT_TYPE, CLASS_NAME, "SettingsID", null, "SettingsGUID", null, null, null, null, null)
        {
            TouchCacheDependencies = true,
        };

        [DatabaseField]
        public virtual int SettingsID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(SettingsID)), 0);
            set => SetValue(nameof(SettingsID), value);
        }

        [DatabaseField]
        public virtual Guid SettingsGUID
        {
            get => ValidationHelper.GetGuid(GetValue(nameof(SettingsGUID)), Guid.Empty);
            set => SetValue(nameof(SettingsGUID), value);
        }

        [DatabaseField]
        public virtual int WebsiteChannelID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(WebsiteChannelID)), 0);
            set => SetValue(nameof(WebsiteChannelID), value);
        }

        [DatabaseField]
        public virtual string CloudName
        {
            get => ValidationHelper.GetString(GetValue(nameof(CloudName)), String.Empty);
            set => SetValue(nameof(CloudName), value);
        }

        [DatabaseField]
        public virtual string ApiKey
        {
            get => ValidationHelper.GetString(GetValue(nameof(ApiKey)), String.Empty);
            set => SetValue(nameof(ApiKey), value);
        }

        [DatabaseField]
        public virtual string ApiSecret
        {
            get => ValidationHelper.GetString(GetValue(nameof(ApiSecret)), String.Empty);
            set => SetValue(nameof(ApiSecret), value);
        }

        [DatabaseField]
        public virtual bool UseSaml
        {
            get => ValidationHelper.GetBoolean(GetValue(nameof(UseSaml)), false);
            set => SetValue(nameof(UseSaml), value);
        }

        [DatabaseField]
        public virtual bool UseIframeSupportForSaml
        {
            get => ValidationHelper.GetBoolean(GetValue(nameof(UseIframeSupportForSaml)), false);
            set => SetValue(nameof(UseIframeSupportForSaml), value);
        }

        [DatabaseField]
        public virtual string DefaultTransformations
        {
            get => ValidationHelper.GetString(GetValue(nameof(DefaultTransformations)), String.Empty);
            set => SetValue(nameof(DefaultTransformations), value);
        }

        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }

        protected override void SetObject()
        {
            Provider.Set(this);
        }

        public CloudinaryDamSettingsInfo()
            : base(TYPEINFO)
        {
        }

        public CloudinaryDamSettingsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }

    public class CloudinaryDamSettingsInfoProvider : AbstractInfoProvider<CloudinaryDamSettingsInfo, CloudinaryDamSettingsInfoProvider>
    {
        public CloudinaryDamSettingsInfoProvider()
            : base(CloudinaryDamSettingsInfo.TYPEINFO)
        {
        }
    }
}
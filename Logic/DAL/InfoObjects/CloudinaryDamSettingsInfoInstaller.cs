using CMS.Core;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.Modules;

namespace CloudinaryDam.Logic.DAL.InfoObjects
{
    public class CloudinaryDamSettingsInfoInstaller(IInfoProvider<ResourceInfo> resourceInfoProvider, IEventLogService eventLogService)
    {
        private readonly IInfoProvider<ResourceInfo> _resourceInfoProvider = resourceInfoProvider;
        private readonly IEventLogService _eventLogService = eventLogService;

        private readonly static string RESOURCE_NAME = "Cloudinary.Dam.Settings.Resource";
        public void Install()
        {
            var resource = _resourceInfoProvider.Get(RESOURCE_NAME) ?? new ResourceInfo();
            InitializeCloudinaryDamResource(resource);
            InitializeCloudinaryDamSettingsInfo(resource);
        }

        private void InitializeCloudinaryDamResource(ResourceInfo resource)
        {
            resource.ResourceDisplayName = RESOURCE_NAME;
            resource.ResourceName = RESOURCE_NAME;
            resource.ResourceDescription = "Stores setting for the Cloudinary DAM module.";
            resource.ResourceIsInDevelopment = false;

            if (resource.HasChanged)
            {
                _resourceInfoProvider.Set(resource);
            }
        }

        private void InitializeCloudinaryDamSettingsInfo(ResourceInfo resource)
        {
            var info = DataClassInfoProvider.GetDataClassInfo(CloudinaryDamSettingsInfo.OBJECT_TYPE) ?? DataClassInfo.New(CloudinaryDamSettingsInfo.OBJECT_TYPE);
            var formInfo = info.ClassID > 0 ? new FormInfo(info.ClassFormDefinition) : FormHelper.GetBasicFormDefinition(nameof(CloudinaryDamSettingsInfo.SettingsID));

            info.ClassName = CloudinaryDamSettingsInfo.CLASS_NAME;
            info.ClassTableName = "Cloudinary_DamSettings";
            info.ClassDisplayName = "Cloudinary DAM Settings";
            info.ClassType = ClassType.OTHER;
            info.ClassResourceID = resource.ResourceID;

            var formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.SettingsGUID));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.DataType = "guid";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.WebsiteChannelID));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.DataType = "integer";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.CloudName));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.Size = 1024;
            formItem.DataType = "text";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.ApiKey));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.Size = 1024;
            formItem.DataType = "text";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.ApiSecret));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.Size = 1024;
            formItem.DataType = "text";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.UseSaml));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.DataType = "boolean";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.UseIframeSupportForSaml));
            formItem.AllowEmpty = false;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.DataType = "boolean";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            formItem = GetFormField(formInfo, nameof(CloudinaryDamSettingsInfo.DefaultTransformations));
            formItem.AllowEmpty = true;
            formItem.Visible = true;
            formItem.Precision = 0;
            formItem.DataType = "text";
            formItem.Enabled = true;
            SetFormField(formInfo, formItem);

            info.ClassFormDefinition = formInfo.GetXmlDefinition();

            if (info.HasChanged)
            {
                DataClassInfoProvider.SetDataClassInfo(info);
            }
        }

        private static FormFieldInfo GetFormField(FormInfo formInfo, string fieldName)
        {
            return formInfo.FieldExists(fieldName) ? formInfo.GetFormField(fieldName) : new FormFieldInfo() { Name = fieldName };
        }

        private static void SetFormField(FormInfo formInfo, FormFieldInfo field)
        {
            if (formInfo.FieldExists(field.Name))
            {
                formInfo.UpdateFormField(field.Name, field);
            }
            else
            {
                formInfo.AddFormItem(field);
            }
        }
    }
}
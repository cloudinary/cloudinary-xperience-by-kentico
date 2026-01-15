# 1. Prerequisites
The integration was developed for the headless CMS Xperience by Kentico, version `30.10.1`.
Expected software environment
* MS SQL Server as the database engine
* React.js 18.3.1
* ASP.NET Core 8

# 2. Project's build actions
## 2.1.`pre-build-event.ps1` script
It builds react js code into js bundle for the further embedding in the output DLL.

*KNOWN ISSUE:* This script can fail with the error `CSC : error CS1566: Error reading resource ...`. This happens due to dynamic js bundle name. The msbuild enumerates all resources before the pre-build event is executed, so it cannot find the dynamically named resource. The webpack forms name for bundle dynamically based on the file hash-sum.

*WHAT WAS TRIED:*
* static bundle name - the CMS runtime error
* different 'after' targets variations - error appears anyway

*WORKAROUND:* run re-build after the first failed build - the second build will succeed as the resource name won't change.

## 2.2. `post-build-event.ps1` script
The main purpose of this script is to install the NuGet package in the CMS and re-run the CMS.

The script relies on the following environment variables:
`XbyKenticoCmsCsprojPath` - path to the csproj file of the CMS project
`XbyKenticoCmsProcessName` - name of the CMS process (without .exe)

# 3. Database schema changes
The integration uses iterative migrations. If you want to apply the migrations manually, you can find them in the `Database\Migrations` folder.
You can include the migration in the custom database updater (see `DatabaseUpdater` class) so it will be applied automatically on the application start.

# 4. Configuration
The integration requires configuration in the `appsettings.json` file.
```
{
  "CloudinaryDam:EncryptionKey": "Your encryption key"
}
```

# 5. Deployment
The integration is deployed as a NuGet package. You can find the package in the `.\artifacts` folder after the build.
To install the package, you can use the following command in the CLI:
```
# Use full path to '.\artifacts' folder
dotnet add package XperienceByKenticoCloudinaryDAM -v 1.0.1-dev.8a3ce443 -s ".\artifacts"
```

# 6. CMS usage
In the widget properties class, add the following member:

```
[CloudinaryDam.UIFormComponents.CloudinaryDamAssetSelectorComponent(Label = "Cloudinary Asset")]
public string CloudinaryAsset { get; set; }
```

After this, the Cloudinary Asset Selector will be available in the CMS admin UI.

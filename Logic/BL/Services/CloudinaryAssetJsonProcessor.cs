using CloudinaryDam.Logic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace CloudinaryDam
{
    public static class CloudinaryAssetJsonProcessor
    {
        public static string GetAssetUrlFromJson(this string assetsJson)
        {
            var assets = ParseAssetsMetadata(assetsJson);
            if (!assets.Any())
            {
                return string.Empty;
            }
            return assets[0]?.derived?.LastOrDefault()?.secure_url
                ?? assets[0]?.secure_url
                ?? string.Empty;
        }

        public static CloudinaryAssetMetadataFromMediaLibraryWidget GetAssetMetadataFromMediaLibraryWidget(this string assetsJson)
        {
            var assets = ParseAssetsMetadata(assetsJson);
            if (!assets.Any())
            {
                return null;
            }
            return assets[0];
        }

        private static CloudinaryAssetMetadataFromMediaLibraryWidget[] ParseAssetsMetadata(string assetsJson)
        {
            JObject parsedAssets = null;
            try
            {
                parsedAssets = JObject.Parse(assetsJson);
            }
            catch { }

            if (parsedAssets != null && parsedAssets.ContainsKey("assets"))
            {
                return JsonConvert.DeserializeObject<CloudinaryAssetMetadataFromMediaLibraryWidget[]>(parsedAssets["assets"].ToString());
            }
            return Array.Empty<CloudinaryAssetMetadataFromMediaLibraryWidget>();
        }
    }
}

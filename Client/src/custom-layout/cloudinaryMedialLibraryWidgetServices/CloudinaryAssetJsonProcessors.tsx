export function GetPreviewUrlFromAssetJson(json: any): string {
  var firstAsset = json?.assets ? json?.assets[0] : null;
  if (!firstAsset) {
    return "";
  }

  if (Array.isArray(firstAsset.derived) && firstAsset.derived.length > 0) {
    return firstAsset.derived[firstAsset.derived.length - 1].secure_url;
  }

  return firstAsset.secure_url;
}

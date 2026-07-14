import { GetSettingsByChannelIdCommandResponse } from "./GetSettingsByChannelIdCommandResponse";

export class WebsiteChannelCloudinaryDamSettings {
  CloudName: string;
  ApiKey: string;
  ApiSecret: string;
  UseSaml: boolean;
  UseIframeSupportForSaml: boolean;
  DefaultTransformations: string;

  constructor(data?: Partial<WebsiteChannelCloudinaryDamSettings>) {
    this.CloudName = data?.CloudName ?? "";
    this.ApiKey = data?.ApiKey ?? "";
    this.ApiSecret = data?.ApiSecret ?? "";
    this.UseSaml = data?.UseSaml ?? false;
    this.UseIframeSupportForSaml = data?.UseIframeSupportForSaml ?? false;
    this.DefaultTransformations = data?.DefaultTransformations ?? "";
  }

  validate(): string[] {
    const errors: string[] = [];
    if (this.CloudName.length === 0 || this.CloudName.length > 1024) {
      errors.push("'Cloud Name' must be 1-1024 characters");
    }
    if (this.ApiKey.length === 0 || this.ApiKey.length > 1024) {
      errors.push("'Api Key' must be 1-1024 characters");
    }
    if (this.ApiSecret.length === 0 || this.ApiSecret.length > 1024) {
      errors.push("'Api Secret' must be 1-1024 characters");
    }
    if (this.DefaultTransformations && this.DefaultTransformations.trim() !== "") {
      try {
        JSON.parse(this.DefaultTransformations);
      } catch {
        errors.push("Wrong format for the 'Default Transformations'. The value should be parsable JSON object.");
      }
    }
    return errors;
  }

  static empty(): WebsiteChannelCloudinaryDamSettings {
    return new WebsiteChannelCloudinaryDamSettings();
  }

  static createFrom_GetSettingsByChannelIdCommandResponse(
    data?: GetSettingsByChannelIdCommandResponse
  ): WebsiteChannelCloudinaryDamSettings {
    return new WebsiteChannelCloudinaryDamSettings({
      CloudName: data?.cloudName ?? "",
      ApiKey: data?.apiKey ?? "",
      ApiSecret: data?.apiSecret ?? "",
      UseSaml: data?.useSaml ?? false,
      UseIframeSupportForSaml: data?.useIframeSupportForSaml ?? false,
      DefaultTransformations: data?.defaultTransformations ?? "",
    });
  }
}

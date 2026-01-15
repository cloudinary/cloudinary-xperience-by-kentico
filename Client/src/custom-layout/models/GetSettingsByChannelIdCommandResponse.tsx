export interface GetSettingsByChannelIdCommandResponse {
  apiKey: string;
  apiSecret: string;
  cloudName: string;
  useSaml: boolean;
  useIframeSupportForSaml: boolean;
  defaultTransformations: string;
}

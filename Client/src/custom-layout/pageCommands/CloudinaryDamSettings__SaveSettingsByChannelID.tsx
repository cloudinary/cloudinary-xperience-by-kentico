import { usePageCommand } from "@kentico/xperience-admin-base";

interface SaveSettingsByChannelIDCommandData {
  websiteChannelID: number;
  cloudName: string;
  apiKey: string;
  apiSecret: string;
  useSaml: boolean;
  useIframeSupportForSaml: boolean;
  defaultTransformations: string;
}

interface SaveSettingsByChannelIDCommandResponse {
  error: string;
  isValid: boolean;
}

export function useSaveSettingsByChannelID(
  after?: (response: SaveSettingsByChannelIDCommandResponse) => void
) {
  const { execute } = usePageCommand<
    SaveSettingsByChannelIDCommandResponse,
    SaveSettingsByChannelIDCommandData
  >("SaveSettingsByChannelID", {
    after: (response) => {
      if (after) {
        after(response!);
      }
    },
  });

  return { execute };
}

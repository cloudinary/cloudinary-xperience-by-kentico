import { usePageCommand } from "@kentico/xperience-admin-base";
import { GetSettingsByChannelIdCommandResponse } from "../models/GetSettingsByChannelIdCommandResponse";

interface GetSettingsByChannelIdCommandData {
  websiteChannelID: number;
}

export function useGetSettingsByChannelID(
  after?: (response: GetSettingsByChannelIdCommandResponse) => void
) {
  const { execute } = usePageCommand<
    GetSettingsByChannelIdCommandResponse,
    GetSettingsByChannelIdCommandData
  >("GetSettingsByChannelID", {
    after: (response) => {
      if (after) {
        after(response!);
      }
    },
  });

  return { execute };
}

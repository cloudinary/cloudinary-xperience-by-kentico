import { FormComponentProps } from "@kentico/xperience-admin-base";

export interface CloudinaryDamSettingProps extends FormComponentProps {
  readonly label: string;
  readonly websiteChannels: WebsiteChannel[];
}

interface WebsiteChannel {
  websiteChannelID: number;
  websiteChannelName: string;
}

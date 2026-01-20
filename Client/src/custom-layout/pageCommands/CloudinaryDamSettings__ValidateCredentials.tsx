import { usePageCommand } from "@kentico/xperience-admin-base";

interface ValidateCredentialsCommandData {
  cloudName: string;
  apiKey: string;
  apiSecret: string;
}

interface ValidateCredentialsCommandResponse {
  error: string;
  isValid: boolean;
}

export function useValidateCredentials(
  after?: (response: ValidateCredentialsCommandResponse) => void
) {
  const { execute } = usePageCommand<
    ValidateCredentialsCommandResponse,
    ValidateCredentialsCommandData
  >("ValidateCredentials", {
    after: (response) => {
      if (after) {
        after(response!);
      }
    },
  });

  return { execute };
}

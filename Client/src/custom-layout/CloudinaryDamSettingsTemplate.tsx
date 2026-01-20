import React, { useState, useEffect } from "react";
import {
  Button,
  ButtonSize,
  Input,
  Select,
  MenuItem,
  Checkbox,
  FormItemWrapper,
  Box,
  Spacing,
  TextArea,
} from "@kentico/xperience-admin-components";

import { WebsiteChannelCloudinaryDamSettings } from "./models/WebsiteChannelCloudinaryDamSettings";
import { PasswordField } from "./componemts/PasswordComponent";
import { useValidateCredentials } from "./pageCommands/CloudinaryDamSettings__ValidateCredentials";
import { useGetSettingsByChannelID } from "./pageCommands/CloudinaryDamSettings__GetSettingsByChannelID";
import { useSaveSettingsByChannelID } from "./pageCommands/CloudinaryDamSettings__SaveSettingsByChannelID";
import { CloudinaryDamSettingProps } from "./models/CloudinaryDamSettingProps";

export const CloudinaryDamSettingsTemplate = (
  props: CloudinaryDamSettingProps
) => {
  const [isEnabled, setIsEnabled] = useState(false);
  const [validationErrors, setValidationErrors] = useState<string[]>([]);
  const [currentWebsiteChannel, setCurrentWebsiteChannel] = useState(0);
  const [currentSettings, setCurrentSettings] =
    useState<WebsiteChannelCloudinaryDamSettings>(
      WebsiteChannelCloudinaryDamSettings.empty()
    );

  const { execute: getSettings_PageCommand } = useGetSettingsByChannelID(
    (response) => {
      setCurrentSettings(
        WebsiteChannelCloudinaryDamSettings.createFrom_GetSettingsByChannelIdCommandResponse(
          response
        )
      );
    }
  );

  const { execute: validateCredentials_PageCommand } = useValidateCredentials(
    (response) => {
      setValidationErrors([response.error]);
    }
  );

  const { execute: saveSettings_PageCommand } = useSaveSettingsByChannelID(
    (response) => {
      setValidationErrors([response.error]);
    }
  );

  const loadSettingsByWebsiteChannelID_OnChangeHandler = (
    websiteChannelID: string | undefined
  ) => {
    if (websiteChannelID) {
      const parsedWebsiteChannelID = parseInt(websiteChannelID, 10);
      setCurrentWebsiteChannel(parsedWebsiteChannelID);
      getSettings_PageCommand({
        websiteChannelID: parsedWebsiteChannelID,
      });
      setIsEnabled(true);
    } else {
      setIsEnabled(false);
      setCurrentSettings(WebsiteChannelCloudinaryDamSettings.empty());
    }
  };

  const saveSettings_ClickHandler = () => {
    var validationErrors = currentSettings.validate();
    if (validationErrors.length > 0) {
      setValidationErrors(validationErrors);
      return;
    }
    saveSettings_PageCommand({
      websiteChannelID: currentWebsiteChannel,
      cloudName: currentSettings.CloudName,
      apiKey: currentSettings.ApiKey,
      apiSecret: currentSettings.ApiSecret,
      useSaml: currentSettings.UseSaml,
      useIframeSupportForSaml: currentSettings.UseIframeSupportForSaml,
      defaultTransformations: currentSettings.DefaultTransformations,
    });
  };

  const validateCredentials_ClickHandler = () => {
    validateCredentials_PageCommand({
      cloudName: currentSettings.CloudName,
      apiKey: currentSettings.ApiKey,
      apiSecret: currentSettings.ApiSecret,
    });
  };

  return (
    <FormItemWrapper
      label={props.label}
      invalid={props.invalid}
      validationMessage={props.validationMessage}
    >
      <div style={{ width: "50%" }}>
        <Box spacing={Spacing.L}>
          <Select
            onChange={(val) =>
              loadSettingsByWebsiteChannelID_OnChangeHandler(val)
            }
            label="Website Channels"
            placeholder="Select the Website Channel to see the settings"
          >
            {props.websiteChannels?.map((item, index) => {
              return (
                <MenuItem
                  primaryLabel={item.websiteChannelName}
                  value={item.websiteChannelID.toString()}
                  key={index}
                />
              );
            })}
          </Select>
          <Box spacing={Spacing.S}>
            <div style={{ color: "red" }}>
              <ul>
                {validationErrors
                  .filter((x) => x != null && x.trim() !== "")
                  .map((error) => (
                    <li key={error}>{error}</li>
                  ))}
              </ul>
            </div>
          </Box>
          <Input
            markAsRequired={true}
            validationMessage={"*"}
            label="Cloud Name"
            value={currentSettings.CloudName}
            disabled={!isEnabled}
            onChange={(e) =>
              setCurrentSettings(
                new WebsiteChannelCloudinaryDamSettings({
                  ...currentSettings,
                  CloudName: e.target.value,
                })
              )
            }
          />
          <PasswordField
            isEnabled={isEnabled}
            isRequired={true}
            label="API Key"
            value={currentSettings.ApiKey}
            onChange={(e) =>
              setCurrentSettings(
                new WebsiteChannelCloudinaryDamSettings({
                  ...currentSettings,
                  ApiKey: e.target.value,
                })
              )
            }
          />
          <PasswordField
            isEnabled={isEnabled}
            isRequired={true}
            label="API Secret"
            value={currentSettings.ApiSecret}
            onChange={(e) =>
              setCurrentSettings(
                new WebsiteChannelCloudinaryDamSettings({
                  ...currentSettings,
                  ApiSecret: e.target.value,
                })
              )
            }
          />
          <div style={{"margin": "15px 0 15px 0"}}>
            <Checkbox
              disabled={!isEnabled}
              label="Use SAML"
              checked={currentSettings.UseSaml}
              onChange={(e) =>
                setCurrentSettings(
                  new WebsiteChannelCloudinaryDamSettings({
                    ...currentSettings,
                    UseSaml: e.target.checked,
                    UseIframeSupportForSaml: e.target.checked
                      ? currentSettings.UseIframeSupportForSaml
                      : false,
                  })
                )
              }
            />
            <Checkbox
              disabled={!isEnabled || !currentSettings.UseSaml}
              label="iFrame Support for SAML"
              checked={currentSettings.UseIframeSupportForSaml}
              onChange={(e) =>
                setCurrentSettings(
                  new WebsiteChannelCloudinaryDamSettings({
                    ...currentSettings,
                    UseIframeSupportForSaml: e.target.checked,
                  })
                )
              }
            />
          </div>
          <TextArea
            disabled={!isEnabled}
            label="Default Transformations"
            value={currentSettings.DefaultTransformations}
            minRows={5}
            onChange={(e) =>
              setCurrentSettings(
                new WebsiteChannelCloudinaryDamSettings({
                  ...currentSettings,
                  DefaultTransformations: e.target.value,
                })
              )
            }
          />
          <br />
          <div style={{ display: "flex", flexDirection: "row", gap: "10px" }}>
            <Button
              label="Save"
              disabled={!isEnabled}
              size={ButtonSize.L}
              onClick={() => saveSettings_ClickHandler()}
            />
            <Button
              label="Test"
              disabled={!isEnabled}
              size={ButtonSize.L}
              onClick={() => validateCredentials_ClickHandler()}
            />
          </div>
        </Box>
      </div>
    </FormItemWrapper>
  );
};

import React, { useState } from "react";

import { FormComponentProps } from "@kentico/xperience-admin-base";
import {
  FormEditMode,
  FormItemWrapper,
} from "@kentico/xperience-admin-components";
import { CloudinaryAssetSelectorComponent } from "./componemts/CloudinaryAssetSelectorComponent";

interface AssetSelectorFormComponentProps extends FormComponentProps {
  userName: string | undefined;
  errorMessage: string;
  cloudName: string;
  apiKey: string;
  useSaml: boolean;
  useIframeSupportForSaml: boolean;
  defaultTransformations: string;
}

export const AssetSelectorFormComponent = (
  props: AssetSelectorFormComponentProps
) => {
  const handleOnChange = (value: string) => {
    if (props.onChange) {
      props.onChange(value);
    }
  };

  const renderAssetSelector = () => {
    if (props.editMode === FormEditMode.ReadOnly) {
      return (
        <div>
          Cloudinary Asset JSON Metadata:
          <span>{props.value}</span>.
        </div>
      );
    } else if (props.editMode === FormEditMode.Disabled) {
      return renderDisabledSelector();
    } else {
      let parsedAssetMetadata = null;
      try {
        parsedAssetMetadata = JSON.parse(props.value);
      } catch {}
      return (
        <div
          style={{
            position: "relative",
            display: "inline-block",
            color: "green",
            width: "100%",
          }}
        >
          <div style={{ color: "red" }}>{props.errorMessage}</div>
          <CloudinaryAssetSelectorComponent
            createConfig={{
              cloud_name: props.cloudName,
              api_key: props.apiKey,
              username: props.userName,
              use_saml: props.useSaml,
              saml_iframe_support: props.useIframeSupportForSaml,
              default_transformations: props.defaultTransformations.trim() !== "" ? JSON.parse(props.defaultTransformations) : "",
            }}
            onSelect={handleOnChange}
            disabled={props.errorMessage != null}
            currentAssetJsonValue={parsedAssetMetadata}
          />
        </div>
      );
    }
  };

  const renderDisabledSelector = () => {
    return (
      <div
        style={{ position: "relative", display: "inline-block", color: "red" }}
      >
        CLOUDINARY ASSET SELECTOR - DISABLED
      </div>
    );
  };

  return (
    <FormItemWrapper
      label={props.label}
      explanationText={props.explanationText}
      invalid={props.invalid}
      validationMessage={props.validationMessage}
      markAsRequired={props.required}
      labelIcon={props.tooltip ? "xp-i-circle" : undefined}
      labelIconTooltip={props.tooltip}
      editMode={props.editMode}
    >
      {renderAssetSelector()}
    </FormItemWrapper>
  );
};

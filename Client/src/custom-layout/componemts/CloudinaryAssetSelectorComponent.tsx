import React, { useState } from "react";

import {
  Button,
  ButtonSize,
  ButtonType,
} from "@kentico/xperience-admin-components";
import { UseCloudinaryMediaLibrary } from "../cloudinaryMedialLibraryWidgetServices/UseCloudinaryMediaLibraryWidget";
import { GetPreviewUrlFromAssetJson } from "../cloudinaryMedialLibraryWidgetServices/CloudinaryAssetJsonProcessors";

export interface CloudinaryAssetSelectorComponentProps {
  createConfig: {
    cloud_name: string;
    api_key?: string;
    username?: string;
    use_saml: boolean;
    saml_iframe_support: boolean;
    default_transformations: object[][];
  };

  onSelect: (value: string) => void;

  disabled?: boolean;
  currentAssetJsonValue?: object;
}

export const CloudinaryAssetSelectorComponent = (
  props: CloudinaryAssetSelectorComponentProps
) => {
  const { ready, error } = UseCloudinaryMediaLibrary();
  const [previewUrl, setPreviewUrl] = useState(GetPreviewUrlFromAssetJson(props.currentAssetJsonValue));

  const openLibrary = () => {
    if (!ready || props.disabled) return;

    const widget = window.cloudinary!.createMediaLibrary(props.createConfig, {
      insertHandler: (data: any) => {
        props.onSelect(JSON.stringify(data));
        setPreviewUrl(GetPreviewUrlFromAssetJson(data));
      },
    });

    widget.show();
  };

  const clearSelection = () => {
    props.onSelect("");
    setPreviewUrl("");
  };

  if (error) {
    return <div>Failed to load media library</div>;
  }

  return (
    <>
      <div
        style={{
          border: "1px dashed var(--color-input-border)",
          borderRadius: "20px",
          padding: "10px",
          minWidth: "100%",
          minHeight: "200px",
          display: "flex",
          alignItems: "center",
          justifyContent: "flex-end",
          overflow: "hidden",
          flexDirection: "column",
          flexWrap: "nowrap",
        }}
      >
        {previewUrl ? (
          <img
            src={previewUrl}
            alt="Selected asset"
            style={{
              objectFit: "cover",
              maxHeight: "200px",
            }}
          />
        ) : (
          <div
            style={{
              color: "var(--color-text-low-emphasis)",
              textAlign: "center",
              fontSize: "14px",
              padding: "0 0px 50px",
            }}
          >
            Select an image from the media library
          </div>
        )}
        <div
          style={{
            display: "flex",
            gap: "10px",
            justifyContent: "center",
            marginTop: "10px",
          }}
        >
          <Button
            disabled={!ready || props.disabled}
            label="Open Media Library"
            onClick={openLibrary}
            size={ButtonSize.S}
            type={ButtonType.Button}
          />
          <Button
            disabled={!ready || props.disabled || !previewUrl}
            label="Clear"
            onClick={clearSelection}
            size={ButtonSize.S}
            type={ButtonType.Reset}
          />
        </div>
      </div>
    </>
  );
};

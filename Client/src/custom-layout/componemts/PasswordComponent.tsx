import React, { useState } from "react";
import {
  Button,
  ButtonSize,
  ButtonType,
  Input,
} from "@kentico/xperience-admin-components";

interface PasswordFieldProps {
  label: string;
  value: string;
  isRequired: boolean;
  isEnabled: boolean;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export function PasswordField(props: PasswordFieldProps) {
  const [isShow, setIsShow] = useState(false);

  return (
    <div style={{ position: "relative" }}>
      <Input
        markAsRequired={props.isRequired}
        validationMessage={"*"}
        label={props.label}
        type={isShow ? "text" : "password"}
        value={props.value}
        disabled={!props.isEnabled}
        onChange={props.onChange}
      />
      <div style={{ position: "absolute", right: "5px", top: "34px" }}>
        <Button
          icon={isShow ? "xp-eye-slash" : "xp-eye"}
          type={ButtonType.Button}
          size={ButtonSize.XS}
          label={isShow ? "Hide" : "Show"}
          disabled={!props.isEnabled}
          onClick={() => setIsShow(!isShow)}
        ></Button>
      </div>
    </div>
  );
}

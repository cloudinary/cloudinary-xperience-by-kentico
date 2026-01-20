import { useEffect, useState } from "react";
import { CloudinaryMediaLibraryWidgetLoader } from "./CloudinaryMediaLibraryWidgetLoader";

export function UseCloudinaryMediaLibrary() {
  const [ready, setReady] = useState(false);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    let mounted = true;

    CloudinaryMediaLibraryWidgetLoader()
      .then(() => {
        if (mounted) setReady(true);
      })
      .catch((err) => {
        if (mounted) setError(err);
      });

    return () => {
      mounted = false;
    };
  }, []);

  return { ready, error };
}

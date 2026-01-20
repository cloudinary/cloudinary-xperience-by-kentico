let scriptLoadingPromise: Promise<void> | null = null;

export function CloudinaryMediaLibraryWidgetLoader(): Promise<void> {
  if (window.cloudinary?.createMediaLibrary) {
    return Promise.resolve();
  }

  if (!scriptLoadingPromise) {
    scriptLoadingPromise = new Promise((resolve, reject) => {
      const script = document.createElement("script");
      script.src = "https://media-library.cloudinary.com/global/all.js";
      script.async = true;

      script.onload = () => resolve();
      script.onerror = () =>
        reject(new Error("Failed to load Cloudinary Media Library"));

      document.body.appendChild(script);
    });
  }

  return scriptLoadingPromise;
}

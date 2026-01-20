export {};

declare global {
  interface Window {
    cloudinary?: {
      createMediaLibrary: (
        config: any,
        options: any
      ) => {
        show: () => void;
        hide: () => void;
      };
    };
  }
}

import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";

// https://vitejs.dev/config/
export default defineConfig({
  base: "/",
  plugins: [react()],
  resolve: {
    alias: {
      assets: "/src/assets",
      data: "/src/data",
      components: "/src/components",
      constants: "/src/constants",
      hooks: "/src/hooks",
      pages: "/src/pages",
      app: "/src/app",
      store: "/src/store",
      types: "/src/types",
      utils: "/src/utils",
      motion: "/src/motion",
      interfaces: "/src/interfaces",
    },
  },
});

import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  build: {
    outDir: '../webapi/wwwroot'
  },
  server: {
    port: 5173
  },
  plugins: [react()],
})

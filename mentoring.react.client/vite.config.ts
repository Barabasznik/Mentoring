import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port:7052,
    },
    build: {
        outDir: 'dist',
        assetsInlineLimit: 4096,
    },
    esbuild: {
        charset: "utf8",
    },
    define: {
        'process.env.LANG': '"pl_PL.UTF-8"',
    }
});



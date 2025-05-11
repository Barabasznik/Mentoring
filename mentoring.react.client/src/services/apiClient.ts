import axios from "axios";
import { msalInstance } from "./authconfig";

const apiClient = axios.create({
    baseURL: "https://localhost:7051", // Adres Twojego API
});

msalInstance.initialize().then(() => {
    apiClient.interceptors.request.use(async (config) => {
        const account = msalInstance.getActiveAccount();
        if (account) {
            const tokenResponse = await msalInstance.acquireTokenSilent({
                account: account,
                scopes: [
                    'api://2c5ae923-c4de-4e3f-8f28-dd27ba0ae7fa/All.ReadWrite' // Dopasowane do Twojego API
                ],
            });

            config.headers.Authorization = `Bearer ${tokenResponse.accessToken}`;
            console.log(`Token: ${tokenResponse.accessToken}`);  // Upewnij się, że token jest dobrze odczytywany
        }
        return config;
    });
});

export default apiClient;

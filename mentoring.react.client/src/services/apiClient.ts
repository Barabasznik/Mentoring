import axios from "axios";
import { msalInstance } from "./authconfig";

const apiClient = axios.create({
    baseURL: "https://localhost:7051/api", // 🔁 Zmień, jeśli Twój backend działa pod innym adresem
});

apiClient.interceptors.request.use(async (config) => {
    const account = msalInstance.getActiveAccount();

    if (account) {
        const tokenResponse = await msalInstance.acquireTokenSilent({
            scopes: ["All.ReadWrite"],
            account: account,
        });

        config.headers.Authorization = `Bearer ${tokenResponse.accessToken}`;
    }

    return config;
});

export default apiClient;

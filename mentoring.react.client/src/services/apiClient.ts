import axios from "axios";

const apiClient = axios.create({
    baseURL: "https://localhost:7051",
});

export default apiClient;

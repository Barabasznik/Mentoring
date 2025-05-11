import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import { TokenPayload } from "../types/TokenPayload";
import { Role } from "../types/Role";
import { useMsal } from "@azure/msal-react";

export const useRole = () => {
    const { instance } = useMsal();
    const [roles, setRoles] = useState<Role[]>([]);

    useEffect(() => {
        const extractRoles = async () => {
            const account = instance.getActiveAccount();
            if (!account) return;

            try {
                const response = await instance.acquireTokenSilent({
                    account,
                    scopes: ["api://2c5ae923-c4de-4e3f-8f28-dd27ba0ae7fa/All.ReadWrite"],  // Użyj poprawnego scope
                });
                console.log('Access Token:', response.accessToken);  // Debugging
                const decoded = jwtDecode<TokenPayload>(response.accessToken);
                if (!decoded.roles || !Array.isArray(decoded.roles)) {
                    console.warn("Brak ról w tokenie lub roles nie są tablicą");
                }

                if (decoded.roles && Array.isArray(decoded.roles)) {
                    setRoles(decoded.roles as Role[]);
                } else {
                    setRoles([]);
                }
            } catch (err) {
                console.error("Błąd pobierania ról użytkownika:", err);
                setRoles([]);
            }
        };

        extractRoles();
    }, [instance]);

    return roles;
};

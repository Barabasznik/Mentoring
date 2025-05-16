import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import { useMsal } from "@azure/msal-react";
import { Role } from "../types/Role";
import { TokenPayload } from "../types/TokenPayload";

export const useRole = () => {
    const { instance } = useMsal();
    const [roles, setRoles] = useState<Role[]>([]);
    const [isLoaded, setIsLoaded] = useState(false);

    useEffect(() => {
        const extractRoles = async () => {
            const account = instance.getActiveAccount();
            if (!account) return;

            try {
                const response = await instance.acquireTokenSilent({
                    account,
                    scopes: ["api://2c5ae923-c4de-4e3f-8f28-dd27ba0ae7fa/All.ReadWrite"],
                });
                const decoded = jwtDecode<TokenPayload>(response.accessToken);

                if (decoded.roles && Array.isArray(decoded.roles)) {
                    setRoles(decoded.roles as Role[]);
                } else {
                    setRoles([]);
                }
            } catch (err) {
                console.error("Błąd pobierania ról:", err);
                setRoles([]);
            } finally {
                setIsLoaded(true);
            }
        };

        extractRoles();
    }, [instance]);

    return { roles, isLoaded };
};

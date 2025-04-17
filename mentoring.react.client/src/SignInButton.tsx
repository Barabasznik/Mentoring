import React from 'react';
import { useMsal } from '@azure/msal-react';

const SignInButton: React.FC = () => {
    const { instance } = useMsal();

    const handleLogin = async () => {
        try {
            const loginResponse = await instance.loginPopup({
                scopes: ["User.Read"],
            });
            instance.setActiveAccount(loginResponse.account);
            console.log("Zalogowano jako:", loginResponse.account);
        } catch (error) {
            console.error("Błąd logowania:", error);
        }
    };

    return <button onClick={handleLogin}>Zaloguj się</button>;
};

export default SignInButton;

import React from 'react';
import { useMsal } from '@azure/msal-react';
import { InteractionStatus } from '@azure/msal-browser';

const SignInButton: React.FC = () => {
    const { instance, inProgress } = useMsal();

    const handleLogin = async () => {
        if (inProgress !== InteractionStatus.None) {
            console.warn("Logowanie już trwa...");
            return;
        }

        try {
            const loginResponse = await instance.loginPopup({
                scopes: ["User.Read"], // możesz podać też swój custom scope jeśli używasz API
            });
            instance.setActiveAccount(loginResponse.account);
            console.log("Zalogowano jako:", loginResponse.account);
        } catch (error) {
            console.error("Błąd logowania:", error);
        }
    };

    return <button className='login-button' onClick={handleLogin}>Zaloguj się</button>;
};

export default SignInButton;

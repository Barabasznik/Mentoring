import { useMsal } from "@azure/msal-react";


const SignOutButton = () => {
    const { instance } = useMsal();
    const handleLogout = () => {
        instance.logoutPopup();

    };
    return <button className="logout-button" onClick={handleLogout}>Wyloguj się</button>;

};

export default SignOutButton
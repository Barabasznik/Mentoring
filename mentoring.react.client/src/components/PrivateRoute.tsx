import { ReactNode } from "react";
import { Navigate } from "react-router-dom";
import { useIsAuthenticated } from "@azure/msal-react";
import { useRole } from "../hooks/useRole";
import { Role } from "../types/Role";

interface Props {
    children: ReactNode;
    allowedRoles?: Role[];
}

const PrivateRoute = ({ children, allowedRoles }: Props) => {
    const isAuthenticated = useIsAuthenticated();
    const { roles, isLoaded } = useRole();

    if (!isAuthenticated) {
        return <Navigate to="/" replace />;
    }

    if (!isLoaded) {
        return <p style={{ textAlign: "center" }}>Ładowanie uprawnień...</p>;
    }

    const hasAccess = allowedRoles
        ? roles.some((role: Role) => allowedRoles.includes(role))
        : true;

    if (!hasAccess) {
        return <p style={{ color: "red", textAlign: "center" }}> Brak dostępu</p>;
    }

    return <>{children}</>;
};

export default PrivateRoute;

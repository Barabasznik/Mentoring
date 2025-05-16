import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate, useLocation } from "react-router-dom";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useIsAuthenticated } from "@azure/msal-react";
import { useEffect } from "react";
import AdminPage from "./pages/AdminPage";
import LibrarianPage from "./pages/LibrarianPage";
import MemberPage from "./pages/MemberPage";
import FirstPageLogin from "./pages/FirstPageLogin";
import { useRole } from "./hooks/useRole";
import { Role } from "./types/Role";
import PrivateRoute from "./components/PrivateRoute";

const AppRoutes = () => {
    const { roles, isLoaded } = useRole();
    const navigate = useNavigate();
    const location = useLocation();
    const isAuthenticated = useIsAuthenticated();

    useEffect(() => {
        if (!isLoaded || !isAuthenticated) return;


        if (location.pathname === "/" || location.pathname === "/member" || location.pathname === "/admin" || location.pathname === "/librarian") {
            if (roles.includes(Role.Admin)) navigate("/admin", { replace: true });
            else if (roles.includes(Role.Librarian)) navigate("/librarian", { replace: true });
            else if (roles.includes(Role.Member)) navigate("/member", { replace: true });
        }
    }, [roles, isLoaded, isAuthenticated, navigate, location.pathname]);

    const getDefaultRedirect = () => {
        if (!isLoaded) return <p style={{ textAlign: "center" }}>🔄 Ładowanie ról...</p>;
        if (roles.includes(Role.Admin)) return <Navigate to="/admin" />;
        if (roles.includes(Role.Librarian)) return <Navigate to="/librarian" />;
        if (roles.includes(Role.Member)) return <Navigate to="/member" />;
        return <p> Brak przypisanej roli.</p>;
    };

    return (
        <Routes>
            <Route path="/" element={getDefaultRedirect()} />
            <Route
                path="/admin"
                element={
                    <PrivateRoute allowedRoles={[Role.Admin]}>
                        <AdminPage />
                    </PrivateRoute>
                }
            />
            <Route
                path="/librarian"
                element={
                    <PrivateRoute allowedRoles={[Role.Librarian]}>
                        <LibrarianPage />
                    </PrivateRoute>
                }
            />
            <Route
                path="/member"
                element={
                    <PrivateRoute allowedRoles={[Role.Member]}>
                        <MemberPage />
                    </PrivateRoute>
                }
            />
        </Routes>
    );
};

const App = () => {
    return (
        <Router>
            <AuthenticatedTemplate>
                <AppRoutes />
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <Routes>
                    <Route path="*" element={<FirstPageLogin />} />
                </Routes>
            </UnauthenticatedTemplate>
        </Router>
    );
};

export default App;
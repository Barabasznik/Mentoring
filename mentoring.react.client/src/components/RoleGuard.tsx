import { ReactNode } from "react";
import { useRole } from "../hooks/useRole";
import { Role } from "../types/Role";

interface RoleGuardProps {
  allowedRoles: Role[];
  children: ReactNode;
}

const RoleGuard = ({ allowedRoles, children }: RoleGuardProps) => {
  const { roles, isLoaded } = useRole();

  if (!isLoaded) return null;

  const hasAccess = roles.some(role => allowedRoles.includes(role));
  console.log("Rola użytkownika:", roles);

  return <>{hasAccess ? children : null}</>;
};

export default RoleGuard;

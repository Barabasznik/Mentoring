import { ReactNode } from "react";
import { useRole } from "../hooks/useRole";
import { Role } from "../types/Role";

interface RoleGuardProps {
  allowedRoles: Role[];
  children: ReactNode;
}

const RoleGuard = ({ allowedRoles, children }: RoleGuardProps) => {
  const roles = useRole();

  const hasAccess = roles.some(role => allowedRoles.includes(role));
  console.log(roles)
  return <>{hasAccess && children}</>;
};

export default RoleGuard;

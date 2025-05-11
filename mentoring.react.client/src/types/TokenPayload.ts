import { Role } from "./Role";

export interface TokenPayload {
  roles?: Role[] | string[];
}

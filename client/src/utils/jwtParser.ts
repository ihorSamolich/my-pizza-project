import { jwtDecode } from "jwt-decode";

export const jwtParser = (value: string | null) => {
  if (!value) {
    return null;
  }
  return jwtDecode(value);
};

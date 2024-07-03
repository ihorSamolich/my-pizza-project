import { jwtDecode } from "jwt-decode";

export const checkTokenExpiration = (token: string | null) => {
  if (!token) {
    return false;
  }

  try {
    const tokenData = jwtDecode(token);

    if (tokenData.exp) {
      const expirationTime = tokenData.exp * 1000;
      return expirationTime > Date.now();
    } else {
      return true;
    }
  } catch (error) {
    return false;
  }
};

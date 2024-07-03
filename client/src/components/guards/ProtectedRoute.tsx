import { getToken } from "app/slice/userSlice.ts";
import { useAppSelector } from "app/store.ts";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { checkTokenExpiration } from "utils/checkTokenExpiration.ts";

const ProtectedRoute = () => {
  const location = useLocation();
  const token = useAppSelector(getToken);

  return checkTokenExpiration(token) ? <Outlet /> : <Navigate to="/auth/sign-in" state={{ from: location }} replace />;
};

export default ProtectedRoute;

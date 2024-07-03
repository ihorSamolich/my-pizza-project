import { zodResolver } from "@hookform/resolvers/zod";
import { IconLoader2, IconLogin2 } from "@tabler/icons-react";
import { useLoginMutation } from "app/services/accountService.ts";
import { setCredentials } from "app/slice/userSlice.ts";
import { useAppDispatch } from "app/store.ts";
import { Button, Input, Label } from "components/ui";
import { IUser } from "interfaces/account.ts";
import { LoginSchema, LoginSchemaType } from "interfaces/zod/account.ts";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { Helmet } from "react-helmet";
import { useForm } from "react-hook-form";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { jwtParser } from "utils/jwtParser.ts";
import { setLocalStorageItem } from "utils/localStorageUtils.ts";
import { showNotification } from "utils/showNotification.ts";

const LoginPage = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const [login, { isLoading }] = useLoginMutation();
  const dispatch = useAppDispatch();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginSchemaType>({ resolver: zodResolver(LoginSchema) });

  const onSubmit = async (data: LoginSchemaType) => {
    try {
      const res = await login(data).unwrap();
      onLoginSuccess(res.token);
      showNotification(`Авторизація успішна!`, "success");
    } catch (error) {
      showNotification(`Помилка авторизаціі. Перевірте ваші дані!`, "error");
    }
  };

  const onLoginSuccess = (token: string) => {
    setLocalStorageItem("authToken", token);
    dispatch(setCredentials({ user: jwtParser(token) as IUser, token: token }));
    const { from } = location.state || { from: { pathname: "/" } };
    navigate(from);
  };

  return (
    <div className="flex flex-col gap-4">
      <Helmet>
        <title>MyPizza | Login</title>
      </Helmet>
      <WelcomeBanner title="Welcome to MyPizza" description="Please log in to your account to continue" />
      <div className="w-full flex justify-center items-center">
        <form className="w-full max-w-md flex flex-col gap-4" onSubmit={handleSubmit(onSubmit)}>
          <div>
            <Label htmlFor="email">Email</Label>
            <Input {...register("email")} type="email" id="email" className="mt-1 block w-full" />
            {errors.email && <p className="mt-2 text-sm text-red-600">{errors.email.message}</p>}
          </div>
          <div>
            <Label htmlFor="password">Password</Label>
            <Input {...register("password")} type="password" id="password" className="mt-1 block w-full" />
            {errors.password && <p className="mt-2 text-sm text-red-600">{errors.password.message}</p>}
          </div>
          <div className="flex justify-center">
            <Button disabled={isLoading} variant="login" size="sm" type="submit">
              {isLoading ? <IconLoader2 className="animate-spin" /> : <IconLogin2 />} Log in
            </Button>
          </div>
          <div className="text-center font-semibold">
            <p className="text-sm text-gray-600 dark:text-gray-100">
              Don't have an account?{" "}
              <Link to="/auth/register" className="text-blue-500 hover:underline">
                Sign up
              </Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;

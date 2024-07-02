import { zodResolver } from "@hookform/resolvers/zod";
import { IconCheck, IconLoader2, IconLogin2 } from "@tabler/icons-react";
import { useLoginMutation } from "app/services/accountService.ts";
import { Button, Input, Label } from "components/ui";
import { ILoginResponse } from "interfaces/account.ts";
import { LoginSchema, LoginSchemaType } from "interfaces/zod/account.ts";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { Helmet } from "react-helmet";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";

import React from "react";

const LoginPage = () => {
  const [login, { isLoading }] = useLoginMutation();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginSchemaType>({ resolver: zodResolver(LoginSchema) });

  const onSubmit = async (data: LoginSchemaType) => {
    try {
      await login(data).unwrap();
      onLoginSuccess();
    } catch (error) {
      console.log(error);
    }
  };

  const onLoginSuccess = (response: ILoginResponse) => {
    console.log(response);
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
              <Link to="auth/register" className="text-blue-500 hover:underline">
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

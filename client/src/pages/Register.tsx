import { zodResolver } from "@hookform/resolvers/zod";
import { IconUserPlus } from "@tabler/icons-react";
import { useRegisterMutation } from "app/services/accountService.ts";
import { setCredentials } from "app/slice/userSlice.ts";
import { useAppDispatch } from "app/store.ts";
import { Button, Input, Label } from "components/ui";
import FileInput from "components/ui/FileInput.tsx";
import { IUser } from "interfaces/account.ts";
import { RegisterSchema, RegisterSchemaType } from "interfaces/zod/account.ts";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { Helmet } from "react-helmet";
import { useForm } from "react-hook-form";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { jwtParser } from "utils/jwtParser.ts";
import { setLocalStorageItem } from "utils/localStorageUtils.ts";
import { showNotification } from "utils/showNotification.ts";

import { useState } from "react";

const RegisterPage = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const [create] = useRegisterMutation();
  const [previewImage, setPreviewImage] = useState<string | undefined>(undefined);

  const dispatch = useAppDispatch();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterSchemaType>({ resolver: zodResolver(RegisterSchema) });

  const onSubmit = async (data: RegisterSchemaType) => {
    try {
      const res = await create({ ...data, image: data.image[0] as File }).unwrap();
      onRegisterSuccess(res.token);
      showNotification(`Реєстрація успішна!`, "success");
    } catch (error) {
      showNotification(`Помилка реєстраціі. Перевірте ваші дані!`, "error");
    }
  };

  const onRegisterSuccess = (token: string) => {
    setLocalStorageItem("authToken", token);
    dispatch(setCredentials({ user: jwtParser(token) as IUser, token: token }));
    const { from } = location.state || { from: { pathname: "/" } };
    navigate(from);
  };

  return (
    <div className="flex flex-col gap-4">
      <Helmet>
        <title>MyPizza | Register</title>
      </Helmet>
      <WelcomeBanner title="Welcome to MyPizza" description="Please register to create your account" />
      <div className="w-full flex justify-center items-center">
        <form className="w-full max-w-md flex flex-col gap-4" onSubmit={handleSubmit(onSubmit)}>
          <div>
            <Label htmlFor="firstName">First Name</Label>
            <Input {...register("firstName")} id="firstName" className="mt-1 block w-full" />
            {errors.firstName && <p className="mt-2 text-sm text-red-600">{errors.firstName.message}</p>}
          </div>
          <div>
            <Label htmlFor="lastName">Last Name</Label>
            <Input {...register("lastName")} id="lastName" className="mt-1 block w-full" />
            {errors.lastName && <p className="mt-2 text-sm text-red-600">{errors.lastName.message}</p>}
          </div>

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

          <div>
            <Label htmlFor="confirmPassword">Confirm Password</Label>
            <Input {...register("confirmPassword")} type="password" id="confirmPassword" className="mt-1 block w-full" />
            {errors.confirmPassword && <p className="mt-2 text-sm text-red-600">{errors.confirmPassword.message}</p>}
          </div>

          <div>
            <Label htmlFor="image">Profile Image</Label>
            <FileInput previewImage={previewImage} setPreviewImage={setPreviewImage} {...register("image")} />
            {errors.image && <p className="mt-2 text-sm text-red-600">{errors.image.message}</p>}
          </div>

          <div className="flex justify-center">
            <Button variant="login" size="sm" type="submit">
              <IconUserPlus /> Register
            </Button>
          </div>
          <div className="text-center font-semibold">
            <p className="text-sm text-gray-600 dark:text-gray-100">
              Already have an account?{" "}
              <Link to="/auth/sign-in" className="text-blue-500 hover:underline">
                Log in
              </Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;

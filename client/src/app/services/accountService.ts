import { createApi } from "@reduxjs/toolkit/query/react";
import { ILogin, ILoginResponse } from "interfaces/account.ts";
import { createBaseQuery } from "utils/baseQuery.ts";

export const accountApi = createApi({
  reducerPath: "accountApi",
  baseQuery: createBaseQuery("accounts"),
  tagTypes: ["Account"],

  endpoints: (builder) => ({
    login: builder.mutation<ILoginResponse, ILogin>({
      query: (data) => {
        const formData = new FormData();
        formData.append("email", data.email);
        formData.append("password", data.password);

        return {
          url: "SignIn",
          method: "POST",
          body: formData,
        };
      },
    }),
  }),
});

export const { useLoginMutation } = accountApi;

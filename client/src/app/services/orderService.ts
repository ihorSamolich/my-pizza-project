import { createApi } from "@reduxjs/toolkit/query/react";
import { IOrder } from "interfaces/order.ts";
import { createBaseQuery } from "utils/baseQuery.ts";

export const orderApi = createApi({
  reducerPath: "orderApi",
  baseQuery: createBaseQuery("order"),
  tagTypes: ["Orders"],

  endpoints: (builder) => ({
    getAllOrders: builder.query<IOrder[], void>({
      query: () => "getAll",
      providesTags: ["Orders"],
    }),
  }),
});

export const { useGetAllOrdersQuery } = orderApi;

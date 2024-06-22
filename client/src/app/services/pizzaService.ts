import { createApi } from "@reduxjs/toolkit/query/react";
import { IPizza } from "interfaces/pizza.ts";
import { createBaseQuery } from "utils/baseQuery.ts";

export const pizzaApi = createApi({
  reducerPath: "pizzaApi",
  baseQuery: createBaseQuery("pizza"),
  tagTypes: ["Pizzas"],

  endpoints: (builder) => ({
    getAllPizzas: builder.query<IPizza[], void>({
      query: () => "getAll",
      providesTags: ["Pizzas"],
    }),
  }),
});

export const { useGetAllPizzasQuery } = pizzaApi;

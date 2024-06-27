import { createApi } from "@reduxjs/toolkit/query/react";
import { IPagedDataResponse } from "interfaces/index.ts";
import { IPizza, IPizzaCreate, IPizzaPagedRequest } from "interfaces/pizza.ts";
import { createBaseQuery } from "utils/baseQuery.ts";
import { createQueryString } from "utils/createQueryString.ts";

export const pizzaApi = createApi({
  reducerPath: "pizzaApi",
  baseQuery: createBaseQuery("pizza"),
  tagTypes: ["Pizzas"],

  endpoints: (builder) => ({
    getAllPizzas: builder.query<IPizza[], void>({
      query: () => "getAll",
      providesTags: ["Pizzas"],
    }),

    getPagedPizzas: builder.query<IPagedDataResponse<IPizza>, IPizzaPagedRequest>({
      query: (params) => {
        const queryString = createQueryString(params as Record<string, any>);
        return `getPage?${queryString}`;
      },
      providesTags: ["Pizzas"],
    }),

    createPizza: builder.mutation<void, IPizzaCreate>({
      query: (pizza) => {
        const formData = new FormData();

        formData.append("Name", pizza.name);
        formData.append("Description", pizza.description);
        formData.append("CategoryId", pizza.categoryId);

        if (pizza.ingredientIds) {
          Array.from(pizza.ingredientIds).forEach((ingredient) => formData.append("IngredientIds", ingredient.toString()));
        }

        if (pizza.photos) {
          Array.from(pizza.photos).forEach((photo) => formData.append("Photos", photo));
        }

        if (pizza.sizes) {
          pizza.sizes.forEach((size, index) => {
            formData.append(`Sizes[${index}].sizeId`, size.sizeId.toString());
            formData.append(`Sizes[${index}].price`, size.price.toString());
          });
        }

        return {
          url: "create",
          method: "POST",
          body: formData,
        };
      },
      invalidatesTags: ["Pizzas"],
    }),

    deletePizza: builder.mutation<void, number>({
      query: (id) => ({
        url: `delete/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Pizzas"],
    }),
  }),
});

export const { useGetAllPizzasQuery, useGetPagedPizzasQuery, useCreatePizzaMutation, useDeletePizzaMutation } = pizzaApi;

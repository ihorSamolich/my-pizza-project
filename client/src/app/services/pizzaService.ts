import { createApi } from "@reduxjs/toolkit/query/react";
import { IPizza, IPizzaCreate } from "interfaces/pizza.ts";
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

        formData.append("Sizes[0].sizeId", "1");
        formData.append("Sizes[0].price", "200");
        formData.append("Sizes[1].sizeId", "2");
        formData.append("Sizes[1].price", "130");

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

export const { useGetAllPizzasQuery, useCreatePizzaMutation, useDeletePizzaMutation } = pizzaApi;

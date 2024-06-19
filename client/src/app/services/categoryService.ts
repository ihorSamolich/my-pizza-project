import { createApi } from "@reduxjs/toolkit/query/react";
import { ICategory } from "interfaces/category.ts";
import { createBaseQuery } from "utils/BaseQuery.ts";

export const categoryApi = createApi({
  reducerPath: "categoryApi",
  baseQuery: createBaseQuery("categories"),
  tagTypes: ["Categories"],

  endpoints: (builder) => ({
    getAllCategories: builder.query<ICategory[], void>({
      query: () => "/",
      providesTags: ["Categories"],
    }),
    deleteCategory: builder.mutation<void, number>({
      query: (id) => ({
        url: `/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Categories"],
    }),
  }),
});

export const { useGetAllCategoriesQuery, useDeleteCategoryMutation } = categoryApi;

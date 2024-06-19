import { createApi } from "@reduxjs/toolkit/query/react";
import { ICategory, ICategoryCreate } from "interfaces/category.ts";
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

    createCategory: builder.mutation<void, ICategoryCreate>({
      query: (category) => {
        const categoryFormData = new FormData();
        categoryFormData.append("Name", category.name);
        categoryFormData.append("Image", category.image);

        return {
          url: "/",
          method: "POST",
          body: categoryFormData,
        };
      },
      invalidatesTags: ["Categories"],
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

export const { useGetAllCategoriesQuery, useDeleteCategoryMutation, useCreateCategoryMutation } = categoryApi;

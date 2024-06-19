import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "components/ui/Button.tsx";
import { CategoryCreateSchema, CategoryCreateSchemaType } from "interfaces/zod/category.ts";
import { useForm } from "react-hook-form";

import React from "react";

const CreateCategory: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CategoryCreateSchemaType>({ resolver: zodResolver(CategoryCreateSchema) });

  const onSubmit = (data: CategoryCreateSchemaType) => {
    console.log(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <label htmlFor="name" className="block text-sm font-medium text-gray-700">
          Name
        </label>
        <input
          type="text"
          id="name"
          {...register("name")}
          className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
        />
        {errors.name && <p className="mt-2 text-sm text-red-600">{errors.name.message}</p>}
      </div>

      <div>
        <label htmlFor="image" className="block text-sm font-medium text-gray-700">
          Image
        </label>
        <input
          type="file"
          id="image"
          {...register("image")}
          className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
        />
        {errors.image && <p className="mt-2 text-sm text-red-600">{errors.image.message}</p>}
      </div>

      <Button variant="secondary" size="sm" type="submit">
        Submit
      </Button>
    </form>
  );
};

export default CreateCategory;

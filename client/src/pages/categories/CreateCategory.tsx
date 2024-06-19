import { zodResolver } from "@hookform/resolvers/zod";
import { useCreateCategoryMutation } from "app/services/categoryService.ts";
import { Button } from "components/ui/Button.tsx";
import FileInput from "components/ui/FileInput.tsx";
import Input from "components/ui/Input.tsx";
import Label from "components/ui/Label.tsx";
import { CategoryCreateSchema, CategoryCreateSchemaType } from "interfaces/zod/category.ts";
import { useForm } from "react-hook-form";

import React from "react";

const CreateCategory: React.FC = () => {
  const [createCategory] = useCreateCategoryMutation();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CategoryCreateSchemaType>({ resolver: zodResolver(CategoryCreateSchema) });

  const onSubmit = async (data: CategoryCreateSchemaType) => {
    await createCategory({ ...data, image: data.image[0] as File }).unwrap();
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <Label htmlFor="name">Name</Label>
        <Input type="text" id="name" {...register("name")} />
        {errors.name && <p className="mt-2 text-sm text-red-600">{errors.name.message}</p>}
      </div>

      <div>
        <Label htmlFor="image">Image</Label>
        <FileInput {...register("image")} />
        {errors.image && <p className="mt-2 text-sm text-red-600">{errors.image.message}</p>}
      </div>

      <Button variant="secondary" size="sm" type="submit">
        Submit
      </Button>
    </form>
  );
};

export default CreateCategory;

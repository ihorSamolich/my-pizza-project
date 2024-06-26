import { zodResolver } from "@hookform/resolvers/zod";
import { IconCheck, IconX } from "@tabler/icons-react";
import { useGetAllCategoriesQuery } from "app/services/categoryService.ts";
import { useGetAllIngredientsQuery } from "app/services/ingredientService.ts";
import { useCreatePizzaMutation } from "app/services/pizzaService.ts";
import { Button } from "components/ui/Button.tsx";
import Checkbox from "components/ui/Checkbox.tsx";
import FileInputMultiple from "components/ui/FileInputMultiple.tsx";
import Input from "components/ui/Input.tsx";
import Label from "components/ui/Label.tsx";
import Select from "components/ui/Select.tsx";
import TextArea from "components/ui/TextArea.tsx";
import { PizzaCreateSchema, PizzaCreateSchemaType } from "interfaces/zod/pizza.ts";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { UseFormGetValues, UseFormSetValue, useForm } from "react-hook-form";

import React, { ChangeEvent, useEffect, useRef, useState } from "react";

const CreatePizzaPage: React.FC = () => {
  const [photos, setPhotos] = useState<File[]>([]);

  const inputRef = useRef<HTMLInputElement>(null);

  const { data: categories, isLoading: isLoadingCategories } = useGetAllCategoriesQuery();
  const { data: ingredients, isLoading: isLoadingIngredients } = useGetAllIngredientsQuery();

  const [createPizza] = useCreatePizzaMutation();

  const {
    register,
    handleSubmit,
    reset,
    setValue,
    getValues,
    formState: { errors },
  } = useForm<PizzaCreateSchemaType>({ resolver: zodResolver(PizzaCreateSchema) });

  useEffect(() => {
    if (inputRef.current) {
      const dataTransfer = new DataTransfer();
      photos.forEach((file) => dataTransfer.items.add(file));
      inputRef.current.files = dataTransfer.files;
    }
    setValue("photos", inputRef.current?.files as any);
  }, [photos, setValue]);

  const onSubmit = handleSubmit(async (data) => {
    console.log(data);

    await createPizza({ ...data, photos: data.photos as File[] });
  });

  const onReset = () => {
    reset();
  };

  const handleCheckboxChange = (
    event: React.ChangeEvent<HTMLInputElement>,
    setValue: UseFormSetValue<PizzaCreateSchemaType>,
    getValues: UseFormGetValues<PizzaCreateSchemaType>,
  ) => {
    const { value, checked } = event.target;
    const currentValues = getValues("ingredientIds") || [];

    if (checked) {
      setValue("ingredientIds", [...currentValues, parseInt(value)], {
        shouldValidate: true,
      });
    } else {
      setValue(
        "ingredientIds",
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-expect-error
        currentValues.filter((val) => val !== parseInt(value)),
        {
          shouldValidate: true,
        },
      );
    }
  };

  const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files;

    if (file) {
      setPhotos((prevFiles) => {
        const updatedFiles = [...prevFiles];
        for (let i = 0; i < file.length; i++) {
          const validImageTypes = ["image/gif", "image/jpeg", "image/webp", "image/png"];
          if (validImageTypes.includes(file[i].type)) {
            const isDuplicate = updatedFiles.some((existingFile) => existingFile.name === file[i].name);
            if (!isDuplicate) {
              updatedFiles.push(file[i]);
            }
          }
        }
        return updatedFiles;
      });
    }
  };

  return (
    <div className="flex flex-col gap-4">
      <WelcomeBanner
        title="Create a New Pizza"
        description="Here you can create a new  pizza. Enter a data and choose an image to get started."
      />

      <form className="space-y-4" onSubmit={onSubmit}>
        <div>
          <Label htmlFor="name">Name</Label>
          <Input {...register("name")} type="text" id="name" />
          {errors.name && <p className="mt-2 text-sm text-red-600">{errors.name.message}</p>}
        </div>

        <div>
          <Label htmlFor="description">Description</Label>
          <TextArea {...register("description")} id="description" />
          {errors.description && <p className="mt-2 text-sm text-red-600">{errors.description.message}</p>}
        </div>

        <div>
          <Label htmlFor="categoryId">Category</Label>
          <Select {...register("categoryId")} disabled={isLoadingCategories} defaultValue="" id="categoryId">
            <option disabled value="">
              Оберіть категорію
            </option>
            {categories?.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </Select>
          {errors.categoryId && <p className="mt-2 text-sm text-red-600">{errors.categoryId.message}</p>}
        </div>

        <div>
          <Label>Ingredients</Label>
          <div className="p-2.5 grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-2 shadow-sm bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-blue-500 focus:border-blue-500 w-full dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 dark:shadow-sm-light">
            {ingredients?.map((ingredient) => (
              <Label key={ingredient.id} size="small">
                <Checkbox
                  id={`ingredient-${ingredient.id}`}
                  type="checkbox"
                  disabled={isLoadingIngredients}
                  value={ingredient.id}
                  onChange={(event) => handleCheckboxChange(event, setValue, getValues)}
                />
                <span>{ingredient.name}</span>
              </Label>
            ))}
          </div>
          {errors.ingredientIds && <p className="mt-2 text-sm text-red-600">{errors.ingredientIds.message}</p>}
        </div>

        <div>
          <Label htmlFor="Images">Images</Label>
          <FileInputMultiple files={photos} setFiles={setPhotos}>
            <Input
              {...register("photos")}
              onChange={handleFileChange}
              multiple
              ref={inputRef}
              id="photos"
              type="file"
              variant="file"
            />
          </FileInputMultiple>
          {errors.photos && <p className="mt-2 text-sm text-red-600">{errors.photos.message}</p>}
        </div>

        <div className="flex justify-center gap-5">
          <Button variant="success" size="sm" type="submit">
            <IconCheck /> Submit
          </Button>
          <Button variant="danger" size="sm" onClick={onReset}>
            <IconX /> Reset
          </Button>
        </div>
      </form>
    </div>
  );
};

export default CreatePizzaPage;

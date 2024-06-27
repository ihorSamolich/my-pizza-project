import { zodResolver } from "@hookform/resolvers/zod";
import { IconX } from "@tabler/icons-react";
import { useGetAllCategoriesQuery } from "app/services/categoryService.ts";
import { useGetAllIngredientsQuery } from "app/services/ingredientService.ts";
import { useGetPizzaByIdQuery } from "app/services/pizzaService.ts";
import LoadingSpinner from "components/LoadingSpinner.tsx";
import Button from "components/ui/Button.tsx";
import Checkbox from "components/ui/Checkbox.tsx";
import Input from "components/ui/Input.tsx";
import Label from "components/ui/Label.tsx";
import Select from "components/ui/Select.tsx";
import TextArea from "components/ui/TextArea.tsx";
import { IPizza } from "interfaces/pizza.ts";
import { PizzaEditSchema, PizzaEditSchemaType } from "interfaces/zod/pizza.ts";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import React, { useEffect } from "react";

const EditPizzaPage: React.FC = () => {
  const { id } = useParams();
  // const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
    setValue,
    getValues,
  } = useForm<PizzaEditSchemaType>({ resolver: zodResolver(PizzaEditSchema) });

  const { data: pizza, isLoading: isPizzaLoading } = useGetPizzaByIdQuery(Number(id));
  const { data: categories, isLoading: isLoadingCategories } = useGetAllCategoriesQuery();
  const { data: ingredients, isLoading: isLoadingIngredients } = useGetAllIngredientsQuery();

  useEffect(() => {
    getDefaultPizza(pizza);
  }, [pizza, setValue]);

  const onSubmit = async (data: PizzaEditSchemaType) => {
    console.log(data);
  };

  const getDefaultPizza = (pizza?: IPizza) => {
    if (pizza) {
      setValue("id", pizza.id);
      setValue("name", pizza.name);
      setValue("description", pizza.description);
      setValue("categoryId", pizza.category.id.toString());

      setValue(
        "ingredientIds",
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-expect-error
        pizza.ingredients.map((ingredient) => ingredient.id),
      );
    }
  };

  const handleReset = () => {
    reset();
    getDefaultPizza(pizza);
  };

  if (isPizzaLoading || isLoadingCategories) {
    return <LoadingSpinner />;
  }

  return (
    <div className="flex flex-col gap-4">
      <WelcomeBanner
        title="Edit Pizza"
        description="Update the details of the pizza. You can modify the name and choose a new image."
      />

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <div>
          <Label htmlFor="name">Name</Label>
          <Input type="text" id="name" {...register("name")} />
          {errors.name && <p className="mt-2 text-sm text-red-600">{errors.name.message}</p>}
        </div>

        <div>
          <Label htmlFor="description">Description</Label>
          <TextArea {...register("description")} id="description" />
          {errors.description && <p className="mt-2 text-sm text-red-600">{errors.description.message}</p>}
        </div>

        <div>
          <Label htmlFor="categoryId">Category</Label>
          <Select {...register("categoryId")} disabled={isLoadingCategories} defaultValue={pizza?.category.id} id="categoryId">
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
                  defaultChecked={getValues("ingredientIds")?.includes(ingredient.id)}
                />
                <span>{ingredient.name}</span>
              </Label>
            ))}
          </div>
          {errors.ingredientIds && <p className="mt-2 text-sm text-red-600">{errors.ingredientIds.message}</p>}
        </div>

        <div className="hidden">
          <Label htmlFor="id">Id</Label>
          <input type="text" id="id" {...register("id")} />
          {errors.id && <p className="mt-2 text-sm text-red-600">{errors.id.message}</p>}
        </div>

        <div className="flex justify-center gap-5">
          <Button variant="success" size="sm" type="submit">
            Submit
          </Button>
          <Button onClick={handleReset} variant="danger" size="sm" type="button">
            <IconX /> Reset
          </Button>
        </div>
      </form>
    </div>
  );
};

export default EditPizzaPage;

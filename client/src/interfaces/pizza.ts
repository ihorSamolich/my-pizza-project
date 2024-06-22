import { ICategory } from "interfaces/category.ts";
import { IPhoto } from "interfaces/index.ts";
import { IIngredient } from "interfaces/ingredient.ts";

export interface IPizzaSize {
  id: number;
  sizeName: string;
  price: number;
}

export interface IPizza {
  id: number;
  name: string;
  description: string;
  rating: number;
  isAvailable: boolean;
  category: ICategory;
  photos: IPhoto[];
  ingredients: IIngredient[];
  sizes: IPizzaSize[];
  dateCreated: string;
}

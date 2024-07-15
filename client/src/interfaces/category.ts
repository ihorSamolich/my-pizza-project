import { IPaginationOptions } from "interfaces/pagination.ts";

export interface ICategory {
  id: number;
  name: string;
  image: string;
  numberOfPizzas: number;
}

export interface ICategoryCreate {
  name: string;
  image: File;
}

export interface ICategoryEdit {
  id: number;
  name: string | null;
  image: File | null;
}

export interface ICategoryPagedRequest extends IPaginationOptions {
  name?: string;
}

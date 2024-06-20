export interface ICategory {
  id: number;
  name: string;
  image: string;
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

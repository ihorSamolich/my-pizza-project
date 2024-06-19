export interface ICategory {
  id: number;
  name: string;
  image: string;
}

export interface ICategoryCreate {
  name: string;
  image: File;
}

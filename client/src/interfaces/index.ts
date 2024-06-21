export interface IPagedDataResponse<T> {
  data: T[];
  pagesAvailable: number;
  itemsAvailable: number;
}

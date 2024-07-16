import { IPizza } from "interfaces/pizza.ts";

interface ISizePrice {
  id: number;
  sizeId: number;
  sizeName: string;
  price: number;
}

export interface IOrderItem {
  pizzaId: number;
  pizzaSizeId: number;
  quantity: number;
  pizza: IPizza;
  sizePrice: ISizePrice;
}

export interface IOrder {
  id: number;
  totalAmount: number;
  deliveryAddress: string;
  isDelivery: boolean;
  dateCreated: string;
  orderItems: IOrderItem[];
}

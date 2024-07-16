import EmptyData from "components/EmptyData.tsx";
import TableCategoriesSkeleton from "components/skeletons/TableCategoriesSkeleton.tsx";
import { IOrder } from "interfaces/order.ts";
import { formatISODate } from "utils/formatISODate.ts";

import React from "react";

interface OrdersTableProps {
  orders: IOrder[] | undefined;
  pagesAvailable: number;
  isLoading: boolean;
}

const OrdersTable: React.FC<OrdersTableProps> = (props) => {
  const { orders, isLoading } = props;

  return (
    <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
      <table className="w-full text-sm font-bold text-left rtl:text-right text-gray-500 dark:text-gray-400">
        <thead className="text-xs text-gray-700 uppercase bg-gray-200 dark:bg-gray-700 dark:text-gray-400">
          <tr>
            <th scope="col" className="px-6 py-3">
              №
            </th>
            <th scope="col" className="px-6 py-3">
              Address
            </th>
            <th scope="col" className="px-6 py-3">
              Total
            </th>
            <th scope="col" className="px-6 py-3">
              Замовлення
            </th>
            <th scope="col" className="px-6 py-3">
              Дата
            </th>
          </tr>
        </thead>
        <tbody>
          {/* Loading skeleton */}
          {isLoading && <TableCategoriesSkeleton />}

          {/* Loaded data */}
          {orders?.map((order) => (
            <tr
              key={order.id}
              className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
            >
              <td className="px-6 py-4">{order.id}</td>
              <td className="px-6 py-4">{order.deliveryAddress || "Самовивіз"}</td>
              <td className="px-6 py-4">{order.totalAmount.toFixed() + " ₴"}</td>

              <td className="px-6 py-4">
                {order.orderItems.map((orderItem, index) => (
                  <p key={index}>
                    {orderItem.pizza.name} {orderItem.sizePrice.sizeName} см * {orderItem.quantity} шт. /{" "}
                    {orderItem.sizePrice.price.toFixed() + " ₴"}
                  </p>
                ))}
              </td>
              <td className="px-6 py-4">{formatISODate(order.dateCreated)}</td>

              {/*<td className="px-6 py-4 text-right space-x-5">*/}
              {/*  <Link to={`/ingredients/edit/${ingredient.id}`}>*/}
              {/*    <button className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</button>*/}
              {/*  </Link>*/}

              {/*  <button*/}
              {/*    // onClick={() => openDeleteConfirm(ingredient.id)}*/}
              {/*    className="font-medium text-red-600 dark:text-red-500 hover:underline"*/}
              {/*  >*/}
              {/*    Remove*/}
              {/*  </button>*/}
              {/*</td>*/}
            </tr>
          ))}
        </tbody>
      </table>

      {/* No data */}
      {!isLoading && !orders?.length && <EmptyData />}

      {/*/!*<Pagination totalPages={pagesAvailable} />*!/*/}

      {/*<ConfirmDialog*/}
      {/*  title="Confirm delete category?"*/}
      {/*  isOpen={isDeleteConfirmOpen}*/}
      {/*  close={() => setIsDeleteConfirmOpen(false)}*/}
      {/*  action={handleDelete}*/}
      {/*  actionProcessing={isDeleting}*/}
      {/*/>*/}
    </div>
  );
};

export default OrdersTable;

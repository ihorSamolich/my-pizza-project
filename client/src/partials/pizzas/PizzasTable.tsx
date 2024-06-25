import { IconCircleOff } from "@tabler/icons-react";
import TableCategoriesSkeleton from "components/skeletons/TableCategoriesSkeleton.tsx";
import { IPizza } from "interfaces/pizza.ts";
import { Link } from "react-router-dom";
import { API_URL } from "utils/envData.ts";

import React from "react";

interface PizzasTableProps {
  pizzas: IPizza[] | undefined;
  pagesAvailable: number;
  isLoading: boolean;
}
const PizzasTable: React.FC<PizzasTableProps> = (props) => {
  const { pizzas, isLoading } = props;

  return (
    <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
      <table className="w-full text-sm font-bold text-left rtl:text-right text-gray-500 dark:text-gray-400">
        <thead className="text-xs text-gray-700 uppercase bg-gray-200 dark:bg-gray-700 dark:text-gray-400">
          <tr>
            <th scope="col" className="px-6 py-3 w-24">
              <span className="sr-only">Image</span>
            </th>
            <th scope="col" className="px-6 py-3">
              Pizza name
            </th>
            <th scope="col" className="px-6 py-3">
              Category
            </th>
            <th scope="col" className="px-6 py-3">
              Description
            </th>
            <th scope="col" className="px-6 py-3">
              Ingredients
            </th>
            <th scope="col" className="px-6 py-3">
              Sizes
            </th>
            <th scope="col" className="px-6 py-3">
              Rating
            </th>
            <th scope="col" className="px-6 py-3">
              <span className="sr-only">Buttons</span>
            </th>
          </tr>
        </thead>
        <tbody>
          {/* Loading skeleton */}
          {isLoading && <TableCategoriesSkeleton />}

          {/* Loaded data */}
          {pizzas?.map((pizza) => (
            <tr
              key={pizza.id}
              className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
            >
              <th scope="row" className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                <img
                  src={`${API_URL}/images/200_${pizza.photos[0].name}`}
                  alt={pizza.name}
                  className="col-span-1 min-w-10 w-10 h-10 bg-gray-200 object-cover rounded-full"
                />
              </th>
              <td className="px-6 py-4">{pizza.name}</td>
              <td className="px-6 py-4 underline">{pizza.category.name}</td>
              <td className="px-6 py-4 italic font-extralight">{pizza.description}</td>
              <td className="px-6 py-4 italic">
                <ul className="flex flex-wrap gap-2">
                  {pizza.ingredients.map((ingredient) => (
                    <li key={ingredient.id} className="bg-gray-200 dark:bg-gray-700 rounded-full px-3 py-1 text-center text-sm">
                      {ingredient.name}
                    </li>
                  ))}
                </ul>
              </td>
              <td className="px-6 py-4 italic">
                <ul className="flex flex-wrap gap-2">
                  {pizza.sizes.map((size) => (
                    <li
                      key={size.id}
                      className="bg-gray-200 dark:bg-gray-700 rounded-full px-3 py-1 text-center font-extralight text-xs flex items-center gap-1 text-nowrap"
                    >
                      <IconCircleOff className="w-4 h-4" />
                      {`${size.sizeName} - ${size.price.toFixed(0)} грн`}
                    </li>
                  ))}
                </ul>
              </td>
              <td className="px-6 py-4">{pizza.rating.toFixed(2)}</td>

              <td className="px-6 py-4 text-right space-x-5">
                <Link to={`#`}>
                  <button className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</button>
                </Link>

                <button
                  // onClick={() => openDeleteConfirm(ingredient.id)}
                  className="font-medium text-red-600 dark:text-red-500 hover:underline"
                >
                  Remove
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/*<Pagination totalPages={pagesAvailable} />*/}

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

export default PizzasTable;

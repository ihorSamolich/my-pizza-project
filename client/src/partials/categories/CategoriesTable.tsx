import { useDeleteCategoryMutation } from "app/services/categoryService";
import ConfirmDialog from "components/ConfirmDialog";
import { ICategory } from "interfaces/category";
import { Link } from "react-router-dom";
import { API_URL } from "utils/envData.ts";

import React, { useState } from "react";

interface CategoriesTableProps {
  categories: ICategory[] | undefined;
  isData: boolean;
}

const CategoriesTable: React.FC<CategoriesTableProps> = (props) => {
  const { categories, isData } = props;

  const [isDeleteConfirmOpen, setIsDeleteConfirmOpen] = useState<boolean>(false);
  const [categoryIdToDelete, setCategoryIdToDelete] = useState<number | null>(null);

  const [deleteCategory, { isLoading: isDeleting }] = useDeleteCategoryMutation();

  const handleDelete = async () => {
    if (categoryIdToDelete !== null) {
      try {
        await deleteCategory(categoryIdToDelete).unwrap();
      } catch (err) {
        console.error("Failed to delete the category: ", err);
      }
      setIsDeleteConfirmOpen(false);
      setCategoryIdToDelete(null);
    }
  };

  const openDeleteConfirm = (id: number) => {
    setCategoryIdToDelete(id);
    setIsDeleteConfirmOpen(true);
  };

  return (
    <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
      <table className="w-full text-sm font-bold text-left rtl:text-right text-gray-500 dark:text-gray-400">
        <thead className="text-xs text-gray-700 uppercase bg-gray-200 dark:bg-gray-700 dark:text-gray-400">
          <tr>
            <th scope="col" className="px-6 py-3 w-24">
              <span className="sr-only">Image</span>
            </th>
            <th scope="col" className="px-6 py-3">
              Category name
            </th>
            <th scope="col" className="px-6 py-3">
              <span className="sr-only">Buttons</span>
            </th>
          </tr>
        </thead>
        <tbody>
          {/* Loading skeleton */}
          {isData &&
            Array.from({ length: 5 }).map((_, index) => (
              <tr
                key={index}
                className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600 animate-pulse"
              >
                <th scope="row" className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                  <div className=" min-w-10 w-10 h-10 bg-gray-200 rounded-full"></div>
                </th>
                <td className="px-6 py-4">
                  <div className="h-4 bg-gray-200 rounded w-1/3"></div>
                </td>
                <td className="px-6 py-4 text-right space-x-5">
                  <div className="inline-block h-4 bg-gray-200 rounded w-14"></div>
                  <div className="inline-block h-4 bg-gray-200 rounded w-14"></div>
                </td>
              </tr>
            ))}

          {/* Loaded data */}
          {categories?.map((category) => (
            <tr
              key={category.id}
              className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
            >
              <th scope="row" className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                <img
                  src={`${API_URL}/images/200_${category.image}`}
                  alt={category.name}
                  className="min-w-10 w-10 h-10 bg-gray-200 object-cover rounded-full"
                />
              </th>
              <td className="px-6 py-4">{category.name}</td>
              <td className="px-6 py-4 text-right space-x-5">
                <Link to={`/categories/edit/${category.id}`}>
                  <button className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</button>
                </Link>

                <button
                  onClick={() => openDeleteConfirm(category.id)}
                  className="font-medium text-red-600 dark:text-red-500 hover:underline"
                >
                  Remove
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <ConfirmDialog
        title="Confirm delete category?"
        isOpen={isDeleteConfirmOpen}
        close={() => setIsDeleteConfirmOpen(false)}
        action={handleDelete}
        actionProcessing={isDeleting}
      />
    </div>
  );
};

export default CategoriesTable;

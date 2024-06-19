import { IconCirclePlus } from "@tabler/icons-react";
import { useGetAllCategoriesQuery } from "app/services/categoryService.ts";
import { Button } from "components/ui/Button.tsx";
import { Link } from "react-router-dom";

import CategoriesTable from "../../partials/categories/CategoriesTable.tsx";
import WelcomeBanner from "../../partials/dashboard/WelcomeBanner.tsx";

const Categories = () => {
  const { data: categories, isLoading } = useGetAllCategoriesQuery();

  return (
    <div className="flex flex-col gap-4">
      <WelcomeBanner />
      <Link to="/categories/create" className="flex justify-end">
        <Button variant="primary" size="sm">
          <IconCirclePlus />
          Create Category
        </Button>
      </Link>
      <CategoriesTable categories={categories} isData={isLoading} />
    </div>
  );
};

export default Categories;

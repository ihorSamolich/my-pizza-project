import { IconCirclePlus } from "@tabler/icons-react";
import { useGetAllCategoriesQuery } from "app/services/categoryService.ts";
import LoadingSpinner from "components/LoadingSpinner.tsx";
import { Button } from "components/ui/Button.tsx";
import CategoriesTable from "partials/categories/CategoriesTable.tsx";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { Link } from "react-router-dom";

const CategoriesPage = () => {
  const { data: categories, isLoading } = useGetAllCategoriesQuery();

  if (isLoading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="flex flex-col gap-4">
      <WelcomeBanner title="List of Categories" description="Here you can view the list of our pizza categories." />
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

export default CategoriesPage;

import { IconCirclePlus } from "@tabler/icons-react";
import { useGetAllPizzasQuery } from "app/services/pizzaService.ts";
import { Button } from "components/ui/Button.tsx";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import PizzasTable from "partials/pizzas/PizzasTable.tsx";
import { Link } from "react-router-dom";

const PizzasPage = () => {
  const { data: pizzas, isLoading } = useGetAllPizzasQuery();

  console.log(pizzas);

  return (
    <div className="flex flex-col gap-4">
      <WelcomeBanner title="List of Pizzas" description="Here you can view the list of our pizzas." />

      <Link to={"#"} className="flex justify-end">
        <Button variant="primary" size="sm">
          <IconCirclePlus />
          Create New Pizza
        </Button>
      </Link>
      <PizzasTable pizzas={pizzas} isLoading={isLoading} pagesAvailable={0} />
    </div>
  );
};

export default PizzasPage;

import { IconCirclePlus } from "@tabler/icons-react";
import { Button } from "components/ui/Button.tsx";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import { Link } from "react-router-dom";

const PizzasPage = () => {
  return (
    <div className="flex flex-col gap-4">
      <WelcomeBanner title="List of Pizzas" description="Here you can view the list of our pizzas." />

      <Link to={"#"} className="flex justify-end">
        <Button variant="primary" size="sm">
          <IconCirclePlus />
          Create New Pizza
        </Button>
      </Link>
    </div>
  );
};

export default PizzasPage;

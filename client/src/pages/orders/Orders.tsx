import { useGetAllOrdersQuery } from "app/services/orderService.ts";
import WelcomeBanner from "partials/dashboard/WelcomeBanner.tsx";
import OrdersTable from "partials/orders/OrdersTable.tsx";
import { Helmet } from "react-helmet";

const OrdersPage = () => {
  const { data: orders, isLoading } = useGetAllOrdersQuery();

  return (
    <div className="flex flex-col gap-4">
      <Helmet>
        <title>{`MyPizza | Orders`}</title>
      </Helmet>
      <WelcomeBanner title="List of Orders" description="Here you can view the list of Orders." />
      <OrdersTable orders={orders} isLoading={isLoading} pagesAvailable={0} />
    </div>
  );
};

export default OrdersPage;

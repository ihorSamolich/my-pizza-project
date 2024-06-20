import WelcomeBanner from "../partials/dashboard/WelcomeBanner.tsx";

const Dashboard = () => {
  return (
    <>
      <WelcomeBanner
        title="Welcome to MyPizza"
        description="Where you can find the tastiest pizzas and create your favorite categories."
      />
      <h1>Dashboard page</h1>
    </>
  );
};

export default Dashboard;

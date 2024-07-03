import ProtectedRoute from "components/guards/ProtectedRoute.tsx";
import LoginPage from "pages/Login.tsx";
import RegisterPage from "pages/Register.tsx";
import Layout from "partials/layouts/Layout.tsx";
import { Route, Routes } from "react-router-dom";
import { lazyWithDelay } from "utils/lazyWithDelay.ts";

const Dashboard = lazyWithDelay(() => import("pages/Dashboard.tsx"));
const Settings = lazyWithDelay(() => import("pages/Settings.tsx"));
const CategoriesPage = lazyWithDelay(() => import("pages/categories/Categories.tsx"));
const CreateCategoryPage = lazyWithDelay(() => import("pages/categories/CreateCategory.tsx"));
const EditCategoryPage = lazyWithDelay(() => import("pages/categories/EditCategory.tsx"));
const PizzasPage = lazyWithDelay(() => import("pages/pizzas/Pizzas.tsx"));
const IngredientsPage = lazyWithDelay(() => import("pages/ingredients/Ingredients.tsx"));
const CreateIngredientPage = lazyWithDelay(() => import("pages/ingredients/CreateIngredient.tsx"));
const EditIngredientPage = lazyWithDelay(() => import("pages/ingredients/EditIngredient.tsx"));
const CreatePizzaPage = lazyWithDelay(() => import("pages/pizzas/CreatePizza.tsx"));
const EditPizzaPage = lazyWithDelay(() => import("pages/pizzas/EditPizza.tsx"));

const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route element={<ProtectedRoute />}>
          <Route index element={<Dashboard />} />
          <Route path="categories">
            <Route path="list" element={<CategoriesPage />} />
            <Route path="create" element={<CreateCategoryPage />} />
            <Route path="edit/:id" element={<EditCategoryPage />} />
          </Route>

          <Route path="pizzas">
            <Route path="list" element={<PizzasPage />} />
            <Route path="create" element={<CreatePizzaPage />} />
            <Route path="edit/:id" element={<EditPizzaPage />} />
          </Route>

          <Route path="ingredients">
            <Route path="list" element={<IngredientsPage />} />
            <Route path="create" element={<CreateIngredientPage />} />
            <Route path="edit/:id" element={<EditIngredientPage />} />
          </Route>

          <Route path="settings">
            <Route path="my-account" element={<Settings />} />
          </Route>
        </Route>

        <Route path="auth">
          <Route path="sign-in" element={<LoginPage />} />
          <Route path="register" element={<RegisterPage />} />
        </Route>
      </Route>
    </Routes>
  );
};

export default App;

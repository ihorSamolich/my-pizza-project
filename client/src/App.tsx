import { Route, Routes } from "react-router-dom";
import { lazyWithDelay } from "utils/lazyWithDelay.ts";

import Layout from "./partials/layouts/Layout.tsx";

const Dashboard = lazyWithDelay(() => import("pages/Dashboard.tsx"));
const Settings = lazyWithDelay(() => import("pages/Settings.tsx"));
const CategoriesPage = lazyWithDelay(() => import("pages/categories/Categories.tsx"));
const CreateCategoryPage = lazyWithDelay(() => import("pages/categories/CreateCategory.tsx"));
const EditCategoryPage = lazyWithDelay(() => import("pages/categories/EditCategory.tsx"));

const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Dashboard />} />

        <Route path="categories">
          <Route path="list" element={<CategoriesPage />} />
          <Route path="create" element={<CreateCategoryPage />} />
          <Route path="edit/:id" element={<EditCategoryPage />} />
        </Route>

        <Route path="settings">
          <Route path="my-account" element={<Settings />} />
        </Route>
      </Route>
    </Routes>
  );
};

export default App;

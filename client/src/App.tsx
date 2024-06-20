import CategoriesPage from "pages/categories/Categories.tsx";
import CreateCategoryPage from "pages/categories/CreateCategory.tsx";
import EditCategoryPage from "pages/categories/EditCategory.tsx";
import { Route, Routes } from "react-router-dom";

import Dashboard from "./pages/Dashboard.tsx";
import Settings from "./pages/Settings.tsx";
import Layout from "./partials/layouts/Layout.tsx";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Dashboard />} />

        <Route path="categories/">
          <Route path="list" element={<CategoriesPage />} />
          <Route path="create" element={<CreateCategoryPage />} />
          <Route path="edit/:id" element={<EditCategoryPage />} />
        </Route>

        <Route path="settings/">
          <Route path="my-account" element={<Settings />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;

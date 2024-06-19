import Categories from "pages/categories/Categories.tsx";
import CreateCategory from "pages/categories/CreateCategory.tsx";
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
          <Route path="list" element={<Categories />} />
          <Route path="create" element={<CreateCategory />} />
        </Route>

        <Route path="settings/">
          <Route path="my-account" element={<Settings />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;

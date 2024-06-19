import { Outlet } from "react-router-dom";

import { useState } from "react";

import Header from "../Header.tsx";
import Sidebar from "../Sidebar.tsx";

const Layout = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);

  return (
    <div className="flex h-screen overflow-hidden">
      {/* Sidebar */}
      <Sidebar sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />
      {/* Content area */}
      <div className="relative flex flex-col flex-1 overflow-y-auto overflow-x-hidden">
        {/*  Site header */}
        <Header sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />
        <main className="px-4 sm:px-6 lg:px-8 py-8 w-full max-w-9xl mx-auto">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default Layout;

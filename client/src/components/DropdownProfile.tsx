import { Menu, MenuButton, MenuItems, Transition } from "@headlessui/react";
import { logOut } from "app/slice/userSlice.ts";
import { useAppDispatch } from "app/store.ts";
import { IUser } from "interfaces/account.ts";
import { Link } from "react-router-dom";
import { API_URL } from "utils/envData.ts";

import React from "react";

import ChevronDown from "./sidebar/ChevronDown.tsx";

interface DropdownProfileProps {
  user: IUser;
}

const DropdownProfile: React.FC<DropdownProfileProps> = (props) => {
  const dispatch = useAppDispatch();
  const { user } = props;

  return (
    <Menu as="div" className="relative inline-block text-left">
      {({ open }) => (
        <>
          <MenuButton className="inline-flex justify-center items-center group">
            <img className="w-8 h-8 rounded-full" src={`${API_URL}/images/200_${user.photo}`} width="32" height="32" alt="User" />
            <div className="flex items-center truncate">
              <span className="truncate ml-2 text-sm font-medium dark:text-slate-300 group-hover:text-slate-800 dark:group-hover:text-slate-200">
                {user.firstName}
              </span>
              <ChevronDown open={open} />
            </div>
          </MenuButton>

          <Transition
            show={open}
            enter="transition ease-out duration-75"
            enterFrom="opacity-0 scale-95"
            enterTo="opacity-100 scale-100"
            leave="transition ease-in duration-100"
            leaveFrom="opacity-100 scale-100"
            leaveTo="opacity-0 scale-95"
          >
            <MenuItems
              static
              className="min-w-44 right-0 z-10 absolute bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 py-1.5 rounded shadow-lg overflow-hidden mt-3"
            >
              <div className="pb-2 px-3 mb-1 border-b border-slate-200 dark:border-slate-700">
                <div className="text-sm text-slate-800 dark:text-slate-100">{user.firstName + " " + user.lastName}</div>
                <div className="text-xs text-slate-500 dark:text-slate-400 italic">Administrator</div>
              </div>

              <Link
                className="font-medium text-sm text-indigo-500 hover:text-indigo-600 dark:hover:text-indigo-400 flex items-center py-1 px-3"
                to="/settings/my-account"
              >
                Settings
              </Link>

              <button
                className="font-medium text-sm text-indigo-500 hover:text-indigo-600 dark:hover:text-indigo-400 flex items-center py-1 px-3"
                onClick={() => dispatch(logOut())}
              >
                Sign Out
              </button>
            </MenuItems>
          </Transition>
        </>
      )}
    </Menu>
  );
};

export default DropdownProfile;

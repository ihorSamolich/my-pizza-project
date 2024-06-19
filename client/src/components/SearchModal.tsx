import { Dialog, DialogPanel, Transition, TransitionChild } from "@headlessui/react";
import { IconReceipt, IconSearch } from "@tabler/icons-react";
import { Link } from "react-router-dom";

import React, { useRef } from "react";

interface SearchModalProps {
  isOpen: boolean;
  close: () => void;
}

const SearchModal: React.FC<SearchModalProps> = (props) => {
  const { isOpen, close } = props;
  const searchInput = useRef<HTMLInputElement>(null);

  return (
    <Transition appear show={isOpen}>
      <Dialog as="div" className="relative z-10 focus:outline-none" onClose={close}>
        <div className="fixed inset-0 z-10 backdrop-opacity-20 w-screen overflow-y-auto">
          <div className="flex min-h-full items-center justify-center bg-black/50 p-4">
            <TransitionChild
              enter="transition ease-out duration-200"
              enterFrom="opacity-0"
              enterTo="opacity-100"
              leave="transition ease-out duration-100"
              leaveFrom="opacity-100"
              leaveTo="opacity-0"
            >
              <DialogPanel className="bg-white dark:bg-slate-800 border border-transparent dark:border-slate-700 overflow-auto max-w-2xl w-full max-h-full rounded-lg shadow-lg">
                {/* Search form */}
                <form className="border-b border-slate-200 dark:border-slate-700">
                  <div className="relative">
                    <label htmlFor="quick-search" className="sr-only">
                      Search
                    </label>
                    <input
                      id="quick-search"
                      className="w-full dark:text-slate-300 bg-white dark:bg-slate-800 border-0 focus:ring-transparent placeholder-slate-400 dark:placeholder-slate-500 appearance-none py-3 pl-10 pr-4"
                      type="search"
                      placeholder="Search Anything…"
                      ref={searchInput}
                    />
                    <button className="absolute inset-0 right-auto group" type="submit" aria-label="Search">
                      <svg
                        className="w-4 h-4 shrink-0 fill-current text-slate-400 dark:text-slate-500 group-hover:text-slate-500 dark:group-hover:text-slate-400 ml-4 mr-2"
                        viewBox="0 0 16 16"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path d="M7 14c-3.86 0-7-3.14-7-7s3.14-7 7-7 7 3.14 7 7-3.14 7-7 7zM7 2C4.243 2 2 4.243 2 7s2.243 5 5 5 5-2.243 5-5-2.243-5-5-5z" />
                        <path d="M15.707 14.293L13.314 11.9a8.019 8.019 0 01-1.414 1.414l2.393 2.393a.997.997 0 001.414 0 .999.999 0 000-1.414z" />
                      </svg>
                    </button>
                  </div>
                </form>
                <div className="py-4 px-2">
                  {/* Recent searches */}
                  <div className="mb-3 last:mb-0">
                    <div className="text-xs font-semibold text-slate-400 dark:text-slate-500 uppercase px-2 mb-2">
                      Recent searches
                    </div>
                    <ul className="text-sm">
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconSearch className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>Form Builder - 23 hours on-demand video</span>
                        </Link>
                      </li>
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconSearch className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>Access Mosaic on mobile and TV</span>
                        </Link>
                      </li>
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconSearch className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>Product Update - Q4 2021</span>
                        </Link>
                      </li>
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconSearch className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>Master Digital Marketing Strategy course</span>
                        </Link>
                      </li>
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconSearch className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>Dedicated forms for products</span>
                        </Link>
                      </li>
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconSearch className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>Product Update - Q4 2021</span>
                        </Link>
                      </li>
                    </ul>
                  </div>
                  {/* Recent pages */}
                  <div className="mb-3 last:mb-0">
                    <div className="text-xs font-semibold text-slate-400 dark:text-slate-500 uppercase px-2 mb-2">
                      Recent pages
                    </div>
                    <ul className="text-sm">
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconReceipt className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>
                            <span className="font-medium">Messages</span> -{" "}
                            <span className="text-slate-600 dark:text-slate-400 group-hover:text-white">
                              Conversation / … / Mike Mills
                            </span>
                          </span>
                        </Link>
                      </li>
                      <li>
                        <Link
                          className="flex items-center p-2 text-slate-800 dark:text-slate-100 hover:text-white hover:bg-indigo-500 rounded group"
                          to="#0"
                        >
                          <IconReceipt className="w-5 h-5 text-slate-400 dark:text-slate-500 group-hover:text-white group-hover:text-opacity-50 shrink-0 mr-3" />
                          <span>
                            <span className="font-medium">Messages</span> -{" "}
                            <span className="text-slate-600 dark:text-slate-400 group-hover:text-white">
                              Conversation / … / Eva Patrick
                            </span>
                          </span>
                        </Link>
                      </li>
                    </ul>
                  </div>
                </div>
              </DialogPanel>
            </TransitionChild>
          </div>
        </div>
      </Dialog>
    </Transition>
  );
};

export default SearchModal;

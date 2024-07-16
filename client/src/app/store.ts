import { configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";
import { accountApi } from "app/services/accountService.ts";
import { categoryApi } from "app/services/categoryService.ts";
import { ingredientApi } from "app/services/ingredientService.ts";
import { orderApi } from "app/services/orderService.ts";
import { pizzaApi } from "app/services/pizzaService.ts";
import { pizzaSizeApi } from "app/services/pizzaSizeService.ts";
import userReducer from "app/slice/userSlice.ts";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";

export const store = configureStore({
  reducer: {
    user: userReducer,
    [categoryApi.reducerPath]: categoryApi.reducer,
    [ingredientApi.reducerPath]: ingredientApi.reducer,
    [pizzaApi.reducerPath]: pizzaApi.reducer,
    [pizzaSizeApi.reducerPath]: pizzaSizeApi.reducer,
    [accountApi.reducerPath]: accountApi.reducer,
    [orderApi.reducerPath]: orderApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(
      categoryApi.middleware,
      accountApi.middleware,
      pizzaApi.middleware,
      ingredientApi.middleware,
      pizzaSizeApi.middleware,
      orderApi.middleware,
    ),
});

setupListeners(store.dispatch);

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

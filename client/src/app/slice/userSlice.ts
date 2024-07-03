import { createSlice } from "@reduxjs/toolkit";
import { IUser, IUserState } from "interfaces/account.ts";
import { jwtParser } from "utils/jwtParser.ts";
import { getLocalStorageItem } from "utils/localStorageUtils.ts";

const initialState: IUserState = {
  user: jwtParser(getLocalStorageItem("authToken")) as IUser,
  token: getLocalStorageItem("authToken") || null,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setCredentials: (state, action: { payload: { user: IUser; token: string } }) => {
      const { user, token } = action.payload;
      state.user = user;
      state.token = token;
    },
    logOut: (state) => {
      state.user = null;
      state.token = null;
      localStorage.removeItem("authToken");
    },
  },
});

export const getUser = (state: { user: IUserState }) => state.user.user;
export const getToken = (state: { user: IUserState }) => state.user.token;
export const { setCredentials, logOut } = userSlice.actions;
export default userSlice.reducer;

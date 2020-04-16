import { state } from "./mutations";

export const getResList = state => {
  return state.restaurantList;
};

export const getToken = state => {
  return state.tokenInfo.token;
};

import axios from "axios";
import router from "../router/index.js";
import { tip, toLogin } from "./utils.js";
import store from "../store/index.js";

const errorHandle = (status, msg) => {
  switch (status) {
    case 400:
      tip(msg);
      break;
    case 401:
      if (router.currentRoute != "/") {
        tip("登入逾時，請重新登入");
        localStorage.removeItem("tokenInfo");
        setTimeout(() => {
          toLogin();
        }, 1000);
      }
      break;
    case 404:
      tip(msg);
      break;
    default:
      tip(msg);
  }
};

var instance = axios.create({
  baseURL: "https://localhost:5001/api"
});
instance.interceptors.request.use(
  config => {
    config.withCredentials = true;
    const token = JSON.parse(JSON.stringify(store.state.member.tokenInfo)).token;
    config.headers = { Authorization: "Bearer " + token };
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);
instance.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    const { response } = error;
    if (response) {
      errorHandle(response.status, response.data);
      return Promise.reject(error);
    } else {
      if (!window.navigator.onLine) {
        tip("網路問題，請重新連線後刷新網頁");
      } else {
        return Promise.reject(error);
      }
    }
  }
);

export default function(method, url, data = null) {
  if (method == "post") {
    return instance.post(url, data);
  } else if (method == "get") {
    return instance.get(url, { params: data });
  } else if (method == "delete") {
    return instance.delete(url, { params: data });
  } else if (method == "put") {
    return instance.put(url, data);
  } else {
    console.log("error:" + method);
    return false;
  }
}

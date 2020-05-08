import request from "./https.js";

export const apiRegister = params =>
  request("post", "/Member/Register", params);

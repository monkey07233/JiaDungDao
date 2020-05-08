import request from "./https.js";

export const apiRegister = params =>
  request("post", "/Member/Register", params);

export const apiVerifyApplication = params =>
  request("post", "/Member/VerifyApplication?pass=" + params.pass + "&account=" +params.account);

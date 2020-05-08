import router from "../router/index.js";

export const tip = msg => {
  this.$bvToast.toast(msg, {
    title: `fail`,
    toaster: "b-toaster-top-center",
    solid: true,
    autoHideDelay: 2000,
    appendToast: false
  });
};

export const toLogin = () => {
  router.replace({
    name: "/"
  });
};

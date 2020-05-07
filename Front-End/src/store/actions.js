import * as types from "./mutations_type.js";
import axios from "axios";

export const getRestaurantList = ({ commit }) => {
    axios
        .get("https://localhost:5001/api/Restaurant/GetAllRestaurant")
        .then(function (res) {
            commit(types.GET_RESLIST, res.data);
        })
        .catch(function (err) {
            console.log(err);
        });
};

export const getRestaurantInfo = ({ commit }, id) => {
    axios
        .get("https://localhost:5001/api/Restaurant/GetRestaurantInfoById?id=" + id)
        .then(function (res) {
            commit(types.GET_RESINFO, res.data);
        })
        .catch(function (err) {
            console.log(err);
        });
};

export const createRestaurant = ({ commit, state }, newRestaurant) => {
    console.log(newRestaurant);
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Restaurant/createRestaurant",
                newRestaurant,
                config
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const getMemberInfo = ({ commit, state }, memberInfo) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Member/GetMemberInformation",
                memberInfo,
                config
            )
            .then(function (res) {
                commit(types.GET_MEMBERINFO, res.data);
            })
            .catch(function (err) {
                console.log(err);
                if (err.response.status == 401) {
                    localStorage.removeItem("tokenInfo");
                    commit(types.CLEAR_TOKEN);
                }
                reject();
            });
    });
};

export const register = ({ commit }, newMember) => {
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Member/Register", newMember)
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject(err);
            });
    });
};

export const login = ({ commit }, loginInfo) => {
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Member/Login", loginInfo)
            .then(function (res) {
                resolve(res.data);
                commit(types.SAVE_TOKEN, res.data);
            })
            .catch(function (err) {
                reject(err);
            });
    });
};

export const checkToken = ({ commit }, tokenInfo) => {
    commit(types.SAVE_TOKEN, tokenInfo);
};

export const logout = ({ commit }) => {
    commit(types.CLEAR_TOKEN);
};

export const UpdateProfile = ({ commit, state }, profileAfterEdit) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Member/EditMemberInformation",
                profileAfterEdit,
                config
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const addItemToShoppingCart = (context, order) => {
    return new Promise((resolve, reject) => {
        context.commit(types.SAVE_SHOPPINGCART, order);
        resolve();
    });
};
export const setCart = ({ commit }, cart) => {
    commit(types.SET_CART, cart);
};

export const deleteItemFromCart = ({ commit }, item) => {
    commit(types.DELETE_SHOPPINGCART, item);
};

export const minusItemToShoppingCart = (context, order) => {
    context.commit(types.MINUS_NUMBER_SHOPPINGCART, order);
};

export const addMenuItem = ({ commit, state }, newMenuItem) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Restaurant/AddMenuItem",
                JSON.parse(JSON.stringify(newMenuItem)),
                config
            )
            .then(function () {
                resolve();
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const deleteMenuItem = ({ commit, state }, menuID) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .get(
                "https://localhost:5001/api/Restaurant/DeleteMenu?MenuID=" + menuID,
                config
            )
            .then(function () {
                resolve();
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const UpdateRestaurant = ({ commit, state }, restaurant) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Restaurant/updateRestaurant",
                restaurant,
                config
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const updateMenu = ({ commit, state }, menu) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Restaurant/updateMenu", menu, config)
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const createOrder = ({ commit, state }, orderInfo) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Order/createOrder", orderInfo, config)
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};
export const getOrderInfo = ({ commit, state }, order) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Order/GetOrderInfo", order, config)
            .then(function (res) {
                commit(types.GET_ORDER, res.data);
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const getOrderInfoByResId = ({ commit, state }, ResId) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .get(
                "https://localhost:5001/api/Order/GetOrderInfoByResId?restaurantId=" +
                ResId,
                config
            )
            .then(function (res) {
                commit(types.GET_ORDER, res.data);
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const deleteRestaurant = ({ commit, state }, restaurantID) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .get(
                "https://localhost:5001/api/Restaurant/DeleteRestaurant?RestaurantID=" +
                restaurantID,
                config
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const getMenuId = ({ commit, state }) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .get("https://localhost:5001/api/Restaurant/GetLatestMenuId", config)
            .then(function (res) {
                resolve(res);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};
export const uploadImage = ({ commit, state }, formData) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Restaurant/uploadRestaurantImg",
                formData,
                config
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const uploadUserImage = ({ commit, state }, formData) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Member/UploadUserImg", formData, config)
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const VerifyAccount = ({ state }, m_account) => {
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Member/VerifyAccount", m_account)
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject();
            });
    });
};

export const UpdatePassword = ({ commit, state }, passwordInfo) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Member/UpdatePassword",
                JSON.parse(JSON.stringify(passwordInfo)),
                config
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err.response);
                reject(err);
            });
    });
};

export const SendResetPasswordMail = ({ commit }, reset_password) => {
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Member/SendResetPasswordMail",
                JSON.parse(JSON.stringify(reset_password))
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err.response);
                reject(err);
            });
    });
};

export const ResetPassword = ({ commit }, password) => {
    return new Promise((resolve, reject) => {
        axios
            .post(
                "https://localhost:5001/api/Member/ResetPassword",
                JSON.parse(JSON.stringify(password))
            )
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject(err);
            });
    });
};

export const applyResAdmin = ({ commit, state }, apply) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    return new Promise((resolve, reject) => {
        axios
            .post("https://localhost:5001/api/Member/ApplyResAdmin", apply, config)
            .then(function (res) {
                resolve(res.data);
            })
            .catch(function (err) {
                console.log(err);
                reject(err);
            });
    });
};

export const getAllApplication = ({ commit, state }) => {
    const config = {
        withCredentials: true,
        headers: {
            Authorization: "Bearer " + state.tokenInfo.token
        }
    };
    axios
        .get("https://localhost:5001/api/Member/GetAllApplication", config)
        .then(function (res) {
            commit(types.GET_APPLIST, res.data);
        })
        .catch(function (err) {
            console.log(err);
        });
};
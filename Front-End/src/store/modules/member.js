import * as types from "../mutations_type.js";
import axios from "axios";

export default {
  namespaced: true,
  state: {
    tokenInfo: {
      account: "",
      token: "",
      role: 0
    },
    memberInfo: {
      MemberId: "",
      m_name: "",
      m_birthday: "",
      m_email: "",
      m_address: "",
      m_imgUrl: ""
    },
    applicationList: [],
    memberList: []
  },

  getters: {
    getMemberInfo(state) {
      return state.memberInfo;
    },

    getTokenInfo(state) {
      return state.tokenInfo;
    },
    getAppList(state) {
      return state.applicationList;
    },
    getMemberList(state) {
      return state.memberList;
    }
  },

  mutations: {
    [types.SAVE_TOKEN](state, data) {
      state.tokenInfo = data;
    },
    [types.CLEAR_TOKEN](state) {
      state.tokenInfo.token = "";
      state.tokenInfo.account = "";
      state.tokenInfo.role = 0;
    },
    [types.GET_MEMBERINFO](state, data) {
      state.memberInfo = data;
      state.memberInfo.m_birthday = data.m_birthday.split("T")[0];
    },
    [types.GET_APPLIST](state, applications) {
      state.applicationList = applications;
    },
    [types.GET_MEMBERLIST](state, members) {
      state.memberList = members;
      state.memberList.forEach(member => {
        var birthday = member.m_birthday.split("T");
        member.m_birthday = birthday[0];
      });
    }
  },

  actions: {
    getMemberInfo({ commit, state }, memberInfo) {
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
    },

    login({ commit }, loginInfo) {
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
    },

    checkToken({ commit }, tokenInfo) {
      commit(types.SAVE_TOKEN, tokenInfo);
    },

    logout({ commit }) {
      commit(types.CLEAR_TOKEN);
    },

    updateProfile({ commit, state }, profileAfterEdit) {
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
    },
    uploadUserImage({ commit, state }, formData) {
      const config = {
        withCredentials: true,
        headers: {
          Authorization: "Bearer " + state.tokenInfo.token
        }
      };
      return new Promise((resolve, reject) => {
        axios
          .post(
            "https://localhost:5001/api/Member/UploadUserImg",
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
    },

    verifyAccount({ commit, state }, memberInfo) {
      return new Promise((resolve, reject) => {
        axios
          .post("https://localhost:5001/api/Member/VerifyAccount", memberInfo)
          .then(function (res) {
            resolve(res.data);
          })
          .catch(function (err) {
            console.log(err);
            reject();
          });
      });
    },

    updatePassword({ commit, state }, passwordInfo) {
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
            passwordInfo,
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
    },

    sendResetPasswordMail({ commit }, memberInfo) {
      return new Promise((resolve, reject) => {
        axios
          .post(
            "https://localhost:5001/api/Member/SendResetPasswordMail",
            memberInfo
          )
          .then(function (res) {
            resolve(res.data);
          })
          .catch(function (err) {
            console.log(err.response);
            reject(err);
          });
      });
    },

    resetPassword({ commit }, password) {
      return new Promise((resolve, reject) => {
        axios
          .post("https://localhost:5001/api/Member/ResetPassword", password)
          .then(function (res) {
            resolve(res.data);
          })
          .catch(function (err) {
            console.log(err);
            reject(err);
          });
      });
    },

    applyResAdmin({ commit, state }, apply) {
      const config = {
        withCredentials: true,
        headers: {
          Authorization: "Bearer " + state.tokenInfo.token
        }
      };
      return new Promise((resolve, reject) => {
        axios
          .post(
            "https://localhost:5001/api/Member/ApplyResAdmin",
            apply,
            config
          )
          .then(function (res) {
            resolve(res.data);
          })
          .catch(function (err) {
            console.log(err);
            reject(err);
          });
      });
    },
    getAllMember({ commit, state }) {
      const config = {
        withCredentials: true,
        headers: {
          Authorization: "Bearer " + state.tokenInfo.token
        }
      };
      axios
        .get("https://localhost:5001/api/Member/GetAllMember", config)
        .then(function (res) {
          commit(types.GET_MEMBERLIST, res.data);
        })
        .catch(function (err) {
          console.log(err);
        });
    },

    blockMember({ commit, state }, account) {
      const config = {
        withCredentials: true,
        headers: {
          Authorization: "Bearer " + state.tokenInfo.token
        }
      };
      axios
        .get(
          "https://localhost:5001/api/Member/BlockMember?m_account=" + account,
          config
        )
        .then(function (res) {
          console.log(res.data);
        })
        .catch(function (err) {
          console.log(err);
        });
    },
    getAllApplication({ commit, state }) {
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
    }
  }
}

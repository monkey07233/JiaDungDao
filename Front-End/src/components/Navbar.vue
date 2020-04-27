<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="dark">
      <b-navbar-brand to="/">JiaDungDao</b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav v-if="tokenInfo.role!=0">
          <b-nav-item to="/RestaurantManagement">餐廳管理</b-nav-item>
        </b-navbar-nav>
        <b-navbar-nav class="ml-auto">
          <!-- 登入前 -->
          <b-nav-item v-b-modal.login v-b-tooltip.hover title="Sign In" v-if="tokenInfo.token===''">
            <font-awesome-icon icon="user" />
          </b-nav-item>
          <!-- 登入後 -->
          <!--購物車-->
          <b-nav-item-dropdown size="lg" no-caret v-if="tokenInfo.token!==''">
            <template v-slot:button-content>
              <font-awesome-icon icon="shopping-cart" />
              <span
                class="fa-layers-counter"
                style="font-size:35px;background:Tomato;width:60px;height:60px"
              >{{shoppingCartNumber}}</span>
            </template>
            <div style="width:270px" class="p-2 text-center">
              <table role="table" class="table table-hover border table-sm">
                <thead role="rowgroup">
                  <tr role="row">
                    <th class="text-left">餐點</th>
                    <th class="text-center">數量</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody v-for="(item,index) in shoppingCart.shoppingCartItems" :key="index">
                  <tr>
                    <td class="text-left">{{item.o_item}}</td>
                    <td class="text-center">{{item.o_count}}</td>
                    <td class="text-center" @click="deleteCartItem(item)">
                      <font-awesome-icon  style="cursor:pointer;" icon="trash-alt"/>
                    </td>
                  </tr>
                </tbody>
              </table>
              <span>總金額 : ${{shoppingCart.shoppingCartTotalPrice}}</span>
              <b-button size="sm" class="mt-2" block to="/Cart" variant="info">
                <font-awesome-icon icon="credit-card" />&nbsp;結帳
              </b-button>
            </div>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown right v-if="tokenInfo.token!==''">
            <template v-slot:button-content>
              <img class="profile-img" src="../assets/images/user.png" />
              <span>{{memberInfo.m_name}}</span>
            </template>
            <b-dropdown-item to="/Profile"><font-awesome-icon icon="user"/>&nbsp;會員專區</b-dropdown-item>
            <b-dropdown-item @click.prevent="logout"><font-awesome-icon icon="sign-out-alt"/>&nbsp;登出</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
    <!-- login -->
    <b-modal id="login" ref="login" centered title="Sign In" hide-footer>
      <b-form @submit.prevent="login">
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="2">
            <label>帳號：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input required size="sm" placeholder="輸入你的帳號" v-model="loginInfo.m_account"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="2">
            <label>密碼：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input
              required
              size="sm"
              placeholder="輸入你的密碼"
              type="password"
              v-model="loginInfo.m_password"
            ></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-4 justify-content-center">
          <b-button pill size="sm" type="submit">登入</b-button>
        </b-row>
      </b-form>
      <hr />
      <b-row class="mb-2 mt-2 justify-content-center">
        還不是會員嗎？
        <b-link @click="showRegisterModal">註冊</b-link>
      </b-row>
    </b-modal>
    <!-- register -->
    <b-modal id="register" ref="register" centered title="Register" hide-footer>
      <b-form @submit.prevent="addMember">
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>帳號：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input required size="sm" v-model="newMember.m_account" placeholder="輸入你的帳號"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>密碼：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input
              required
              size="sm"
              v-model="newMember.m_password"
              placeholder="輸入你的密碼"
              type="password"
            ></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>確認密碼：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input
              required
              size="sm"
              :state="validation"
              v-model="confirmPassword"
              placeholder="確認你輸入的密碼"
              type="password"
            ></b-form-input>
            <b-form-invalid-feedback :state="validation">Wrong password.</b-form-invalid-feedback>
            <b-form-valid-feedback :state="validation"></b-form-valid-feedback>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>姓名：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input required size="sm" v-model="newMember.m_name" placeholder="輸入你的姓名"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>生日：</label>
          </b-col>
          <b-col sm="6">
            <b-form-datepicker
              required
              size="sm"
              v-model="newMember.m_birthday"
              placeholder="輸入你的生日"
            ></b-form-datepicker>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>信箱：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input
              required
              type="email"
              size="sm"
              v-model="newMember.m_email"
              placeholder="example@gmail.com"
            ></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-col sm="3">
            <label>地址：</label>
          </b-col>
          <b-col sm="6">
            <b-form-input required size="sm" v-model="newMember.m_address" placeholder="輸入你的地址"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-3 justify-content-center">
          <b-button pill size="sm" type="submit">送出</b-button>
        </b-row>
      </b-form>
      <hr />
      <b-row class="mb-2 mt-2 justify-content-center">
        <b-link @click="showLoginModal">回到登入頁面</b-link>
      </b-row>
    </b-modal>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      newMember: {
        m_name: "",
        m_account: "",
        m_password: "",
        m_birthday: "",
        m_email: "",
        m_address: ""
      },
      confirmPassword: "",
      loginInfo: {
        m_account: "",
        m_password: ""
      },
    };
  },
  computed: {
    validation() {
      return this.confirmPassword == this.newMember.m_password;
    },
    ...mapGetters({
      tokenInfo: "getTokenInfo",
      memberInfo: "getMemberInfo",
      shoppingCart: "getShoppingCartInfo",
      shoppingCartNumber:"getShoppingCartTotalNum"
    })
  },
  methods: {
    showRegisterModal() {
      this.$refs["login"].hide();
      this.$refs["register"].show();
    },
    showLoginModal() {
      this.$refs["register"].hide();
      this.$refs["login"].show();
    },
    addMember() {
      this.$store.dispatch("register", this.newMember).then(res => {
        this.$bvToast.toast(res, {
          title: `Registration`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
        if (res == "successed") {
          this.newMember.m_name = "";
          this.newMember.m_account = "";
          this.newMember.m_password = "";
          this.newMember.m_birthday = "";
          this.newMember.m_email = "";
          this.newMember.m_address = "";
          this.confirmPassword = "";
          this.showLoginModal();
        }
      });
    },
    login() {
      if (this.tokenInfo.token === "") {
        this.$store
          .dispatch("login", this.loginInfo)
          .then(res => {
            localStorage.setItem("tokenInfo", JSON.stringify(res));
            this.$bvToast.toast("登入成功", {
              title: `Login Success`,
              toaster: "b-toaster-top-center",
              solid: true,
              autoHideDelay: 1000,
              appendToast: false
            });
            this.$refs["login"].hide();
            this.$store.dispatch("getMemberInfo", {
              m_account: this.tokenInfo.account
            });
          })
          .catch(err => {
            if (err.response.status == 400) {
              this.$bvToast.toast(err.response.data, {
                title: `Login Fail`,
                toaster: "b-toaster-top-center",
                solid: true,
                autoHideDelay: 1000,
                appendToast: false
              });
            }
          });
      }
    },
    logout() {
      this.$store.dispatch("logout");
      localStorage.clear();
      this.$bvToast.toast("登出成功", {
        title: `Logout`,
        toaster: "b-toaster-top-center",
        solid: true,
        autoHideDelay: 1000,
        appendToast: false
      });
      this.$router.push("/");
    },
    deleteCartItem(item){
      this.$store.dispatch("deleteItemFromCart",[item]);
    }
  },
  created() {
    if (this.tokenInfo.token != "") {
      this.$store.dispatch("getMemberInfo", {
        m_account: this.tokenInfo.account
      });
    }
    const cart = JSON.parse(localStorage.getItem("shpopingCart"));
    if(cart!=null){
      this.$store.dispatch("setCart",cart)
    }
  }
};
</script>

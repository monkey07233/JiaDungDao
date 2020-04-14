<template>
  <nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-dark">
    <a class="navbar-brand" href="#">JiaDungDao</a>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item">
          <a class="nav-link" href="#">
            Home
            <span class="sr-only">(current)</span>
          </a>
        </li>
      </ul>
    </div>
    <div class="ml-auto">
      <ul class="navbar-nav ml-auto">
        <li class="nav-item">
          <a class="nav-link" v-b-modal.login v-b-tooltip.hover title="Sign In">
            <font-awesome-icon icon="user" />
          </a>
        </li>
      </ul>
    </div>
    <b-modal id="login" ref="login" centered title="Sign In" hide-footer>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>帳號:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" placeholder="輸入你的帳號"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>密碼:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" placeholder="輸入你的密碼" type="password"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2 justify-content-center">
        <b-button pill size="sm">登入</b-button>
      </b-row>
      <hr />
      <b-row class="mb-2 mt-2 justify-content-center">
        還不是會員嗎？
        <b-link @click="showRegisterModal">註冊</b-link>
      </b-row>
    </b-modal>
    <b-modal id="register" ref="register" centered title="Register" hide-footer>
      <b-form @submit.prevent="addMember">
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>帳號:</label>
          </b-col>
          <b-col sm="9">
            <b-form-input required size="sm" v-model="newMember.m_account" placeholder="輸入你的帳號"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>密碼:</label>
          </b-col>
          <b-col sm="9">
            <b-form-input
              required
              size="sm"
              v-model="newMember.m_password"
              placeholder="輸入你的密碼"
              type="password"
            ></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>確認密碼:</label>
          </b-col>
          <b-col sm="9">
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
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>姓名:</label>
          </b-col>
          <b-col sm="9">
            <b-form-input required size="sm" v-model="newMember.m_name" placeholder="輸入你的姓名"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>生日:</label>
          </b-col>
          <b-col sm="9">
            <b-form-datepicker
              required
              size="sm"
              v-model="newMember.m_birthday"
              placeholder="輸入你的生日"
            ></b-form-datepicker>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>信箱:</label>
          </b-col>
          <b-col sm="9">
            <b-form-input
              required
              type="email"
              size="sm"
              v-model="newMember.m_email"
              placeholder="example@gmail.com"
            ></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2">
          <b-col sm="3">
            <label>地址:</label>
          </b-col>
          <b-col sm="9">
            <b-form-input required size="sm" v-model="newMember.m_address" placeholder="輸入你的地址"></b-form-input>
          </b-col>
        </b-row>
        <b-row class="mb-2 mt-2 justify-content-center">
          <b-button pill size="sm" type="submit">送出</b-button>
        </b-row>
      </b-form>
      <hr />
      <b-row class="mb-2 mt-2 justify-content-center">
        <b-link @click="showLoginModal">回到登入頁面</b-link>
      </b-row>
    </b-modal>
  </nav>
</template>

<script>
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
      confirmPassword: ""
    };
  },
  computed: {
    validation() {
      return this.confirmPassword == this.newMember.m_password;
    }
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
        alert(res);
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
    }
  }
};
</script>
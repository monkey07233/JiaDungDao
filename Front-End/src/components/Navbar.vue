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
          <a class="nav-link" href="#" v-b-modal.login v-b-tooltip.hover title="Sign In">
            <font-awesome-icon icon="sign-in-alt" />
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
      <hr>
      <b-row class="mb-2 mt-2 justify-content-center">
      還不是會員嗎? | <b-link @click="showRegisterModal"> 註冊</b-link>
      </b-row>
    </b-modal>
    <b-modal id="register" ref="register" centered title="Register" hide-footer>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>帳號:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" v-model="newMember.account" placeholder="輸入你的帳號"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>密碼:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" v-model="newMember.password" placeholder="輸入你的密碼" type="password"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>確認密碼:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" placeholder="確認你輸入的密碼" type="password"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>姓名:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" v-model="newMember.name" placeholder="輸入你的姓名"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>生日:</label>
        </b-col>
        <b-col sm="9">
          <b-form-datepicker size="sm" v-model="newMember.birthday" placeholder="輸入你的生日"></b-form-datepicker>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>信箱:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" v-model="newMember.email" placeholder="輸入你的信箱"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2">
        <b-col sm="3">
          <label>地址:</label>
        </b-col>
        <b-col sm="9">
          <b-form-input size="sm" v-model="newMember.address" placeholder="輸入你的地址"></b-form-input>
        </b-col>
      </b-row>
      <b-row class="mb-2 mt-2 justify-content-center">
        <b-button pill size="sm" @click="showMember">送出</b-button>
      </b-row>
      <b-row class="mb-2 mt-2 justify-content-center">
        <b-link @click="showLoginModal">去登入</b-link>
      </b-row>
    </b-modal>
  </nav>
</template>

<script>
export default {
  data(){
    return{
      newMember:{
        name:'',
        account:'',
        password:'',
        birthday:'',
        email:'',
        address:''
      }
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
    showMember(){
      this.$store.dispatch('memberRegistrationInformation', this.newMember);
    }
  }
};
</script>
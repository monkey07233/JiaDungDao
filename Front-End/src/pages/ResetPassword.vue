<template>
  <div class="container rounded bg-white" style="width:40%">
    <div class="row ml-3 mr-3 pt-3 justify-content-center">
      <h4 class="form-title">
        <font-awesome-icon icon="key" />&nbsp;重設密碼
      </h4>
    </div>
    <hr />
    <div class="row">
      <b-form class="col-12" @submit.prevent="ResetPassword">
        <b-form-group label="新密碼:" label-for="input-1">
          <b-form-input
            id="input-1"
            type="password"
            v-model="password.new_password"
            placeholder="請輸入新密碼"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group label="確認密碼:" label-for="input-2">
          <b-form-input
            id="input-2"
            type="password"
            v-model="confirmPassword"
            placeholder="請輸入新密碼"
            required
            :state="confirm_password"
            aria-describedby="confirm-feedback"
            trim
          ></b-form-input>
          <b-form-invalid-feedback id="confirm-feedback">與新密碼不相符</b-form-invalid-feedback>
        </b-form-group>
        <div class="row mb-4 justify-content-center">
          <b-button type="submit" variant="info" class="submit-btn">送出</b-button>
        </div>
      </b-form>
    </div>
  </div>
</template>

<script>
import "@/assets/css/index.css";
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      password: {
        m_account: "",
        new_password: ""
      },
      confirmPassword: ""
    };
  },
  computed: {
    confirm_password() {
      return this.confirmPassword === this.password.new_password ? true : false;
    }
  },
  methods: {
    ResetPassword() {
      this.password.m_account = this.$route.query.account;
      this.$store
        .dispatch("member/ResetPassword", this.password)
        .then(res => {
          this.$bvToast.toast("重設密碼成功，請重新登入", {
            title: `重設密碼成功`,
            toaster: "b-toaster-top-center",
            solid: true,
            autoHideDelay: 2000,
            appendToast: false
          });
          setTimeout(() => {
            this.$router.push("/");
          }, 2000);
        })
        .catch(err => {
          if (err.response.status == 400) {
            this.$bvToast.toast(err.response.data, {
              title: `重設密碼失敗`,
              toaster: "b-toaster-top-center",
              solid: true,
              autoHideDelay: 2000,
              appendToast: false
            });
          }
        });
    }
  },
  created() {
    var param = this.$route.query.account;
    if (param == undefined || param == null) {
      this.$router.push("Index");
    }
  }
};
</script>
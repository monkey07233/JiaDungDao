<template>
  <div class="container rounded bg-white" style="width:40%">
    <div class="row ml-3 mr-3 pt-3 justify-content-center">
      <h4 class="form-title">
        <font-awesome-icon icon="key" />&nbsp;重設密碼
      </h4>
    </div>
    <hr />
    <div class="row">
      <b-form class="col-12" @submit.prevent>
        <b-form-group label="新密碼:" label-for="input-1">
          <b-form-input
            id="input-1"
            type="text"
            v-model="password.new_password"
            placeholder="請輸入新密碼"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group label="確認密碼:" label-for="input-2">
          <b-form-input
            id="input-2"
            type="text"
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
export default {
  data() {
    return {
      password: {
        new_password: ""
      },
      confirmPassword: ""
    };
  },
  computed: {
    confirm_password() {
      return this.confirmPassword === this.password.new_password ? true : false;
    }
  }
};
</script>
<template>
  <div class="container">
    <b-button class="mb-4 font-weight-bold" @click="back" variant="warning">回到上一頁</b-button>
    <div class="container rounded bg-white mb-4">
      <div class="row ml-3 mr-3 pt-3">
        <h4 class="form-title">
          <font-awesome-icon icon="plus" />&nbsp;新增餐廳
        </h4>
      </div>
      <hr />
      <div class="row">
        <b-form class="col-12" @submit.prevent="createRestaurant">
          <b-form-group label="餐廳名稱" label-for="input-1">
            <b-form-input
              id="input-1"
              type="text"
              v-model="newRestaurant.r_name"
              placeholder="請輸入餐廳名稱"
              required
            ></b-form-input>
          </b-form-group>
          <b-form-group label="餐廳地址：" label-for="input-2">
            <b-form-input
              id="input-2"
              type="text"
              v-model="newRestaurant.r_address"
              placeholder="請輸入餐廳地址"
              required
            ></b-form-input>
          </b-form-group>
          <b-form-group label="聯絡電話" label-for="input-3">
            <b-form-input
              id="input-3"
              type="tel"
              v-model="newRestaurant.r_tel"
              placeholder="請輸入餐廳聯絡電話"
              required
            ></b-form-input>
          </b-form-group>
          <b-form-group label="餐廳圖片" label-for="input-3">
            <b-form-file v-model="resImage" placeholder="請上傳餐廳圖片" required></b-form-file>
          </b-form-group>
          <b-button type="submit" variant="primary" class="submit-btn">送出</b-button>
        </b-form>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      newRestaurant: {
        r_name: "",
        r_address: "",
        r_tel: "",
        m_account: ""
      },
      resImage: null,
      formData: new FormData()
    };
  },
  computed: {
    ...mapGetters({
      tokenInfo: "getTokenInfo",
      MemberInfo: "getMemberInfo"
    })
  },
  methods: {
    createRestaurant() {
      this.newRestaurant.m_account = this.tokenInfo.account;
      this.$store.dispatch("createRestaurant", this.newRestaurant).then(res => {
        this.formData.append("files", this.resImage);
        this.formData.append("uploadType", 0);
        this.formData.append("RestaurantID", res);
        this.formData.append("r_name", this.newRestaurant.r_name);
        this.$store.dispatch("uploadImage", this.formData).then(res2 => {
          this.$bvToast.toast("新增餐廳成功", {
            title: `successed`,
            toaster: "b-toaster-top-center",
            solid: true,
            autoHideDelay: 1000,
            appendToast: false
          });
          return setTimeout(() => {
            this.$router.push("/RestaurantManagement");
          }, 1000);
        });
      });
    },
    back() {
      this.$router.back();
    }
  }
};
</script>

<style >
.form-title {
  margin-left: auto;
  margin-right: auto;
}

.submit-btn {
  margin-bottom: 12px;
}
</style>

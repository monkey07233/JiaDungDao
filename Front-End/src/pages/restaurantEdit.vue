<template>
  <div class="container">
    <b-button class="mb-4 font-weight-bold" @click="back" variant="warning">回到上一頁</b-button>
    <div class="rounded bg-white mb-4">
      <div class="row">
        <div class="col-12 mt-3 mb-5">
          <div>
            <b-form ref="form" @submit.prevent="UpdateRestaurant">
              <b-alert show variant="white">
                <h2 class="alert-heading mb-2 text-center">編輯餐廳資料</h2>
                <hr />
                <p class="mb-2">
                  <span class="item-text">餐廳照片：</span>
                  <input type="file" />
                </p>
                <p class="mb-2">
                  <span class="item-text">餐廳名稱：</span>
                  <input
                    name="r_name"
                    required
                    :value="restaurantInfo.restaurant.r_name"
                    type="text"
                  />
                </p>
                <p class="mb-2">
                  <span class="item-text">餐廳地址：</span>
                  <input
                    name="r_address"
                    required
                    :value="restaurantInfo.restaurant.r_address"
                    type="text"
                  />
                </p>
                <p class="mb-2">
                  <span class="item-text">餐廳電話：</span>
                  <input name="r_tel" required :value="restaurantInfo.restaurant.r_tel" type="text" />
                </p>
              </b-alert>
              <div class="text-center">
                <button type="submit" class="btn btn-info">儲存編輯</button>
              </div>
            </b-form>
          </div>
        </div>
      </div>
    </div>
    <div class="rounded bg-white mb-4">
      <div class="row">
        <div class="col-12 mt-3 mb-5">
          <div>
            <b-alert show variant="white">
              <h2 class="alert-heading mb-2 text-center">編輯餐廳菜單</h2>

              <b-form @submit.prevent="addMenuItem" inline class="justify-content-center">
                <label class="sr-only" for="input-name">菜名</label>
                <b-input
                  v-model="newMenuItem.m_item"
                  name="input-name"
                  class="mb-2 mr-sm-2 mb-sm-0"
                  placeholder="請輸入菜名"
                  required
                ></b-input>
                <label class="sr-only" for="input-price">價錢</label>
                <b-input
                  v-model="newMenuItem.m_price"
                  id="input-price"
                  class="mb-2 mr-sm-2 mb-sm-0"
                  placeholder="請輸入價錢"
                  required
                ></b-input>
                <label class="sr-only" for="input-type">分類</label>
                <b-input
                  v-model="newMenuItem.m_type"
                  id="input-type"
                  class="mb-2 mr-sm-2 mb-sm-0"
                  placeholder="請輸入分類"
                  required
                ></b-input>
                <b-button type="submit" variant="success">新增</b-button>
              </b-form>
              <table class="table mt-2">
                <thead class="thead-light">
                  <tr>
                    <th>#</th>
                    <th>菜名</th>
                    <th>價錢</th>
                    <th>分類</th>
                  </tr>
                  <template v-for="(items,index) in restaurantInfo.typeAndMenu">
                    <tr v-for="(menu,m_index) in items.menu" :key="m_index">
                      <td>{{index+1}}</td>
                      <td>{{menu.m_item}}</td>
                      <td>{{menu.m_price}}</td>
                      <td>{{menu.m_type}}</td>
                    </tr>
                  </template>
                </thead>
              </table>
            </b-alert>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      newMenuItem: {
        RestaurantID: this.$route.params.id,
        m_item: "",
        m_type: "",
        m_price: null
      }
    };
  },
  computed: {
    ...mapGetters({
      restaurantInfo: "getResInfo",
      tokenInfo: "getTokenInfo"
    })
  },
  created() {
    this.$store.dispatch("getRestaurantInfo", this.$route.params.id);
  },
  methods: {
    UpdateRestaurant() {
      var restaurant = {
        RestaurantID: this.$route.params.id,
        r_name: this.$refs.form.r_name.value,
        r_address: this.$refs.form.r_address.value,
        r_tel: this.$refs.form.r_tel.value,
        m_account: this.tokenInfo.account
      };
      this.$store.dispatch("UpdateRestaurant", restaurant).then(res => {
        this.$bvToast.toast("更新餐廳資訊成功", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
        this.$store.dispatch("getRestaurantInfo", this.$route.params.id);
      });
    },
    addMenuItem() {
      this.newMenuItem.m_price = parseInt(this.newMenuItem.m_price);
      this.$store.dispatch("addMenuItem", this.newMenuItem).then(res => {
        this.$bvToast.toast("新增餐點成功", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
        this.newMenuItem.m_item = "";
        this.newMenuItem.m_type = "";
        this.newMenuItem.m_price = null;
        this.$store.dispatch("getRestaurantInfo", this.$route.params.id);
      });
    },
    back(){
      this.$router.back();
    }
  }
};
</script>

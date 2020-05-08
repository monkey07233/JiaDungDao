<template>
  <div class="container">
      <div class="mb-5">
    <div class="d-flex">
      <b-button class="mb-4 font-weight-bold" @click="back" variant="warning">回到上一頁</b-button>
      <h2 class="ml-auto">{{restaurantInfo.restaurant.r_name}}</h2>
      <b-button class="mb-4 ml-auto" @click="deleteRestaurant(resId)" variant="danger">刪除此餐廳</b-button>
    </div>
    <div>
      <b-card no-body>
        <b-tabs card fill>
          <b-tab title="訂單列表" active>
            <restaurantOrder />
          </b-tab>
          <b-tab title="編輯餐廳資料">
            <restaurantEdit />
          </b-tab>
        </b-tabs>
      </b-card>
    </div>
    </div>
  </div>
</template>

<script>
import restaurantEdit from "./restaurantEdit";
import restaurantOrder from "./restaurantOrder";
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      resId: this.$route.params.id
    };
  },
  components: {
    restaurantEdit,
    restaurantOrder
  },
  computed: {
    ...mapGetters({
      restaurantInfo: "restaurant/getResInfo",
      tokenInfo: "member/getTokenInfo"
    })
  },
  created() {
    this.$store.dispatch("restaurant/getRestaurantInfo", this.$route.params.id);
  },
  methods: {
    deleteRestaurant(ResID) {
      console.log(ResID);
      this.$store.dispatch("restaurant/deleteRestaurant", ResID).then(res => {
        this.$bvToast.toast("成功刪除餐廳", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
        setTimeout(() => {
          this.$router.push("/RestaurantManagement");
        }, 1500);
      });
    },
    back() {
      this.$router.back();
    }
  }
};
</script>
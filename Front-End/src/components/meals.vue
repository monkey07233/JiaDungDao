<template>
  <b-tab :title="tabTitle" active>
    <div class="row">
      <div class="ml-3 mb-3" v-for="(item,index) in list" :key="index">
        <div>
          <b-card no-body class="overflow-hidden" style="max-width: 359px;">
            <b-row no-gutters>
              <b-col md="5">
                <b-card-img
                  :src="require('../../../Back-End/File/Menu/' + item.m_imgUrl)"
                  alt="Image"
                  class="rounded-0"
                  width="148.75"
                  height="140.67"
                ></b-card-img>
              </b-col>
              <b-col md="7">
                <b-card-body title-tag="h5" :title="item.m_item">
                  <b-card-text text-tag="h6">${{item.m_price}}</b-card-text>
                  <b-button @click="addToShoppingCart(item)" variant="outline-primary">加入購物車</b-button>
                </b-card-body>
              </b-col>
            </b-row>
          </b-card>
        </div>
      </div>
    </div>
  </b-tab>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters({
      tokenInfo: "getTokenInfo"
    })
  },
  data() {
    return {};
  },
  props: {
    tabTitle: String,
    list: Array,
    resName: String
  },
  methods: {
    addToShoppingCart: function(item) {
      let order = {
        r_id: item.restaurantID,
        r_name: this.resName,
        o_item: item.m_item,
        o_price: item.m_price,
        o_count: 1
      };
      this.$store.dispatch("addItemToShoppingCart", order).then(res => {
        this.$bvToast.toast("餐點已加入購物車", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
      });
    }
  }
};
</script>

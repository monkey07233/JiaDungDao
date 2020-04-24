<template>
  <b-tab :title="tabTitle" active>
    <div class="row">
      <div class="ml-3 mb-3" v-for="(item,index) in list" :key="index">
        <div>
          <b-card no-body class="overflow-hidden" style="max-width: 359px;">
            <b-row no-gutters>
              <b-col md="5">
                <b-card-img
                  src="https://picsum.photos/400/400/?image=20"
                  alt="Image"
                  class="rounded-0"
                ></b-card-img>
              </b-col>
              <b-col md="7">
                <b-card-body title-tag="h5" :title="item.m_item">
                  <b-card-text text-tag="h6">${{item.m_price}}</b-card-text>
                  <b-button
                    @click="addToShoppingCart(item.m_item, item.m_price)"
                    variant="outline-primary"
                  >加入購物車</b-button>
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
      shoppingCart: "getShoppingCartInfo"
    })
  },
  data() {
    return {};
  },
  props: {
    tabTitle: String,
    list: Array
  },
  methods: {
    addToShoppingCart: function(m_item, m_price) {
      let item = {
        name: m_item,
        price: m_price,
        number: 1,
        subtotal: m_price
      };
      this.$store.dispatch("addItemToShoppingCart", item).then(res => {
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

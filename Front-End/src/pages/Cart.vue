<template>
  <div class="container rounded bg-white mb-4">
    <div class="row ml-3 mr-3 pt-3">
      <h4>
        <font-awesome-icon icon="shopping-bag" />&nbsp;購物清單
      </h4>
    </div>
    <div class="row mt-3 ml-3 mr-3">
      <b-table-simple hover outlined v-for="(items,indexs) in GroupBy" :key="indexs">
        <b-thead>
          <b-tr>
            <b-td colspan="5" style="text-align:center;">{{indexs}}</b-td>
          </b-tr>
          <b-tr>
            <b-td style="width:400px;">餐點名稱</b-td>
            <b-td>數量</b-td>
            <b-td>單價</b-td>
            <b-td>小計</b-td>
            <b-td>刪除</b-td>
          </b-tr>
        </b-thead>
        <b-tbody>
          <b-tr v-for="(item,index) in items" :key="index">
            <b-td style="width:400px;">{{item.o_item}}</b-td>
            <b-td>
              <b-button
                :disabled="item.o_count==1"
                @click="minusItemToShoppingCart(item)"
                pill
                size="sm"
                variant="outline-secondary"
              >
                <font-awesome-icon icon="minus" />
              </b-button>
              &nbsp;{{item.o_count}}&nbsp;
              <b-button
                @click="addItemToShoppingCart(item)"
                pill
                size="sm"
                variant="outline-danger"
              >
                <font-awesome-icon icon="plus" />
              </b-button>
            </b-td>
            <b-td>{{item.o_price}}</b-td>
            <b-td>{{item.o_count*item.o_price}}</b-td>
            <b-td>
              <font-awesome-icon style="cursor:pointer;" icon="trash-alt" @click="deleteCartItem(item)" />
            </b-td>
          </b-tr>
          <b-tr class="text-center">
            <b-td colspan="2">
              <h5>
                訂單金額
                <span class="ml-2">${{calculateOrderSubtotal(indexs)}}</span>
              </h5>
            </b-td>
            <b-td colspan="3">
              <b-button
                @click="checkout(indexs, calculateOrderSubtotal(indexs))"
                block
                variant="info"
              >
                <font-awesome-icon icon="credit-card" />&nbsp;結帳
              </b-button>
            </b-td>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </div>
    <div class="row mb-3">
      <div class="col-10">
        <h4 class="text-right mr-3">總金額</h4>
      </div>
      <div class="col-2 totalPrice">
        <h4>${{shoppingCart.shoppingCartTotalPrice}}</h4>
      </div>
    </div>
    <div class="row p-3">
      <div class="col-12">
        <router-link
          class="btn btn-secondary btn-block"
          style="color:white;text-decoration:none;"
          to="/"
        >
          <font-awesome-icon icon="store" />&nbsp;繼續購物
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import axios from "axios";
export default {
  computed: {
    ...mapGetters({
      shoppingCart: "getShoppingCartInfo",
      tokenInfo: "getTokenInfo"
    }),
    GroupBy() {
      const result = {};
      this.shoppingCart.shoppingCartItems.forEach(item => {
        if (!result[item["r_name"]]) result[item["r_name"]] = [];
        result[item["r_name"]].push(item);
      });
      return result;
    }
  },
  data() {
    return {};
  },
  methods: {
    addItemToShoppingCart(item) {
      this.$store.dispatch("addItemToShoppingCart", item);
    },
    minusItemToShoppingCart: function(item) {
      this.$store.dispatch("minusItemToShoppingCart", item);
    },
    deleteCartItem(item) {
      this.$store.dispatch("deleteItemFromCart", [item]);
    },
    calculateOrderSubtotal(resName) {
      let sum = 0;
      this.shoppingCart.shoppingCartItems.forEach(item => {
        if (item.r_name === resName) {
          sum += item.o_count * item.o_price;
        }
      });
      return sum;
    },
    checkout(resName, totalPrice) {
      let deleteItems=[];
      let orderInfo = {
        title: {
          RestaurantID: null,
          r_name: resName,
          m_account: this.tokenInfo.account,
          o_total: totalPrice
        },
        orderDetail: []
      };
      this.shoppingCart.shoppingCartItems.forEach(list => {
        if (list.r_name === resName) {
          orderInfo.title.RestaurantID = list.r_id;
          let item = {
            o_item: list.o_item,
            o_price: list.o_price,
            o_count: list.o_count
          };
          orderInfo.orderDetail.push(item);
          deleteItems.push(list);
        }
      });
      this.$store.dispatch("createOrder", orderInfo).then(res =>{
        this.$store.dispatch("deleteItemFromCart", deleteItems);
        this.$bvToast.toast("訂單已成立，可至會員專區→訂單查詢查看訂單", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 2000,
          appendToast: false
        });
        setTimeout(() => {
        this.$router.push("/");
        }, 2000);
      });
    }
  }
};
</script>

<style>
.totalPrice {
  color: red;
}
</style>

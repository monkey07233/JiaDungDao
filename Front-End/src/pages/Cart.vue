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
            <b-td colspan="4" style="text-align:center;">{{indexs}}</b-td>
          </b-tr>
          <b-tr>
            <b-td style="width:400px;">餐點名稱</b-td>
            <b-td>數量</b-td>
            <b-td>單價</b-td>
            <b-td>小計</b-td>
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
          </b-tr>
        </b-tbody>
      </b-table-simple>
        <!-- <template v-slot:cell(del)="data">
          <font-awesome-icon icon="trash-alt" @click="deleteCartItem(data.item)"/>
        </template> -->
    </div>
    <div class="row mb-3">
      <div class="col-10">
        <h5 class="text-right mr-3">訂單總金額</h5>
      </div>
      <div class="col-2 totalPrice">${{shoppingCart.shoppingCartTotalPrice}}</div>
    </div>

    <div class="row p-3">
      <div class="col-6">
        <router-link
          class="btn btn-secondary btn-block"
          style="color:white;text-decoration:none;"
          to="/"
        >
          <font-awesome-icon icon="store" />&nbsp;繼續購物
        </router-link>
      </div>
      <div class="col-6">
        <router-link
          class="btn btn-info btn-block"
          style="color:white;text-decoration:none;"
          to="/"
        >
          <font-awesome-icon icon="credit-card" />&nbsp;結帳
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
      shoppingCart: "getShoppingCartInfo"
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
    return {
      fields: [
        { key: "o_item", label: "餐點名稱" },
        { key: "o_count", label: "數量", class: "text-center" },
        { key: "o_price", label: "單價", class: "text-center" },
        { key: "subtotal", label: "小計", class: "text-center" },
        { key: "del", label: "", class: "text-center" }
      ]
    };
  },
  methods: {
    addItemToShoppingCart(item) {
      this.$store.dispatch("addItemToShoppingCart", item);
    },
    minusItemToShoppingCart: function(item) {
      this.$store.dispatch("minusItemToShoppingCart", item);
    },
    deleteCartItem(item){
      this.$store.dispatch("deleteItemFromCart", item);

    }
  }
};
</script>

<style>
.totalPrice {
  color: red;
}
</style>

<template>
  <div class="container rounded bg-white mb-4">
    <div class="row ml-3 mr-3 pt-3">
      <h4>
        <font-awesome-icon icon="shopping-bag" />&nbsp;購物清單
      </h4>
    </div>
    <div class="row mt-3 ml-3 mr-3">
      <b-table hover :fields="fields" :items="shoppingCart.shoppingCartItems" outlined>
        <template v-slot:cell(count)="data">
            <b-button pill size="sm" variant="outline-secondary">
              <font-awesome-icon icon="minus" />
            </b-button>
            &nbsp;{{ items.name }}&nbsp;
            <b-button pill size="sm" variant="outline-danger">
              <font-awesome-icon icon="plus" />
            </b-button>
        </template>
      </b-table>
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
export default {
  computed: {
    ...mapGetters({
      shoppingCart: "getShoppingCartInfo"
    })
  },
  data() {
    return {
      fields: [
        { key: "name", label: "餐點名稱" },
        { key: "number", label: "數量", class: "text-center" },
        { key: "price", label: "小計", class: "text-center" }
      ]
    };
  }
};
</script>

<style>
.totalPrice {
  color: red;
}
</style>
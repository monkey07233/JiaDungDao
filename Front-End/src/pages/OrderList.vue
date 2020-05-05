<template>
  <div class="container">
    <b-button class="mb-4 font-weight-bold" @click="back" variant="warning">回到上一頁</b-button>
    <div class="container rounded bg-white mb-4">
      <h4 class="text-center pt-4">
        <font-awesome-icon icon="search" />&nbsp;訂單查詢
      </h4>
      <span v-for="(order, index) in OrderInfo" :key="index">
        <b-card-body v-b-toggle="'accordion-' + index">
          <b-list-group-item style="border-bottom: 0px;">
            <font-awesome-icon icon="store-alt" />
            &nbsp;店家：{{order.title.r_name}}
          </b-list-group-item>

          <b-list-group-item style="border-bottom: 0px;">
            <font-awesome-icon icon="calendar-alt" />
            &nbsp;訂單時間：{{order.title.o_createtime}}
          </b-list-group-item>
          <b-collapse
            :id=" 'accordion-' + index "
            class="p-2"
            style="border-left: 1px solid rgba(0, 0, 0, 0.125);border-right: 1px solid rgba(0, 0, 0, 0.125);"
          >
            <b-card-body style="border: 0px;">
              <span v-for="(orderdetail,index) in order.orderDetail" :key="index">
                <b-list-group-item style="border-bottom: 0px;">品名：{{orderdetail.o_item}}</b-list-group-item>
                <b-list-group-item style="border-bottom: 0px;">數量：{{orderdetail.o_count}}</b-list-group-item>
                <b-list-group-item style="border-top: 0px;">小計：{{orderdetail.o_price * orderdetail.o_count}}</b-list-group-item>
              </span>
            </b-card-body>
          </b-collapse>
          <b-list-group-item style="border-top: 0px;">
            <font-awesome-icon icon="credit-card" />
            &nbsp;總計：{{order.title.o_total}}
          </b-list-group-item>
        </b-card-body>
      </span>
    </div>
  </div>
</template>

<script>
import "@/assets/css/index.css";
import { mapGetters } from "vuex";
export default {
  components: {},
  data() {
    return {};
  },
  computed: {
    ...mapGetters({
      tokenInfo: "getTokenInfo",
      OrderInfo: "getOrderInfo"
    })
  },
  methods: {
    back() {
      this.$router.back();
    }
  },
  created() {
    this.$store.dispatch("getOrderInfo", {
      m_account: this.tokenInfo.account
    });
  }
};
</script>

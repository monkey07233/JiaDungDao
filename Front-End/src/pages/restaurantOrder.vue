<template>
  <div style="width:80%" class="mx-auto mt-3">
    <b-table-simple small hover outlined v-for="(order,indexs) in OrderInfo" :key="indexs">
      <b-thead class="thead-light">
        <b-tr class="text-center">
          <b-td colspan="5">{{order.title.orderId}}</b-td>
        </b-tr>
        <b-tr class="text-left">
          <b-td colspan="2">
            <font-awesome-icon icon="user" />
            &nbsp;訂購人：{{order.title.m_account}}
            <br />
            <font-awesome-icon icon="dollar-sign" />
            &nbsp;
            訂單金額：${{order.title.o_total}}
          </b-td>
          <b-td colspan="3">
            <font-awesome-icon icon="calendar-alt" />
            &nbsp;訂單時間：{{order.title.o_createtime}}
            <br />
            <font-awesome-icon icon="utensils" />
            &nbsp;餐點總數：{{getMenuTotalCount(order.orderDetail)}}
          </b-td>
        </b-tr>
        <b-tr>
          <b-th style="width:90px">#</b-th>
          <b-th>餐點名稱</b-th>
          <b-th>數量</b-th>
          <b-th>單價</b-th>
          <b-th>小計</b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="(item,index) in order.orderDetail" :key="index">
          <b-td>{{index+1}}</b-td>
          <b-td style="width:400px;">{{item.o_item}}</b-td>
          <b-td>{{item.o_count}}</b-td>
          <b-td>{{item.o_price}}</b-td>
          <b-td>{{item.o_count*item.o_price}}</b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  data() {
    return {};
  },
  computed: {
    ...mapGetters({
      OrderInfo: "order/getOrderInfo",
    })
  },
  methods: {
    getMenuTotalCount(menuList) {
      let sum = 0;
      menuList.forEach(menu => {
        sum += menu.o_count;
      });
      return sum;
    }
  },
  created() {
    this.$store.dispatch("order/getOrderInfoByResId", this.$route.params.id);
  }
};
</script>
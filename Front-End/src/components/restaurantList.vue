<template>
  <div class="row mt-5 ml-2 mr-2">
    <b-table-simple striped hover outlined>
      <b-thead>
        <b-tr>
          <b-th>#</b-th>
          <b-th>店名</b-th>
          <b-th>地址</b-th>
          <b-th>電話</b-th>
          <b-th>刪除</b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="(res, index) in restaurantList" :key="index">
          <b-td>{{index + 1}}</b-td>
          <b-td>{{res.r_name}}</b-td>
          <b-td>{{res.r_address}}</b-td>
          <b-td>{{res.r_tel}}</b-td>
          <b-td></b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters({
      restaurantList: "getResList"
    })
  },
  created() {
    this.$store.dispatch("getRestaurantList");
  }
};
</script>
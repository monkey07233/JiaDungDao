<template>
  <div class="container">
    <b-button class="mb-4 font-weight-bold" @click="back" variant="warning">回到上一頁</b-button>
    <div class="container rounded bg-white mb-4">
      <h4 class="text-center pt-4">
        <font-awesome-icon icon="search" />&nbsp;訂單查詢
      </h4>
      <b-card-body v-b-toggle.allOrder-1>
        <b-list-group-item style="border-bottom: 0px;">
          <font-awesome-icon icon="store-alt" />&nbsp;店家：丹尼爾拉姆齊ㄉ窩
        </b-list-group-item>

        <b-list-group-item style="border-bottom: 0px;">
          <font-awesome-icon icon="calendar-alt" />&nbsp;訂單時間：2020/04/24 下午 02:40
        </b-list-group-item>
        <b-collapse
          id="allOrder-1"
          class="p-2"
          style="border-left: 1px solid rgba(0, 0, 0, 0.125);border-right: 1px solid rgba(0, 0, 0, 0.125);"
        >
          <b-card-body style="border: 0px;">
            <b-list-group-item style="border-bottom: 0px;">品名：威靈頓組合牛排:D</b-list-group-item>
            <b-list-group-item style="border-bottom: 0px;">數量：1</b-list-group-item>
            <b-list-group-item style="border-top: 0px;">小計：180</b-list-group-item>
          </b-card-body>
        </b-collapse>
        <b-list-group-item style="border-top: 0px;">
          <font-awesome-icon icon="credit-card" />&nbsp;總計：180
        </b-list-group-item>
      </b-card-body>

      <b-card-body v-b-toggle.allOrder-2>
        <b-list-group-item style="border-bottom: 0px;">
          <font-awesome-icon icon="birthday-cake" />&nbsp;店家：丹尼爾拉姆齊ㄉ第二ㄍ窩
        </b-list-group-item>

        <b-list-group-item style="border-bottom: 0px;">
          <font-awesome-icon icon="envelope" />&nbsp;訂單時間：2020/04/23 下午 02:40
        </b-list-group-item>
        <b-collapse
          id="allOrder-2"
          class="p-2"
          style="border-left: 1px solid rgba(0, 0, 0, 0.125);border-right: 1px solid rgba(0, 0, 0, 0.125);"
        >
          <b-card-body style="border: 0px;">
            <b-list-group-item style="border-bottom: 0px;">品名：威靈頓組合牛排:D</b-list-group-item>
            <b-list-group-item style="border-bottom: 0px;">數量：1</b-list-group-item>
            <b-list-group-item style="border-top: 0px;">小計：180</b-list-group-item>
          </b-card-body>
        </b-collapse>
        <b-list-group-item style="border-top: 0px;">
          <font-awesome-icon icon="home" />&nbsp;總計：220
        </b-list-group-item>
      </b-card-body>
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
  computed: mapGetters({}),
  methods: {
    back() {
      this.$router.back();
    }
  },
  created() {}
};
</script>

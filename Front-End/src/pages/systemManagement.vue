<template>
  <div class="container">
    <div class="rounded bg-white p-3">
      <div class="row p-3 justify-content-md-center">
        <h2 class="font-weight-bold">
          <font-awesome-icon icon="tools" />&nbsp;系統管理
        </h2>
      </div>
      <div class="p-4">
        <b-tabs content-class="mt-3" fill v-model="tabIndex">
          <b-tab active :title-link-class="linkClass(0)">
            <template v-slot:title>
              <font-awesome-icon icon="user" />&nbsp;會員管理
            </template>
            <memberList/>
          </b-tab>
          <b-tab :title-link-class="linkClass(1)">
            <template v-slot:title>
              <font-awesome-icon icon="store" />&nbsp;餐廳管理
            </template>
            <restaurantList/>
          </b-tab>
          <b-tab :title-link-class="linkClass(2)">
            <template v-slot:title>
              <font-awesome-icon icon="check-square" />&nbsp;權限審核
            </template>
            <applicationList/>
          </b-tab>
        </b-tabs>
      </div>
    </div>
  </div>
</template>

<script>
import memberList from "../components/memberList"
import restaurantList from "../components/restaurantList"
import applicationList from "../components/applicationList"
export default {
  data() {
    return {
      tabIndex: 0
    };
  },
  components:{
      memberList,
      restaurantList,
      applicationList
  },
  methods: {
    linkClass(idx) {
      if (this.tabIndex === idx) {
        return ["text-danger", "font-weight-bold"];
      } else {
        return ["text-dark"];
      }
    }
  }
};
</script>

<template>
  <div>
    <div class="d-flex flex-wrap" style="justify-content:center;">
      <b-form-input class="col-5" placeholder="餐廳名稱" v-model="search"></b-form-input>
    </div>
    <div class="d-flex flex-wrap">
      <div class="item col-md-3 col-sm-6" v-for="(res,index) in filterData" :key="index">
        <div class="card">
          <img
            class="card-img-top"
            :src="require('../../../Back-End/File/Restaurant/' + res.restaurantID + '.jpg')"
            width="283.83"
            height="190.25"
          />
          <div class="card-body">
            <p class="card-text">
              <font-awesome-icon icon="utensils" />
              <span class="item-text">{{res.r_name}}</span>
              <br />
              <font-awesome-icon icon="map-marker-alt" />
              <span class="item-text">{{res.r_address}}</span>
              <br />
              <font-awesome-icon icon="phone-alt" />
              <span class="item-text">{{res.r_tel}}</span>
              <br />
            </p>
            <router-link
              class="stretched-link"
              :to="{
              name:'Restaurant',
              params:{id:res.restaurantID}
              }"
            ></router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import "@/assets/css/index.css";
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      search: ""
    };
  },
  computed: {
    ...mapGetters({
      restaurantList: "restaurant/getResList"
    }),
    filterData() {
      const filter = this.search;
      return filter.trim() != ""
        ? this.restaurantList.filter(function(data) {
            return data.r_name.indexOf(filter) > -1;
          })
        : this.restaurantList;
    }
  },
  created() {
    this.$store.dispatch("restaurant/getRestaurantList");
  }
};
</script>

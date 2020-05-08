<template>
  <div>
    <div class="row">
      <div class="col-11"></div>
      <div class="col-1">
        <router-link to="/AddRestaurant">
          <b-button pill variant="light">
            <font-awesome-icon style="color:#A9A9A9" icon="plus" />
          </b-button>
        </router-link>
      </div>
    </div>
    <div class="row p-3">
      <div class="item col-md-3 col-sm-6" v-for="(res,index) in restaurantList" :key="index">
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
              name:'RestaurantDetail',
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
  computed: mapGetters({
    restaurantList: "restaurant/getResByAcc"
  }),
  created() {
    this.$store.dispatch("restaurant/getRestaurantList");
  }
};
</script>

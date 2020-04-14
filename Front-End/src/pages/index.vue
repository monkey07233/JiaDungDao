
<template>
  <div class="d-flex flex-wrap">
    <div class="item col-md-3 col-sm-6" v-for="res in restaurantList">
      <div class="card">
        <img class="card-img-top" src="../assets/images/restaurant.jpg" />
        <div class="card-body">
          <p class="card-text">
            <font-awesome-icon icon="utensils" /><span class="item-text">{{res.r_name}}</span>
            <br />
            <font-awesome-icon icon="map-marker-alt" /><span class="item-text">{{res.r_address}}</span>
            <br />
            <font-awesome-icon icon="phone-alt" /><span class="item-text">{{res.r_tel}}</span>
            <br />
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import "@/assets/css/index.css";
import { mapGetters } from "vuex";
export default {
  data(){
    return{
      
    }
  },
  computed:mapGetters({
    restaurantList: "getResList",
  }),
  created(){
    this.$store.dispatch("getRestaurantList")
  }
};
</script>
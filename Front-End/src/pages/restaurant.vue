<template>
  <div class="container rounded bg-white mb-4">
    <div class="row">
      <div class="col-6 mt-3 mb-5">
        <img
          class="rounded img-fluid"
          :src="require('../../../Back-End/File/Restaurant/' + restaurantInfo.restaurant.r_imgUrl)"
        />
      </div>
      <div class="col-6 mt-3 mb-5">
        <div>
          <b-alert show variant="white">
            <h2 class="alert-heading mb-4">{{restaurantInfo.restaurant.r_name}}</h2>
            <h5 class="mt-5">詳細資訊</h5>
            <hr />
            <p class="mb-2">
              <font-awesome-icon icon="map-marker-alt" />
              <span class="item-text">{{restaurantInfo.restaurant.r_address}}</span>
            </p>
            <p class="mb-0">
              <font-awesome-icon icon="phone-alt" />
              <span class="item-text">{{restaurantInfo.restaurant.r_tel}}</span>
            </p>
          </b-alert>
        </div>
      </div>
    </div>
    <div>
      <b-tabs content-class="mt-3" align="center">
        <template v-for="(item,index) in restaurantInfo.typeAndMenu">
          <mealsCard
            :list="item.menu"
            :tabTitle="item.m_type"
            :resName="restaurantInfo.restaurant.r_name"
            :key="index"
          />
        </template>
      </b-tabs>
    </div>
  </div>
</template>

<script>
import "@/assets/css/index.css";
import mealsCard from "../components/meals";
import { mapGetters } from "vuex";
export default {
  components: {
    mealsCard
  },
  data() {
    return {};
  },
  computed: mapGetters({
    restaurantInfo: "getResInfo"
  }),
  created() {
    this.$store.dispatch("getRestaurantInfo", this.$route.params.id);
  }
};
</script>

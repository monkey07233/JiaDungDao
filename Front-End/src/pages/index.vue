
<template>
  <div class="d-flex flex-wrap">
    <div class="item col-md-3 col-sm-6" v-for="i in 10">
      <div class="card">
        <img class="card-img-top" src="../assets/images/restaurant.jpg" />
        <div class="card-body">
          <p class="card-text">
            <i class="fas fa-utensils"></i><span class="item-text">麥味登</span>
            <br />
            <i class="fas fa-map-marker-alt"></i><span class="item-text">台中市西區XX路XX號</span>
            <br />
            <i class="fas fa-phone-alt"></i><span class="item-text">04-1234-5678</span>
            <br />
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import navbar from '@/components/navbar'
import Vue from "vue";
import "@/assets/css/index.css";
export default {};
</script>
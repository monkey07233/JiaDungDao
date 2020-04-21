<template>
  <div class="container rounded bg-white mb-4">
    <div class="row">
      <div class="col-6 mt-3 mb-5">
        <img class="rounded img-fluid" src="../assets/images/restaurant.jpg" />
      </div>
      <div class="col-6 mt-3 mb-5">
        <div>
          <b-alert show variant="white">
            <h2 class="alert-heading mb-4">陳大帥美式餐廳</h2>
            <p>
              Aww yeah, you successfully read this important alert message. This example text is going to
              run a bit longer so that you can see how spacing within an alert works with this kind of
              content.
            </p>
            <h5 class="mt-5">詳細資訊</h5>
            <hr />
            <p class="mb-2">
              <font-awesome-icon icon="map-marker-alt" />
              <span class="item-text">國際路一段30弄58號</span>
            </p>
            <p class="mb-0">
              <font-awesome-icon icon="phone-alt" />
              <span class="item-text">0978638009</span>
            </p>
          </b-alert>
        </div>
      </div>
    </div>
    <div>
      <b-tabs content-class="mt-3" align="center">
        <template v-for="item in mealsList">
          <mealsCard :list="item.list" :tabTitle="item.title" />
        </template>
      </b-tabs>
    </div>
  </div>
</template>

<script>
import "@/assets/css/index.css";
import mealsCard from "../components/meals";
export default {
  components: {
    mealsCard
  },
  data() {
    return {
      mealsList: [
        {
          title:"漢堡",
          list: [
            {
              title: "招牌大漢堡",
              price: 239
            },
            {
              title: "巨無霸漢堡",
              price: 399
            },
            {
              title: "花生進辣雞腿漢堡",
              price: 256
            },
            {
              title: "數來寶",
              price: 199
            }
          ]
        },
        {
          title:"義大利麵",
          list: [
            {
              title: "青醬雞肉義大利麵",
              price: 256
            },
            {
              title: "橘醬招牌義大利麵",
              price: 289
            }
          ]
        }
      ]
    };
  }
};
</script>
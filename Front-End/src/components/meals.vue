<template>
  <b-tab :title="tabTitle" active>
    <div class="row">
      <div class="ml-3 mb-3" v-for="item in list">
        <div>
          <b-card no-body class="overflow-hidden" style="max-width: 359px;">
            <b-row no-gutters>
              <b-col md="5">
                <b-card-img
                  src="https://picsum.photos/400/400/?image=20"
                  alt="Image"
                  class="rounded-0"
                ></b-card-img>
              </b-col>
              <b-col md="7">
                <b-card-body title-tag="h5" :title="item.m_item">
                  <b-card-text text-tag="h6">${{item.m_price}}</b-card-text>
                  <b-button
                    @click="addToShoppingCart(item.m_item,item.m_price)"
                    variant="outline-primary"
                  >加入購物車</b-button>
                  <span v-for="item in items">
                    <span v-html="item[0]"></span> -
                    <span v-html="item[1]"></span>
                  </span>
                </b-card-body>
              </b-col>
            </b-row>
          </b-card>
        </div>
      </div>
    </div>
  </b-tab>
</template>

<script>
export default {
  data() {
    return {
      items: [],
      item: ["", ""]
    };
  },
  props: {
    tabTitle: String,
    list: Array
  },
  methods: {
    addToShoppingCart(a, b) {
      this.item = [a, b];
      this.items.push(this.item);
    }
  }
};
</script>

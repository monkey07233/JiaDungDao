<template>
  <div class="container rounded bg-white mb-4">
      <div class="row p-3">
          <h4><font-awesome-icon icon="shopping-bag"/>&nbsp購物清單</h4>
      </div>
    <div class="row p-3">
      <b-table hover :fields="fields" :items="items">
        <template v-slot:cell(count)="data">
          <b-button pill size="sm" variant="outline-secondary"><font-awesome-icon icon="minus" /></b-button>
          &nbsp{{ data.item.count}}&nbsp
          <b-button pill size="sm"  variant="outline-danger"><font-awesome-icon icon="plus" /></b-button>
        </template>
      </b-table>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      fields: [
        { key: "m_item", label: "餐點名稱"},
        { key: "count", label: "數量" ,class:"text-center"},
        { key: "m_price", label: "單價",class:"text-center" },
        { key: "totalPrice", label: "小計",class:"text-center" }
      ],
      items: [
        { m_item: "招牌大漢堡", count: 1, m_price: 12, totalPrice: 12 },
        { m_item: "數來寶", count: 2, m_price: 14, totalPrice: 28 },
        { m_item: "青醬雞肉義大利麵", count: 3, m_price: 15, totalPrice: 45 }
      ]
    };
  }
};
</script>
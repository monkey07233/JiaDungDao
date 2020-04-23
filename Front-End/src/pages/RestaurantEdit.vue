<template>
  <div class="container">
    <div class="rounded bg-white mb-4">
      <div class="row">
        <div class="col-12 mt-3 mb-5">
          <div>
            <b-alert show variant="white">
              <h2 class="alert-heading mb-2 text-center">編輯餐廳資料</h2>
              <hr />
              <p class="mb-2">
                <span class="item-text">餐廳照片：</span>
                <input type="file" />
              </p>
              <p class="mb-2">
                <span class="item-text">餐廳名稱：</span>
                <input type="text" />
              </p>
              <p class="mb-2">
                <span class="item-text">餐廳地址：</span>
                <input type="text" />
              </p>
              <p class="mb-2">
                <span class="item-text">餐廳電話：</span>
                <input type="text" />
              </p>
            </b-alert>
            <div class="text-center">
              <button class="btn btn-info">儲存編輯</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="rounded bg-white mb-4">
      <div class="row">
        <div class="col-12 mt-3 mb-5">
          <div>
            <b-alert show variant="white">
              <h2 class="alert-heading mb-2 text-center">編輯餐廳菜單</h2>
            
              <b-form inline class="justify-content-center">
                <label class="sr-only" for="input-name">菜名</label>
                <b-input
                  id="input-name"
                  class="mb-2 mr-sm-2 mb-sm-0"
                  placeholder="請輸入菜名"
                ></b-input>
                <label class="sr-only" for="input-price">價錢</label>
                <b-input
                  id="input-price"
                  class="mb-2 mr-sm-2 mb-sm-0"
                  placeholder="請輸入價錢"
                ></b-input>
                <label class="sr-only" for="input-type">分類</label>
                <b-input
                  id="input-type"
                  class="mb-2 mr-sm-2 mb-sm-0"
                  placeholder="請輸入分類"
                ></b-input>
                <b-button variant="success" @click="addMenuItem">新增</b-button>
              </b-form>
              <table class="table mt-2">
                <thead class="thead-light">
                  <tr>
                    <th>#</th>
                    <th>菜名</th>
                    <th>價錢</th>
                    <th>分類</th>
                  </tr>
                  <tr v-for="(res,index) in menulist">
                    <td>{{index+1}}</td>
                    <td>{{res.item}}</td>
                    <td>{{res.price}}</td>
                    <td>{{res.type}}</td>
                  </tr>
                </thead>
              </table>
            </b-alert>
            <div class="text-center">
              <button class="btn btn-info">儲存菜單</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      menulist: [
        {
          item: "美式漢堡",
          price: 100,
          type: "漢堡"
        }
      ]
    };
  },
  addMenuItem() {}
};
</script>
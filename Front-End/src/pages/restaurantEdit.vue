<template>
  <div>
    <div class="rounded bg-white mb-4">
      <div class="row">
        <div class="col-12 mt-3 mb-5">
          <div>
            <b-form ref="form" @submit.prevent="UpdateRestaurant">
              <b-alert show variant="white">
                <h2 class="alert-heading mb-2 text-center">編輯餐廳資料</h2>
                <hr />
                <b-row class="mb-2 mt-2 justify-content-center">
                  <b-col sm="2" class="text-center">
                    <label>餐廳照片：</label>
                  </b-col>
                  <b-col sm="6" class="text-center">
                    <b-form-file id="restaurantImage" v-model="resImage"></b-form-file>
                  </b-col>
                </b-row>
                <b-row class="mb-2 mt-2 justify-content-center">
                  <b-col sm="2" class="text-center">
                    <label>餐廳名稱：</label>
                  </b-col>
                  <b-col sm="6">
                    <b-form-input
                      name="r_name"
                      required
                      :value="restaurantInfo.restaurant.r_name"
                      type="text"
                    ></b-form-input>
                  </b-col>
                </b-row>
                <b-row class="mb-2 mt-2 justify-content-center">
                  <b-col sm="2" class="text-center">
                    <label>餐廳地址：</label>
                  </b-col>
                  <b-col sm="6">
                    <b-form-input
                      name="r_address"
                      required
                      :value="restaurantInfo.restaurant.r_address"
                      type="text"
                    ></b-form-input>
                  </b-col>
                </b-row>
                <b-row class="mb-2 mt-2 justify-content-center">
                  <b-col sm="2" class="text-center">
                    <label>餐廳電話：</label>
                  </b-col>
                  <b-col sm="6">
                    <b-form-input
                      name="r_tel"
                      required
                      :value="restaurantInfo.restaurant.r_tel"
                      type="text"
                    ></b-form-input>
                  </b-col>
                </b-row>
              </b-alert>
              <div class="text-center justify-content-center">
                <button type="submit" class="btn btn-info">儲存編輯</button>
              </div>
            </b-form>
          </div>
        </div>
      </div>
    </div>
    <hr>
    <div class="rounded bg-white mb-4">
      <div class="row">
        <div class="col-12 mt-3 mb-5">
          <div>
            <b-alert show variant="white">
              <h2 class="alert-heading mb-2 text-center">編輯餐廳菜單</h2>
              <b-form inline class="edit-form justify-content-center pt-3">
                <table>
                  <tr>
                    <td>
                      <b-form-file
                        class="upload-file"
                        placeholder="請上傳菜單照片"
                        id="file-default"
                        v-model="menuImage"
                      ></b-form-file>
                    </td>
                    <td>
                      <label class="sr-only" for="input-name">菜名</label>
                      <b-input
                        v-model="newMenuItem.m_item"
                        name="input-name"
                        class="mb-2 mr-sm-2 mb-sm-0"
                        placeholder="請輸入菜名"
                        required
                      ></b-input>
                    </td>
                    <td>
                      <label class="sr-only" for="input-price">價錢</label>
                      <b-input
                        v-model="newMenuItem.m_price"
                        id="input-price"
                        class="mb-2 mr-sm-2 mb-sm-0"
                        placeholder="請輸入價錢"
                        required
                        type="number"
                      ></b-input>
                    </td>
                    <td>
                      <label class="sr-only" for="input-type">分類</label>
                      <b-input
                        v-model="newMenuItem.m_type"
                        id="input-type"
                        class="mb-2 mr-sm-2 mb-sm-0"
                        placeholder="請輸入分類"
                        required
                      ></b-input>
                    </td>
                    <td>
                      <b-button type="button" variant="success" @click.prevent="addMenuItem">新增</b-button>
                    </td>
                  </tr>
                </table>
              </b-form>
              <table class="table mt-2">
                <thead class="thead-light">
                  <tr>
                    <th>#</th>
                    <th>菜名</th>
                    <th>價錢</th>
                    <th>分類</th>
                    <th></th>
                    <th></th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item,index) in getMenuItemList()" :key="index">
                    <td>{{index+1}}</td>
                    <template v-if="edit_Index != index">
                      <td>{{item.m_item}}</td>
                      <td>{{item.m_price}}</td>
                      <td>{{item.m_type}}</td>
                      <td>
                        <font-awesome-icon
                          @click="edit_Index = index"
                          style="cursor:pointer;"
                          icon="pencil-alt"
                        />
                      </td>
                      <td>
                        <font-awesome-icon
                          style="cursor:pointer;"
                          icon="trash-alt"
                          @click="deleteMenuItem(item.menuID)"
                        />
                      </td>
                    </template>
                    <template v-if="edit_Index == index">
                      <td>
                        <b-input name="item" type="text" required :value="item.m_item"></b-input>
                      </td>
                      <td>
                        <b-input name="price" type="number" required :value="item.m_price"></b-input>
                      </td>
                      <td>
                        <b-input name="type" type="text" required :value="item.m_type"></b-input>
                      </td>
                      <td>
                        <b-form-file placeholder="編輯菜單圖片" v-model="menuImage_update"></b-form-file>
                      </td>
                      <td class="text-center table-icon">
                        <font-awesome-icon
                          @click="updateMenu(item)"
                          style="cursor:pointer;"
                          icon="save"
                        />&nbsp;&nbsp;
                        <font-awesome-icon
                          @click="edit_Index = null"
                          style="cursor:pointer;"
                          icon="times"
                        />
                      </td>
                      <td class="table-icon">
                        <font-awesome-icon
                          style="cursor:pointer;"
                          icon="trash-alt"
                          @click="deleteMenuItem(item.menuID)"
                        />
                      </td>
                    </template>
                  </tr>
                </tbody>
              </table>
            </b-alert>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.table-icon {
  width: 100px;
  padding-top: 20px;
}

.edit-form input,
.edit-form .upload-file {
  /* width:100%; */
  font-size: 13px;
}
</style>

<script>
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      newMenuItem: {
        RestaurantID: this.$route.params.id,
        m_item: "",
        m_type: "",
        m_price: null
      },
      edit_Index: null,
      menuImage: null,
      menuImage_update: null,
      resImage: null,
      formData: new FormData()
    };
  },
  computed: {
    ...mapGetters({
      restaurantInfo: "restaurant/getResInfo",
      tokenInfo: "member/getTokenInfo"
    })
  },
  created() {
    this.$store.dispatch("restaurant/getRestaurantInfo", this.$route.params.id);
  },
  methods: {
    getMenuItemList() {
      let ItemList = [];
      this.restaurantInfo.typeAndMenu.forEach(element => {
        element.menu.forEach(item => {
          ItemList.push(item);
        });
      });
      return ItemList;
    },
    updateMenu(item) {
      this.edit_Index = null;
      this.$store
        .dispatch("menu/updateMenu", {
          MenuID: item.menuID,
          RestaurantID: item.restaurantID,
          m_item: document.querySelector("input[name=item]").value,
          m_price: parseInt(document.querySelector("input[name=price]").value),
          m_type: document.querySelector("input[name=type]").value
        })
        .then(res => {
          this.$store.dispatch("restaurant/getRestaurantInfo", this.$route.params.id);
        });
      this.formData.append("files", this.menuImage_update);
      this.formData.append("uploadType", 1);
      this.formData.append("MenuID", item.menuID);
      this.$store.dispatch("restaurant/uploadImage", this.formData).then(res => {
        this.formData = new FormData();
      });
      this.$bvToast.toast("更新菜單資訊成功", {
        title: `successed`,
        toaster: "b-toaster-top-center",
        solid: true,
        autoHideDelay: 1000,
        appendToast: false
      });
    },
    UpdateRestaurant() {
      var restaurant = {
        RestaurantID: this.$route.params.id,
        r_name: this.$refs.form.r_name.value,
        r_address: this.$refs.form.r_address.value,
        r_tel: this.$refs.form.r_tel.value,
        m_account: this.tokenInfo.account
      };
      this.$store.dispatch("restaurant/UpdateRestaurant", restaurant).then(res => {
        this.formData.append("files", this.resImage);
        this.formData.append("uploadType", 0);
        this.formData.append("id", restaurant.RestaurantID);
        this.$store.dispatch("restaurant/uploadImage", this.formData).then(res => {
          this.formData = new FormData();
          this.$bvToast.toast("更新餐廳資訊成功", {
            title: `successed`,
            toaster: "b-toaster-top-center",
            solid: true,
            autoHideDelay: 1000,
            appendToast: false
          });
        });
        this.$store.dispatch("restaurant/getRestaurantInfo", this.$route.params.id);
      });
    },
    addMenuItem() {
      this.newMenuItem.RestaurantID = parseInt(this.newMenuItem.RestaurantID);
      this.newMenuItem.m_price = parseInt(this.newMenuItem.m_price);
      if (this.menuImage != null) {
        this.$store.dispatch("menu/addMenuItem", this.newMenuItem).then(res => {
          this.$store.dispatch("menu/getMenuId").then(res2 => {
            this.formData.append("files", this.menuImage);
            this.formData.append("uploadType", 1);
            this.formData.append("id", res2.data);
            this.formData.append("m_item", this.newMenuItem.m_item);
            this.$store.dispatch("restaurant/uploadImage", this.formData).then(res3 => {
              this.formData = new FormData();
              this.$bvToast.toast("新增餐點成功", {
                title: `successed`,
                toaster: "b-toaster-top-center",
                solid: true,
                autoHideDelay: 1000,
                appendToast: false
              });
              this.newMenuItem.m_item = "";
              this.newMenuItem.m_type = "";
              this.newMenuItem.m_price = null;
              this.menuImage = null;
              this.$store.dispatch("restaurant/getRestaurantInfo", this.$route.params.id);
            });
          });
        });
      } else {
        this.$bvToast.toast("請上傳菜單照片", {
          title: `Message`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
      }
    },
    deleteMenuItem(menuID) {
      this.$store.dispatch("menu/deleteMenuItem", menuID).then(res => {
        this.$bvToast.toast("刪除餐點成功", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
        this.$store.dispatch("restaurant/getRestaurantInfo", this.$route.params.id);
      });
    },
  }
};
</script>

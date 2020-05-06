<template>
  <b-container class="bv-example-row mb-4">
    <b-row>
      <div class="col-sm-10 col-md-5">
        <b-card>
          <template v-if="!isShow">
            <b-card-body>
              <b-button @click="toggle">
                <font-awesome-icon icon="undo-alt" />&nbsp;返回
              </b-button>
            </b-card-body>
          </template>
          <!-- 會員資料 -->
          <template v-if="isShow" v-bind="MemberInfo">
            <div id="image">
              <img
                v-if="MemberInfo.m_imgUrl === null"
                class="img-fluid"
                src="../assets/images/user.png"
              />
              <img
                class="img-fluid"
                v-if="MemberInfo.m_imgUrl !== null"
                :src="require('../../../Back-End/File/UserImg/' + MemberInfo.memberId + '.jpg')"
              />
              <div id="mask" class="card-img-top">
                <label>
                  <b-form-file id="profileImage" @change="uploadImage" plain></b-form-file>
                  <font-awesome-icon icon="camera" />
                </label>
              </div>
            </div>
            <b-tabs pills justified card>
              <b-tab button-id="profile" title="個人資料" active no-body>
                <b-card-body>
                  <b-card-title class="font-weight-bold text-center">{{MemberInfo.m_name}}</b-card-title>
                </b-card-body>
                <b-list-group flush>
                  <b-list-group-item class="d-flex">
                    <div align-v="center">
                      <font-awesome-icon icon="users-cog" />
                      <label>權限：一般使用者</label>
                    </div>
                    <b-button v-b-modal.userApplication size="sm" variant="warning" class="ml-auto font-weight-bold">申請餐廳管理者</b-button>
                  </b-list-group-item>
                  <b-list-group-item>
                    <font-awesome-icon icon="birthday-cake" />
                    &nbsp;生日：{{MemberInfo.m_birthday}}
                  </b-list-group-item>

                  <b-list-group-item>
                    <font-awesome-icon icon="envelope" />
                    &nbsp;E-mail：{{MemberInfo.m_email}}
                  </b-list-group-item>

                  <b-list-group-item>
                    <font-awesome-icon icon="home" />
                    住址：{{MemberInfo.m_address}}
                  </b-list-group-item>
                </b-list-group>
                <b-card-body>
                  <b-button @click="toggle">
                    <font-awesome-icon icon="pencil-alt" />
                  </b-button>
                </b-card-body>
              </b-tab>
              <b-tab title="更換密碼" no-body>
                <div class="mt-3">
                  <b-form @submit.prevent="updatePassword">
                    <b-form-group label="輸入舊密碼:" label-for="oldPassword">
                      <b-form-input
                        v-model="passwordInfo.m_password"
                        id="oldPassword"
                        type="password"
                        required
                        placeholder="請輸入舊密碼"
                      ></b-form-input>
                    </b-form-group>

                    <b-form-group label="輸入新密碼:" label-for="newPassword">
                      <b-form-input
                        v-model="passwordInfo.new_password"
                        id="newPassword"
                        type="password"
                        required
                        placeholder="請輸入新密碼"
                        trim
                        :state="check_oldAndnew_password"
                        aria-describedby="check-feedback"
                      ></b-form-input>
                      <b-form-invalid-feedback id="check-feedback">不能與舊密碼相同</b-form-invalid-feedback>
                    </b-form-group>
                    <b-form-group label="確認新密碼:" label-for="confirm">
                      <b-form-input
                        id="confirm"
                        v-model="confirmPassword"
                        :state="confirm_password"
                        aria-describedby="confirm-feedback"
                        placeholder="確認新密碼"
                        type="password"
                        required
                        trim
                      ></b-form-input>
                      <b-form-invalid-feedback id="confirm-feedback">與新密碼不相符</b-form-invalid-feedback>
                    </b-form-group>

                    <div class="row justify-content-md-center">
                      <b-button type="submit" variant="danger" class="justify-content-md-center">
                        <font-awesome-icon icon="save" />&nbsp;Save
                      </b-button>
                    </div>
                  </b-form>
                </div>
              </b-tab>
            </b-tabs>
            <b-modal id="userApplication" centered title="申請成為餐廳管理者" hide-footer>
              <b-form>
                <b-row class="mt-2">
                  <b-col sm="4">
                    <label for="textarea-default">請填寫申請原因：</label>
                  </b-col>
                  <b-col sm="8">
                    <b-form-textarea id="textarea-default" placeholder="在此輸入文字..."></b-form-textarea>
                  </b-col>
                </b-row>
                <b-row class="mb-2 mt-4 justify-content-center">
                  <b-button pill type="submit" variant="info">送出</b-button>
                </b-row>
              </b-form>
            </b-modal>
          </template>
          <!-- 修改會員資料 -->
          <template v-else>
            <div>
              <b-form ref="form" @submit.prevent="UpdateProfile">
                <b-card-body>
                  <b-form-group id="input-group-1" label="姓名:" label-for="input-1">
                    <b-form-input
                      name="name"
                      :value="MemberInfo.m_name"
                      required
                      placeholder="Enter name"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group id="input-group-2" label="生日:" label-for="input-2">
                    <b-form-datepicker
                      name="birthday"
                      :value="MemberInfo.m_birthday"
                      required
                      placeholder="Enter birthday"
                    ></b-form-datepicker>
                  </b-form-group>

                  <b-form-group id="input-group-3" label="E-mail:" label-for="input-3">
                    <b-form-input
                      name="email"
                      :value="MemberInfo.m_email"
                      type="email"
                      required
                      placeholder="Enter email"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group id="input-group-4" label="住址:" label-for="input-4">
                    <b-form-input
                      name="address"
                      :value="MemberInfo.m_address"
                      required
                      placeholder="Enter address"
                    ></b-form-input>
                  </b-form-group>
                  <div class="row justify-content-md-center">
                    <b-button class="mr-2" type="submit" variant="primary">Save</b-button>
                  </div>
                </b-card-body>
              </b-form>
            </div>
          </template>
        </b-card>
      </div>
      <!-- 訂單資料 -->
      <div v-if="OrderInfo[0]!=null" class="col-sm-6">
        <b-card>
          <template v-bind="OrderInfo">
            <h5 class="text-center">
              <font-awesome-icon icon="search" />&nbsp;訂單查詢
            </h5>
            <b-card-body flush>
              <b-list-group-item>
                <font-awesome-icon icon="store-alt" />
                &nbsp;店家：{{OrderInfo[0].title.r_name}}
              </b-list-group-item>
              <b-list-group-item>
                <font-awesome-icon icon="calendar-alt" />
                &nbsp;訂單時間：{{OrderInfo[0].title.o_createtime}}
              </b-list-group-item>
              <b-list-group-item>
                <font-awesome-icon icon="credit-card" />
                &nbsp;總計：{{OrderInfo[0].title.o_total}}
              </b-list-group-item>
            </b-card-body>
            <b-card-body>
              <b-button to="/OrderList">查看更多</b-button>
            </b-card-body>
          </template>
        </b-card>
      </div>
    </b-row>
  </b-container>
</template>

<script>
import "@/assets/css/profile.css";
import { mapGetters } from "vuex";
export default {
  data: function() {
    return {
      isShow: true,
      passwordInfo: {
        m_account: "",
        m_password: "",
        new_password: ""
      },
      confirmPassword: "",
      formData: new FormData()
    };
  },
  computed: {
    ...mapGetters({
      tokenInfo: "getTokenInfo",
      MemberInfo: "getMemberInfo",
      OrderInfo: "getOrderInfo"
    }),
    confirm_password() {
      return this.confirmPassword === this.passwordInfo.new_password
        ? true
        : false;
    },
    check_oldAndnew_password() {
      return this.passwordInfo.new_password != this.passwordInfo.m_password
        ? true
        : false;
    }
  },
  methods: {
    toggle: function() {
      this.isShow = !this.isShow;
    },
    UpdateProfile(e) {
      e.preventDefault();
      var updateProfile = {
        m_account: this.tokenInfo.account,
        m_name: this.$refs.form.name.value,
        m_birthday: this.$refs.form.birthday.value,
        m_email: this.$refs.form.email.value,
        m_address: this.$refs.form.address.value
      };
      this.$store.dispatch("UpdateProfile", updateProfile).then(res => {
        this.isShow = !this.isShow;
        this.$bvToast.toast(res, {
          title: `Update Personal Information`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
        this.$store.dispatch("getMemberInfo", {
          m_account: this.tokenInfo.account
        });
      });
    },
    updatePassword() {
      this.passwordInfo.m_account = this.tokenInfo.account;
      this.$store
        .dispatch("UpdatePassword", this.passwordInfo)
        .then(res => {
          this.$bvToast.toast(res, {
            title: `更新密碼成功`,
            toaster: "b-toaster-top-center",
            solid: true,
            autoHideDelay: 1000,
            appendToast: false
          });
          this.passwordInfo.m_password = "";
          this.passwordInfo.new_password = "";
          this.confirmPassword = "";
          this.$store.dispatch("logout");
          localStorage.clear();
          setTimeout(() => {
            this.$router.push("/");
          }, 1000);
        })
        .catch(err => {
          if (err.response.status == 400) {
            this.$bvToast.toast(err.response.data, {
              title: `更新密碼失敗`,
              toaster: "b-toaster-top-center",
              solid: true,
              autoHideDelay: 1000,
              appendToast: false
            });
          }
        });
    },
    uploadImage(e) {
      this.formData.append("files", e.target.files[0]);
      this.formData.append("uploadType", 2);
      this.formData.append("id", this.MemberInfo.memberId);
      this.$store.dispatch("uploadImage", this.formData).then(res => {
        console.log(res);
        this.formData = new FormData();
        this.$bvToast.toast("照片更換成功", {
          title: `successed`,
          toaster: "b-toaster-top-center",
          solid: true,
          autoHideDelay: 1000,
          appendToast: false
        });
      });
    }
  },
  created() {
    this.$store.dispatch("getMemberInfo", {
      m_account: this.tokenInfo.account
    });
    this.$store.dispatch("getOrderInfo", {
      m_account: this.tokenInfo.account
    });
  }
};
</script>

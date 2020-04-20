<template>
  <b-container class="bv-example-row">
    <b-row>
      <div class="col-sm-4">
        <b-card>
          <template v-if="!isShow">
            <b-card-body>
              <b-button @click="toggle">
                <font-awesome-icon icon="undo-alt" />&nbsp返回
              </b-button>
            </b-card-body>
          </template>
          <!-- 會員資料 -->
          <template v-if="isShow" v-bind="MemberInfo">
            <img class="card-img-top" src="../assets/images/restaurant.jpg" />
            <b-card-body>
              <b-card-title>{{MemberInfo.m_name}}</b-card-title>
            </b-card-body>
            <b-list-group flush>
              <b-list-group-item>
                <font-awesome-icon icon="birthday-cake" />
                &nbsp生日:{{MemberInfo.m_birthday}}
              </b-list-group-item>

              <b-list-group-item>
                <font-awesome-icon icon="envelope" />
                &nbspE-mail:{{MemberInfo.m_email}}
              </b-list-group-item>

              <b-list-group-item>
                <font-awesome-icon icon="home" />
                &nbsp住址:{{MemberInfo.m_address}}
              </b-list-group-item>
            </b-list-group>
            <b-card-body>
              <b-button @click="toggle">
                <font-awesome-icon icon="pencil-alt" />
              </b-button>
            </b-card-body>
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
    </b-row>
  </b-container>
</template>

<script>
import "@/assets/css/index.css";
import { mapGetters } from "vuex";
export default {
  data: function() {
    return {
      isShow: true
    };
  },
  computed: {
    ...mapGetters({
      tokenInfo: "getTokenInfo",
      MemberInfo: "getMemberInfo"
    })
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
    }
  },
  created() {
    this.$store.dispatch("getMemberInfo", {
      m_account: this.tokenInfo.account
    });
  }
};
</script>

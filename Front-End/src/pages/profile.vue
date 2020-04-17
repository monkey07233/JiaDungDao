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

          <template v-if="isShow" v-bind="MemberInfo">
            <img class="card-img-top" src="../assets/images/restaurant.jpg" />
            <b-card-body>
              <b-card-title>{{MemberInfo.m_name}}</b-card-title>
            </b-card-body>
            <b-list-group flush>
              <b-list-group-item>
                <font-awesome-icon icon="birthday-cake"/>&nbsp生日: {{MemberInfo.data.m_birthday}}
              </b-list-group-item>

              <b-list-group-item>
                <font-awesome-icon icon="envelope"/>&nbspE-mail: {{MemberInfo.data.m_email}}
              </b-list-group-item>

              <b-list-group-item>
                <font-awesome-icon icon="home"/>&nbsp住址: {{MemberInfo.data.m_address}}
              </b-list-group-item>
            </b-list-group>
            <b-card-body>
              <b-button @click="toggle">
                <font-awesome-icon icon="pencil-alt" />
              </b-button>
            </b-card-body>
          </template>
          <!-- edit -->
          <template v-else>
            <div>
              <b-form ref="form" @submit.prevent="updateProfile">
                <b-card-body>
                  <b-form-group id="input-group-1" label="姓名:" label-for="input-1">
                    <b-form-input name="name" :value="form.name" required placeholder="Enter name"></b-form-input>
                  </b-form-group>

                  <b-form-group id="input-group-2" label="生日:" label-for="input-2">
                    <b-form-datepicker
                      name="birthday"
                      :value="form.birthday"
                      required
                      placeholder="Enter birthday"
                    ></b-form-datepicker>
                  </b-form-group>

                  <b-form-group id="input-group-3" label="E-mail:" label-for="input-3">
                    <b-form-input
                      name="email"
                      :value="form.email"
                      type="email"
                      required
                      placeholder="Enter email"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group id="input-group-4" label="住址:" label-for="input-4">
                    <b-form-input
                      name="address"
                      :value="form.address"
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
      form: {
        email: "tiffany@example.com",
        name: "Tiffany",
        address: "鬼之王冠路77弄386巷666號",
        birthday: "1998-08-07"
      },
      isShow: true
    };
  },
  computed: {
    ...mapGetters({
      account: "getAccount"
    })
  },
  methods: {
    toggle: function() {
      this.isShow = !this.isShow;
    },
    updateProfile(e) {
      e.preventDefault();
      let name = this.$refs.form.name.value;
      let birthday = this.$refs.form.birthday.value;
      let email = this.$refs.form.email.value;
      let address = this.$refs.form.address.value;
      this.$store
        .dispatch("updateProfile", {
          m_account: account,
          m_name: name,
          m_birthday: birthday,
          m_email: email,
          m_address: address
        })
        .then(res => {
          this.isShow = !this.isShow;
          this.$bvToast.toast(res, {
            title: `Update Personal Information`,
            toaster: "b-toaster-top-center",
            solid: true,
            autoHideDelay: 1000,
            appendToast: false
          });
        });
    }
  },
  created(){
    var account = JSON.parse(localStorage.getItem("tokenInfo")).account
    var memberInfo = {
      "m_account": account
    }
    this.$store.dispatch("getMemberInfo",memberInfo);
  },
  computed:mapGetters({
    MemberInfo: "getMemberInfo",
  })
};
</script>

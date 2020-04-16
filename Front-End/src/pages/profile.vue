<template>
  <b-container class="bv-example-row">
    <b-row>
      <div class="col-sm-4">
        <b-card>
          <template v-if="!isShow">
            <b-card-body>
              <b-button @click="toggle">返回</b-button>
            </b-card-body>
          </template>

          <template v-if="isShow">
            <img class="card-img-top" src="../assets/images/restaurant.jpg" />
            <b-card-body>
              <b-card-title>Tiffany</b-card-title>
            </b-card-body>
            <b-list-group flush>
              <b-list-group-item>生日: 87/08/07</b-list-group-item>

              <b-list-group-item>E-mail: tiffany@example.com</b-list-group-item>

              <b-list-group-item>住址: 鬼之王冠路77弄386巷666號</b-list-group-item>
            </b-list-group>
            <b-card-body>
              <b-button @click="toggle">編輯</b-button>
            </b-card-body>
          </template>
          <template v-else>
            <div>
              <b-form @submit="onSubmit" @reset="onReset">
                <b-card-body>
                  <b-form-group id="input-group-1" label="姓名:" label-for="input-1">
                    <b-form-input
                      id="input-1"
                      v-model="form.name"
                      required
                      placeholder="Enter name"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group
                    id="input-group-2"
                    label="E-mail:"
                    label-for="input-2"
                  >
                    <b-form-input
                      id="input-2"
                      v-model="form.email"
                      type="email"
                      required
                      placeholder="Enter email"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group id="input-group-3" label="住址:" label-for="input-3">
                    <b-form-input
                      id="input-3"
                      v-model="form.address"
                      required
                      placeholder="Enter address"
                    ></b-form-input>
                  </b-form-group>

                  <b-button type="submit" variant="primary">Save</b-button>
                  <b-button type="reset" variant="danger">Reset</b-button>
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

export default {
  data: function() {
    return {
      form: {
        email: "",
        name: "",
        address: ""
      },
      isShow: true
    };
  },
  methods: {
    toggle: function() {
      this.isShow = !this.isShow;
    },
    onSubmit(evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
    },
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
      this.form.email = "";
      this.form.name = "";
      this.form.address = "";
      // Trick to reset/clear native browser form validation state
    }
  }
};
</script>

<template>
  <div class="row mt-5 ml-2 mr-2">
    <b-table-simple striped hover outlined>
      <b-thead>
        <b-tr>
          <b-th>#</b-th>
          <b-th>申請人</b-th>
          <b-th>申請原因</b-th>
          <b-th>審核狀態</b-th>
          <b-th></b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="(app , index) in applicationList" :key="index">
          <b-td>{{index+1}}</b-td>
          <b-td>{{app.m_account}}</b-td>
          <b-td>{{app.reason}}</b-td>
          <b-td>{{app.status==null? "未審核":app.status==1?"通過":"駁回"}}</b-td>
          <b-td>
            <b-button
              variant="success"
              v-if="app.status==null"
              @click="verifyApplication(true,app.m_account)"
            >
              <font-awesome-icon icon="check" style="margin-right:0.5em;" />通過
            </b-button>
            <b-button
              variant="danger"
              v-if="app.status==null"
              @click="verifyApplication(false,app.m_account)"
            >
              <font-awesome-icon icon="times" style="margin-right:0.5em;" />失敗
            </b-button>
          </b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters({
      applicationList: "getAppList"
    })
  },
  created() {
    this.$store.dispatch("getAllApplication");
  },
  methods: {
    verifyApplication(pass, account) {
      this.$store
        .dispatch("verifyApplication", {
          pass: pass,
          account: account
        })
        .then(res => {
          this.$bvToast.toast(res, {
            title: `successed`,
            toaster: "b-toaster-top-center",
            solid: true,
            autoHideDelay: 1000,
            appendToast: false
          });
          this.$store.dispatch("getAllApplication");
        });
    }
  }
};
</script>

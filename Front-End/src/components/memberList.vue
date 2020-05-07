<template>
  <div class="row mt-5 ml-2 mr-2">
    <b-table-simple striped hover outlined>
      <b-thead>
        <b-tr>
          <b-th>#</b-th>
          <b-th>帳號</b-th>
          <b-th>姓名</b-th>
          <b-th>E-mail</b-th>
          <b-th>生日</b-th>
          <b-th>地址</b-th>
          <b-th>權限</b-th>
          <b-th>封鎖</b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="(member , index) in memberList" :key="index">
          <b-td class="align-middle">{{member.memberId}}</b-td>
          <b-td class="align-middle">{{member.m_account}}</b-td>
          <b-td class="align-middle">{{member.m_name}}</b-td>
          <b-td class="align-middle">{{member.m_email}}</b-td>
          <b-td class="align-middle">{{member.m_birthday}}</b-td>
          <b-td class="align-middle">{{member.m_address}}</b-td>
          <b-td class="align-middle">{{member.m_role}}</b-td>
          <b-td>
            <b-button variant="danger">
              <font-awesome-icon icon="ban" style="margin-right:0.5em;" />封鎖
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
      memberList: "getMemberList"
    })
  },
  methods: {},
  created() {
    this.$store.dispatch("getAllMember");
  }
};
</script>
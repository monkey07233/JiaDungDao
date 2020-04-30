<template>
  <div class="container">
    <h1 class="validate">恭喜完成驗證</h1>
    <br />
    <h3 class="validate">接下來將跳轉至首頁，跳轉後請從右上角會員圖示登入</h3>
  </div>
</template>
<script>
import "@/assets/css/index.css";
import { mapGetters } from "vuex";
export default {
  data() {
    return {};
  },
  created() {
    var param = this.$route.fullPath.split("?")[1];
    if (param == undefined || param == null) {
      this.$router.push("Index");
    } else {
      if (param.split("=")[0] != "account") {
        this.$router.push("Index");
      } else {
        this.$store.dispatch("VerifyAccount", {
          m_account: param.split("=")[1].toString()
        });
      }
    }
    setTimeout(function() {
      this.location.replace("Index");
    }, 3000);
  }
};
</script>
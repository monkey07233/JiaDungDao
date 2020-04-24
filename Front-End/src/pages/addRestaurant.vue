<template>
  <div class="container rounded bg-white mb-4">
     <div class="row ml-3 mr-3 pt-3">
      <h4 class="form-title">
        <font-awesome-icon icon="plus" />&nbsp;新增餐廳
      </h4>
    </div>
    <hr>
    <b-form>
        <b-form-group label="餐廳名稱" label-for="input-1">
          <b-form-input id="input-1" type="text" required placeholder="請輸入餐廳名稱"></b-form-input>
        </b-form-group>
        <b-form-group label="餐廳地址：" label-for="input-2">
          <b-form-input id="input-2" type="text" required placeholder="請輸入餐廳地址"></b-form-input>
        </b-form-group>
        <b-form-group label="聯絡電話" label-for="input-3">
          <b-form-input id="input-3" type="tel" required placeholder="請輸入餐廳聯絡電話"></b-form-input>
        </b-form-group>       
        <b-button type="submit" variant="primary" class="submit-btn">送出</b-button>       
    </b-form>
  </div>
</template>

<script>
export default {
  data() {
    return {

    };
  },
};
</script>

<style >
  .form-title{
      margin-left:auto;
      margin-right:auto;
  }

  .submit-btn{
      margin-bottom:12px;
  }
</style>
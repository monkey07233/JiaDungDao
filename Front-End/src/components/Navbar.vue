<template>
  <nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-dark">
    <a class="navbar-brand" href="#">JiaDungDao</a>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item">
          <a class="nav-link" href="#">
            Home
            <span class="sr-only">(current)</span>
          </a>
        </li>
      </ul>
    </div>
    <div class="ml-auto">
      <ul class="navbar-nav ml-auto">
        <li class="nav-item">
          <b-button v-b-modal.login>
            <font-awesome-icon icon="sign-in-alt" />Sign In
          </b-button>
          <b-modal id="login" centered title="Sign in" hide-footer>
            <b-row class="mb-2 mt-2">
              <b-col sm="2">
                <label>帳號:</label>
              </b-col>
              <b-col sm="10">
                <b-form-input size="sm" placeholder="輸入你的帳號"></b-form-input>
              </b-col>
            </b-row>
            <b-row class="mb-2 mt-2">
              <b-col sm="2">
                <label>密碼:</label>
              </b-col>
              <b-col sm="10">
                <b-form-input size="sm" placeholder="輸入你的密碼" type="password"></b-form-input>
              </b-col>
            </b-row>
            <b-row class="mb-2 mt-2 justify-content-center">
              <b-button pill size="sm">登入</b-button>
            </b-row>
            <b-row class="mb-2 mt-2 justify-content-center">
              <b-link href="#">去註冊</b-link>
            </b-row>
          </b-modal>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {};
</script>
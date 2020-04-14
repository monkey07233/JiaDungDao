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
          <button>
            <font-awesome-icon icon="sign-in-alt" />Sign In 
          </button>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {};
</script>
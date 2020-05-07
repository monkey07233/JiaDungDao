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
        <b-tr>
          <b-td>1</b-td>
          <b-td>1111</b-td>
          <b-td>1111</b-td>
          <b-td>1111</b-td>
          <b-td>
            <b-button variant="success"><font-awesome-icon icon="check"/>通過</b-button>
            <b-button variant="danger"><font-awesome-icon icon="times"/>失敗</b-button>
          </b-td>
        </b-tr>
        <b-tr>
          <b-td>2</b-td>
          <b-td>1111</b-td>
          <b-td>1111</b-td>
          <b-td>1111</b-td>
          <b-td>
            <b-button variant="success"><font-awesome-icon icon="check"/>通過</b-button>
            <b-button variant="danger"><font-awesome-icon icon="times"/>失敗</b-button>
          </b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

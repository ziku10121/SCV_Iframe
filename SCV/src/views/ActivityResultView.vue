<template>
  <div class="container">
    <div>
        <table class="table mt-3">
          <thead>
            <tr>
              <th scope="col">訪談結果</th>
              <th scope="col">統計人數</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>待聯絡客戶</td>
              <td>1</td>
            </tr>
            <tr>
              <td>成交</td>
              <td>1</td>
            </tr>
            <tr>
              <td>續追</td>
              <td>2</td>
            </tr>
            <tr>
              <td>拒絕行銷</td>
              <td>2</td>
            </tr>
            <tr>
              <td>聯絡不上</td>
              <td>2</td>
            </tr>
            <tr>
              <td>意見反饋</td>
              <td>2</td>
            </tr>
            <tr>
              <td>限售客戶</td>
              <td>2</td>
            </tr>
            <tr>
              <td>KYC不符</td>
              <td>2</td>
            </tr>
            <tr>
              <td>無效戶</td>
              <td>2</td>
            </tr>
          </tbody>
          <tfoot>
            <tr>
              <td>合計</td>
              <td>16</td>
            </tr>
          </tfoot>
        </table>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ActivityResultView',
  data(){
    return{

    }
  },
  methods(){
    
  }
}
</script>

<style>

</style>
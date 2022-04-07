<template>
  <div class="container mt-3">
    <div class="fs-4">
      營業員權客戶現貨成交金額日查詢
    </div>
    <div class="row mt-2 mx-1">
      <table class="table caption-top align-middle">
        <caption>
          <div class="row">
            <div class="col-5 fs-6 align-self-center">
              <div class="fs-6">資料日期:{{Today}}</div>
            </div>
            <div class="col-7">
              <div class="input-group flex-nowrap">
                <label for="inputGroupSelect01" class="input-group-text">分公司</label>
                <select class="form-select form-select-sm" id="inputGroupSelect01">
                  <option selected>請選擇分公司</option>
                  <option value="1">台中分公司</option>
                  <option value="2">基隆分公司</option>
                  <option value="3">台北分公司</option>
                </select>
                <button class="btn btn-outline-secondary btn-sm ">查詢</button>
              </div>   
            </div>
          </div>       
        </caption>
        <thead>
          <tr>
            <th scope="col">排名</th>
            <th scope="col">客戶帳號</th>
            <th scope="col">客戶姓名</th>
            <th scope="col">成交金額</th>
            <th scope="col">營業員</th>
            <th scope="col">明細</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for=" (item,index) in TableData" :key="index">
            <td>{{index+1}}</td>
            <td>{{item.account}}</td>
            <td>{{item.name}}</td>
            <td>{{item.turnover}}</td>
            <td>{{item.AE}}</td>
            <td>
              <button class="btn btn-primary btn-sm" @click="detail()">明細</button>
            </td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td>合計</td>
            <td>{{Object.keys(TableData).length}}筆</td>
            <td></td>
            <td>{{Total}}</td>
            <td></td>
            <td></td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
</template>
<script>
export default {
    name: 'Home',
    data(){
      return{
        TableData:[
          {account:'0000000',name:'宓奇文',turnover:999999, AE:'王小名'},
          {account:'0000001',name:'融立夏',turnover:899999, AE:'王小名'},
          {account:'0000002',name:'汪琳敏',turnover:799999, AE:'張阿花'},
          {account:'0000003',name:'谷月朗',turnover:699999, AE:'牧雨竹'},
          {account:'0000004',name:'魏芮波',turnover:599999, AE:'王小名'},
          {account:'0000005',name:'司静娴',turnover:499999, AE:'張阿花'},
          {account:'0000005',name:'富嘉歆',turnover:399999, AE:'王小名'},
          {account:'0000005',name:'景凝静',turnover:299999, AE:'張阿花'},
          {account:'0000005',name:'郜添智',turnover:199999, AE:'王小名'},
          {account:'0000005',name:'田梓楠',turnover:99999, AE:'王小名'},
          {account:'0000005',name:'林秋彤',turnover:89999, AE:'牧雨竹'},
        ]
      }
    },
    computed:{
      Today: function(){
        var Today = new Date();
        var result =  Today.getFullYear()+ "年" + (Today.getMonth()+1) + "月" + Today.getDate() + "日";
        return result ;
      },
      Total:function(){
        var result = 0;
        for(let i = 0 ; i<Object.keys(this.TableData).length;i++){         
          result += this.TableData[i].turnover;
        }
        return result;
      }
    },
    methods:{
      detail(){
        this.$router.push({name:'About'})
      }
    }
  }
</script>

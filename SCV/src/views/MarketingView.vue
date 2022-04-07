<template>
  <div class="container-fluid pt-4 pb-5 mt-5">
    <!-- #region Company -->
    <div class="row fixed-top">
      <div class="bg-white ps-4 pe-4 pt-3 pb-3">
        <div class="col-12 col-md-6 col-lg-4">
          <div class="input-group flex-nowrap">
            <label for="inputGroupSelect01" class="input-group-text"
              >分公司</label
            >
            <select
              class="form-select form-select-sm"
              id="inputGroupSelect01"
              v-model="company"
              :disabled="isFirm"
            >
              <option v-for="item in comData" :key="item" :value="item.firmno">
                {{ item.firmnm }}
              </option>
            </select>
          </div>
        </div>
      </div>
    </div>
    <!-- #endregion -->

    <!-- #region 行銷效益 -->
    <div v-for="item in filterData" :key="item">
      <ActList :ActData="item"></ActList>
    </div>

    <!-- 商品貢獻度 -->
    <div class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        商品貢獻度
      </div>
      <div class="card-body cardbodybg">
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="200px">類別</th>
              <th scope="col" width="200px">項目</th>
              <th scope="col">今年貢獻度</th>
              <th scope="col">佔比</th>
              <th scope="col">去年貢獻度</th>
              <th scope="col">前年貢獻度</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row" rowspan="5" style="vertical-align : middle;text-align:center;">經紀</td>
              <td scope="row">台股貢獻度</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">信用貢獻度</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">借券貢獻度</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">期貨</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr class="table-primary">
              <td scope="row">經紀合計</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row" rowspan="8" style="vertical-align : middle;text-align:center;">財管</td>
              <td scope="row">不限用途</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">海外股票</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">海外債</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">基金</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">壽險</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">結構型</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">OSU</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr class="table-primary">
              <td scope="row">財管合計</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr class="table-success">
              <td scope="row" colspan="2" style="vertical-align : middle;text-align:center;">合計</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- 潛力貼標 -->
    <div class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        潛力貼標
      </div>
      <div class="card-body cardbodybg">
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="400px">項目</th>
              <th scope="col">貼標值</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row">金控潛力貼標</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">海外投資偏好貼標</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">外匯偏好貼標</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">高股利貼標</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">投資活動力貼標</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">期貨商品偏好貼標</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">壽險偏好貼標</td>
              <td></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- 行銷效益 -->
    <div class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        行銷效益
      </div>
      <div class="card-body cardbodybg">
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="400px">指標項目</th>
              <th scope="col">今年參考值</th>
              <th scope="col">去年參考值</th>
              <th scope="col">前年參考值</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row">總資產(萬)</td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">財管貢獻度</td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">經紀貢獻度</td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">總貢獻度</td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">月均交易量</td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">股利金額</td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
          </tbody>
        </table>
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="400px">指標項目</th>
              <th scope="col">參考值</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row">定期定額每月扣款金額</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">當沖佔比</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">今年累積專案名單數</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">行銷Call客紀錄次數</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">最後交易日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">最後Call客日</td>
              <td></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <!-- #endregion -->
  </div>
</template>
<script>
var actData = [
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王XX",
    },
    firmno: "9654",
    acttype: "001",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "null",
    dtrdlmt: "null",
    eopndate: "null",
  },
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王XX",
    },
    firmno: "9654",
    acttype: "002",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "1",
    dtrdlmt: "1111",
    eopndate: "2020/12/22",
  },
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王XX",
    },
    firmno: "9654",
    acttype: "002",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "1",
    dtrdlmt: "1111",
    eopndate: "2020/12/22",
  },
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王OO",
    },
    firmno: "9654",
    acttype: "003",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "1",
    dtrdlmt: "1111",
    eopndate: "2020/12/22",
  },
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王OO",
    },
    firmno: "9654",
    acttype: "004",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "1",
    dtrdlmt: "1111",
    eopndate: "2020/12/22",
  },
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王OO",
    },
    firmno: "9654",
    acttype: "005",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "1",
    dtrdlmt: "1111",
    eopndate: "2020/12/22",
  },
  {
    BasicInfo: {
      actno: "0000000",
      idno: "A248678996",
      name: "Test1",
      cusgrptype: "高貢獻客戶",
      gender: "1",
      age: "20",
      bday: "2020/09/06",
      mate: "王OO",
    },
    firmno: "9655",
    acttype: "006",
    actopendate: "2020/09/06",
    email: "SYSTEX@example.com",
    mobile: "0912456753",
    phone: "1263156",
    hraddr: "台中市西屯區惠來路二段101號",
    caddr: "台中市西屯區惠來路二段101號",
    contact: "王小明",
    agent: "王小明",
    cussrctyp: "自開",
    translmt: "999999",
    evalidflag: "1",
    banknm: "00000000000",
    emgstklmt: "88888",
    crvalidflag: "1",
    mgntralmt: "999889",
    sselllmt: "999999",
    dtrdflag: "1",
    piflag: "2",
    actclsdate: "1",
    dtrdlmt: "1111",
    eopndate: "2020/12/22",
  },
];

import { defineComponent } from "vue";
import { GetAccountList } from "../api/Api_manager.js";

import ActList from "../components/ActList.vue";

export default defineComponent({
  name: "App",
  components: { ActList },
  data() {
    return {
      company: "",
      comData: [],
      actData: [],
      filterData: [],
      isFirm: false,
      quest: {
        idno: "",
        userid: "",
        staffno: "",
        dep: "",
        gr: "",
        bh: "",
        level: "",
      },
    };
  },
  watch: {
    company: function () {
      console.log("firm change");
      // var self= this
      this.filterData = this.FilterActDataDemo();
    },
  },
  computed: {},
  created() {
    this.Geturlparams();
  },
  methods: {
    Geturlparams() {
      const queryString = window.location.search;
      const geturlParams = new URLSearchParams(queryString);
      console.log("外面接收網址", queryString);
      if (queryString != "") {
        this.quest.idno = geturlParams.get("id");
        this.quest.userid = geturlParams.get("userid");
        this.quest.staffno = geturlParams.get("staffno");
        this.quest.dep = geturlParams.get("dep");
        this.quest.gr = geturlParams.get("gr");
        this.quest.bh = geturlParams.get("bh");
        this.quest.level = geturlParams.get("level");
        console.log(this.quest);
        this.PostAccountApi();
      }
    },
    async PostAccountApi() {
      try {
        var data = JSON.stringify(this.quest);
        console.log(data);
        var rawdata = await GetAccountList(data);
        if (rawdata.statusText == "OK") {
          if (rawdata.data.status == 1) {
            this.comData = rawdata.data.AllCompany;
            this.company = this.comData[0].firmno;
            this.actData = rawdata.data.actData;
            this.isFirm = !rawdata.data.isFirm;
          } else {
            console.log(rawdata.data.errorMsg);
          }
        }
        console.log(rawdata, this.company);
      } catch (err) {
        console.log(err);
      }
    },
    FilterActData() {
      console.log("enter filter data");
      let result = [];
      if (this.actData.length === 0) return null;
      this.actData.filter((item) => {
        if (item.firmno == this.company) {
          result.push(item);
        }
      });
      console.log("filter have", result, this.actData);
      return result;
    },
    FilterActDataDemo() {
      console.log("DEMO enter filter data");
      let result = [];
      if (actData.length === 0) return null;
      actData.filter((item) => {
        if (item.firmno == this.company) {
          result.push(item);
        }
      });
      this.actData = actData;
      console.log("filter have", result, this.actData);
      return result;
    },
  },
});
</script>
<style scoped>
.col {
  font-size: 16px;
}

.CardColor {
  color: #f5f5f5;
  background-color: #5fa9f8;
}

.InfoColor {
  color: #f5f5f5;
  background-color: #f8af39;
}

.CardTitleText {
  font-size: 20px;
  font-weight: 800;
}

.cardbodybg {
  background-color: #f1f8ff;
}

.InfoBodyColor {
  background-color: #fff5d0;
}
</style>
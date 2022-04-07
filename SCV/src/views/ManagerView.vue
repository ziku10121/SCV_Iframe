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

    <!-- #region 客戶管理 -->
    <div v-for="item in filterData" :key="item">
      <ActList :ActData="item"></ActList>
    </div>

    <!-- 風險 -->
    <div class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        風險
      </div>
      <div class="card-body cardbodybg">
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="200px">風險類別</th>
              <th scope="col" width="200px">風險指標</th>
              <th scope="col">值</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row" rowspan="5" style="vertical-align : middle;text-align:center;">1. 商品類</td>
              <td scope="row">台股高風險股票庫存</td>
              <td>%</td>
            </tr>
            <tr>
              <td scope="row">台股整戶維持率</td>
              <td>%</td>
            </tr>
            <tr>
              <td scope="row">不限用途維持率</td>
              <td>%</td>
            </tr>
            <tr>
              <td scope="row">借券擔保維持率</td>
              <td>%</td>
            </tr>
            <tr>
              <td scope="row">期貨維持率</td>
              <td>%</td>
            </tr>
            <tr>
              <td scope="row" rowspan="11" style="vertical-align : middle;text-align:center;">2. 財管類</td>
              <td scope="row">台股已實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">台股未實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">期貨已實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">期貨未實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">基金已實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">基金未實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">海外債已實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">海外債未實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">結構型已實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">結構型未實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row">當沖交易已實現損益</td>
              <td>$</td>
            </tr>
            <tr>
              <td scope="row" rowspan="3" style="vertical-align : middle;text-align:center;">3. 帳戶類</td>
              <td scope="row">客戶KYC</td>
              <td>C1</td>
            </tr>
            <tr>
              <td scope="row">客戶風險燈號</td>
              <td>紅</td>
            </tr>
            <tr>
              <td scope="row">禁銷客戶</td>
              <td>N</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- 軌跡 -->
    <div class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        軌跡
      </div>
      <div class="card-body cardbodybg">
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="400px">軌跡項目</th>
              <th scope="col">參考值</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row">E01最後上線日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">E點通最後上線日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">網路交易最後交易日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">電話委託最後交易日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">網路交易佔比</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">客服最後聯繫日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">行銷Call客最後聯繫日</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">臨櫃服務最後接觸日</td>
              <td></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- 費率 -->
    <div class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        費率
      </div>
      <div class="card-body cardbodybg">
        <table class="table">
          <thead class="thead-dark table-active">
            <tr>
              <th scope="col" width="200px">帳戶別</th>
              <th scope="col" width="200px">費率條件</th>
              <th scope="col">參考值</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td scope="row" rowspan="14" style="vertical-align : middle;text-align:center;">台股</td>
              <td scope="row">一般月折讓</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">電子特殊月折</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">上市/櫃手續費日折</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">電子日折</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">興櫃手續費日折</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">興櫃一般接單</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">融資利率日折</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">融券利率日折</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">融券手續費(留倉)</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">融券手續費(當沖)</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">不限用途-利率</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">不限用途-手續費</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">一般接單費</td>
              <td></td>
            </tr>
            <tr>
              <td scope="row">電子接單費</td>
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
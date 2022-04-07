<template>
  <!-- #region Error Page -->
  <div v-if='isError' class="container vh-100">
      <div class="row vh-100 align-items-center">
          <div>
              <img alt="Vue logo" src="../assets/warning.png"  width="150" height="150"/>
              <h3 class="mt-2">Error！！</h3>
          </div>
      </div>
  </div>
  <!-- #endregion -->
  <div v-if='!isError' class="container-fluid pt-4 pb-4 mt-5">
    <div v-if="isLoading" class="row justify-content-center align-items-center">
      <div class="col-auto spinner-border" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
      <div class="col-auto">Loading...</div>
    </div>
    <!-- #region Company -->
    <div v-if="!isLoading" class="row fixed-top">
      <div class="bg-white ps-4 pe-4 pt-3 pb-3">
        <div class="col-12 col-md-6 col-lg-4">
          <div class="input-group flex-nowrap">
            <label for="inputGroupSelect01" class="input-group-text">分公司</label>
            <select class="form-select form-select-sm" id="inputGroupSelect01" v-model="company" :disabled="isFirm">
              <option v-for="item in comData" :key="item" :value="item.firmno">{{item.firmnm}}</option>
            </select>
          </div>
        </div>
      </div>
    </div>
    <!-- #endregion -->
    <!--  #region 資產總攬 -->
    <div v-if="!isLoading" class="row pt-5">
      <div class="col-12 col-lg-8">
        <div class="row row-cols-1 g-2 justify-content-center">
          <h5 class="col-12 p-2 text-right align-middle TitleColor">
            資產
          </h5>
          <div class="col-4 text-right align-middle"><strong>資產類別</strong></div>
          <div class="col-4 text-right align-middle"><strong>庫存餘額</strong></div>
          <div class="col-4 text-right align-middle"><strong>佔比</strong></div>
          <hr/>
          <div v-for="item,key in filterData" :key="item" class="row ps-0 pe-0 pb-2" data-bs-toggle="collapse" :href="'#collapseExample'+key" role="button" aria-expanded="false" :aria-controls="'collapseExample'+key">
            <AssetList :FilterData="item" :Key="key" :Asset="asset"/>
          </div>
          <hr/>
          <div class="row ps-0 pe-0 pb-2">
            <div class="col-4 text-right align-middle">合計</div>
            <div class="col-4 text-right align-middle">{{asset.assetp}}</div>
            <div class="col-4 text-right align-middle"></div>
          </div>
        </div>
      </div>
      <!-- #region 圓餅圖 -->
      <div class="col-12 col-lg-4 d-flex justify-content-center">
        <Chart style="height: 400px; width: 400px;" v-bind:chartData="state.chartData" v-bind:chartOptions="state.chartOptions" />
      </div>
      <!-- #endregion -->
    </div>
    <!-- #endregion -->
  </div>
</template>

<script>
//#region 測試資料
var actData=[
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王XX",
    },
    firmno:"9654",
    acttype:"001",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"null",
    dtrdlmt:"null",
    eopndate:"null",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王XX",
    },
    firmno:"9654",
    acttype:"002",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"1",
    dtrdlmt:"1111",
    eopndate:"2020/12/22",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王OO",
    },
    firmno:"9654",
    acttype:"003",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"1",
    dtrdlmt:"1111",
    eopndate:"2020/12/22",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王OO",
    },
    firmno:"9654",
    acttype:"004",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"1",
    dtrdlmt:"1111",
    eopndate:"2020/12/22",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王OO",
    },
    firmno:"9655",
    acttype:"005",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"1",
    dtrdlmt:"1111",
    eopndate:"2020/12/22",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王OO",
    },
    firmno:"9655",
    acttype:"006",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"1",
    dtrdlmt:"1111",
    eopndate:"2020/12/22",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王XX",
    },
    firmno:"9661",
    acttype:"001",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"null",
    dtrdlmt:"null",
    eopndate:"null",
  },
  {
    BasicInfo:{
      actno:"0000000",
      idno:"A248678996",
      name:"Test1",
      cusgrptype:"高貢獻客戶",
      gender:"1",
      age:"20",
      bday:"2020/09/06",
      mate:"王XX",
    },
    firmno:"9661",
    acttype:"002",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"1263156",
    hraddr:"台中市西屯區惠來路二段101號",
    caddr:"台中市西屯區惠來路二段101號",
    contact:"王小明",
    agent:"王小明",
    cussrctyp:"自開",
    translmt:"999999",
    evalidflag:"1",
    banknm:"00000000000",
    emgstklmt:"88888",
    crvalidflag:"1",
    mgntralmt:"999889",
    sselllmt:"999999",
    dtrdflag:"1",
    piflag:"2",
    actclsdate:"1",
    dtrdlmt:"1111",
    eopndate:"2020/12/22",
  },
]
//#endregion
import { defineComponent } from 'vue';
import {GetAccountList} from '../api/Api_manager.js';
import {GetAssestList} from '../api/Api_manager.js';

import format from "../utility/format";
import AssetList from '../components/AssetList.vue';
import Chart from '../components/chart.vue';

export default defineComponent({
    name: 'App',
    components: {
      Chart,AssetList
    },
    data () {
      return {
        isLoading:true,
        isError:false,
        state: {
            chartData: {
              datasets: [
                {
                  data: [24000,10000,2000,0,400,1400],
                  backgroundColor: ['#FF9D3B','#00d2a9','#71CCF3','#749DD3','#b2bec3','#FEE06E']
                }
              ],
              // These labels appear in the legend and in the tooltips when hovering different arcs
              labels: ['台股帳戶', '複委託帳戶', 'OSU帳戶', '期貨帳戶', '信託帳戶', '資金管理帳戶']
            },
            chartOptions: {
              responsive: true
            }
        },
        company:"",
        comData:[],
        actData:[],
        filterData:{},
        isFirm:false,
        quest:{
          "idno":'',
          "userid":'',
          "staffno":'',
          "dep":'',
          "gr":'',
          "bh":'',
          "level":'',
        },
        assestQuest:[],
        assetData:[],
        asset:{},
        chartDatas:[],
        labels:[]
      }
    },
    watch:{
      company:function(){
        var self = this;
        let tempData=[];
        this.labels.length=0;

        this.filterData = this.FilterActData();
        this.assetData.forEach(function(item){
          if(item.firmno==self.company){
            self.asset = item;
          }
        })
        if(this.assetData.length!=0)this.Convertion();
        for(let x=0; x<this.filterData.length;x++){
          tempData.push(this.filterData[x].totalp);
          this.labels.push(this.filterData[x].acttypenm);
        }

        this.chartDatas = tempData;
        this.state.chartData={
          datasets:[{
            data: this.chartDatas,
            backgroundColor: this.state.chartData.datasets[0].backgroundColor
          }],
          labels: this.labels
        }
      }
    },
    created(){
      this.Geturlparams();
    },
    methods:{
      Geturlparams(){
        const queryString = window.location.search;
        const geturlParams = new URLSearchParams(queryString); 
        console.log("外面接收網址",queryString);
        if(queryString!='')
        {
          this.quest.idno = geturlParams.get('id');
          this.quest.userid = geturlParams.get('userid');
          this.quest.staffno = geturlParams.get('staffno');
          this.quest.dep = geturlParams.get('dep');
          this.quest.gr = geturlParams.get('gr');
          this.quest.bh = geturlParams.get('bh');
          this.quest.level = format.level(geturlParams.get('level'));
          console.log('查詢帳戶需求:',this.quest);
          this.PostAccountApi();
        }
      },
      async PostAccountApi(){
        try{
          var data = JSON.stringify(this.quest);
          var rawdata = await GetAccountList(data);
          if(rawdata.status==200){
            if(rawdata.data.status==1){
              this.comData = rawdata.data.AllCompany;
              this.actData = rawdata.data.actData;
              this.isFirm = !rawdata.data.isFirm;
              this.ActtypeFormat();
              //接收到帳戶後再查詢資產
              this.AssetProcess();
            }else{
              this.isError=true;
              console.log(rawdata.data.errorMsg);
            }
          }
        }catch(err){
          this.isError=true;
          console.log(err);
        }
      },
      //帳戶處理
      AssetProcess(){
        console.log("處理中",this.comData);
        this.assestQuest.length=0;
        this.comData.forEach(com => {
          let result = [];
          let firmAct={
            firmno:'',
            account:[]
          }

          this.actData.forEach(item => {
            if(item.firmno == com.firmno){
              result.push(item.BasicInfo.actno);
            }
          });

          firmAct.firmno = com.firmno;
          firmAct.account= result;
          this.assestQuest.push(firmAct);
        });
        this.PostAssetApi();
      },
      async PostAssetApi(){
        try{
          var data = JSON.stringify(this.assestQuest);
          console.log('data',data);
          var rawdata = await GetAssestList(data);
          console.log(rawdata);
          if(rawdata.status==200){
            if(rawdata.data.status==1){
              this.assetData = rawdata.data.assetData;
              this.company = this.comData[0].firmno;
              this.isLoading=false;
              this.isError=false;
            }else{
              this.isError=true;
              console.log(rawdata.data.errorMsg);
            }
          }
        }catch(err){
          this.isError=true;
          console.log(err);
        }
      },
      FilterActData(){
        let _acttype = [];
        let result=[];
        if(this.actData.length === 0) return null;
        this.actData.filter(item => {
          //format.acttypeno[7] 自營帳戶
          if(item.acttype== format.acttypeno[7])item.acttype='001';
          if(item.firmno==this.company){
            _acttype.push(item.acttype);
          }
        })
        //刪除重複字串
        _acttype = Array.from(new Set(_acttype));
        //篩成每個陣列擁有object
        _acttype.forEach(type => {
          let obj={acttype:''}
          obj.acttype = type;
          result.push(obj);
        });
        // console.log('filter have',result,this.actData);
        return result;
      },
      FilterActDataDemo(){
        let result = [];
        if(actData.length === 0) return null;
        actData.filter(item => {
          let obj={
            acttype:''
          }
          obj.acttype = item.acttype;
          if(item.firmno==this.company){
            result.push(obj);
          }
        })
        this.actData = actData;
        // console.log('filter have',result,this.actData);
        return result;
      },
      Convertion:function(){
        var twStock = this.asset.twstock;
        var sBroke = this.asset.sbrokerage;
        var osu = this.asset.osu;
        var future = this.asset.future;
        var cma = this.asset.cma;
        var trust = this.asset.trust;
        this.filterData.sort(format.DyanmicSort('acttype'));
        for(var i=0;i<this.filterData.length;i++){
          this.filterData[i].acttypenm = (this.filterData[i].acttype=="002")?format.acttype[1]:format.acttype[parseInt(this.filterData[i].acttype)];
          switch(this.filterData[i].acttype){
            case "001":
              this.filterData[i].totalp = twStock.totalp;
              this.filterData[i].price = twStock.totalp==0 ?'--':twStock.totalp;
              this.filterData[i].pr=twStock.pr;
              break;
            case "003":
              this.filterData[i].totalp = sBroke.totalp;
              this.filterData[i].price = sBroke.totalp ==0 ?'--': sBroke.totalp;
              this.filterData[i].pr=sBroke.pr;
              break;
            case "004":
              this.filterData[i].totalp = osu.totalp;
              this.filterData[i].price = osu.totalp==0 ?'--':osu.totalp;
              this.filterData[i].pr= osu.pr;
              break;
            case "005":
              this.filterData[i].totalp = future.totalp;
              this.filterData[i].price = future.totalp==0 ?'--':future.totalp;
              this.filterData[i].pr=future.pr;
              break;
            case "007":
              this.filterData[i].totalp = cma.totalp;
              this.filterData[i].price = cma.totalp==0 ?'--':cma.totalp;
              this.filterData[i].pr=cma.pr;
              break;
            case "006":
              this.filterData[i].totalp = trust.totalp;
              this.filterData[i].price = trust.totalp==0 ?'--':trust.totalp;
              this.filterData[i].pr=trust.pr;
              break;
            case "002":
              this.filterData[i].totalp = twStock.totalp;
              this.filterData[i].price = twStock.totalp==0 ?'--':twStock.totalp;
              this.filterData[i].pr=twStock.pr;
              break;
            default:
              console.log("帳號類別 ERROR!");
              break;
          }
        }
      },
      dyanmicSort:function(attr){
        return function(a,b){
          a = a[attr];
          b = b[attr];
          if(a < b){
              return  -1;
          }
          if(a > b){
              return  1;
          }
          return 0;
        }
      },
      ActtypeFormat(){
        if(this.actData!=null){
          this.actData.forEach(item => {
            item.acttype = format.acttypeno[parseInt(item.acttype)];
          });
        }
      }
    }
})
</script>
<style scoped>
.col-2 {
  font-size: 14px;
}
.TitleColor{
  color: #f5f5f5;
  background-color: #5fa9f8;
}

.card-body{
  background-color:#f1f8ff;
}
</style>
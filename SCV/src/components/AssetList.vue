<template>
  <div class="col-4 text-right align-middle">
    {{tempData.acttypenm}}
  </div>
  <div class="col-4 text-right align-middle">
    {{tempData.price}}
  </div>
  <div class="col-4 text-right align-middle">{{tempData.pr}}%</div>
  <div v-if="tempData.acttype !='005'" class="col-12 text-right align-middle collapse" :id="'collapseExample'+Key">
    <div class="card card-body">
      <div class="row">
        <div class="col-4 mb-2 text-right align-middle">商品類別</div>
        <div class="col-4 text-right align-middle">總資產</div>
        <div class="col-4 text-right align-middle">佔比</div>
        <hr />
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">現股</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.stockp}}</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.stockpr}}%</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">融資</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.marginp}}</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.marginpr}}%</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">融券</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.ssellp}}</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.ssellpr}}%</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">出借股票</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.loanp}}</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.loanpr}}%</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">不限用途</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.nrecp}}</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.nrecpr}}%</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">境內衍生性金融</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.dfinp}}</div>
        <div v-if="tempData.acttype == '001' || tempData.acttype == '002'" class="col-4 text-right align-middle">{{Asset.twstock.dfinpr}}%</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">海外股票</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.ostockp}}</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.ostockpr}}%</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">海外股票(信用)</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.ocreditp}}</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.ocreditpr}}%</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">海外債</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.odebtp}}</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.odebtpr}}%</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">境外基金</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.ofundp}}</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.ofundpr}}%</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">結構型商品</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.structp}}</div>
        <div v-if="tempData.acttype =='003'" class="col-4 text-right align-middle">{{Asset.sbrokerage.structpr}}%</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">海外股票</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.ostockp}}</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.ostockpr}}%</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">海外債</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.odebtp}}</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.odebtpr}}%</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">境外基金</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.ofundp}}</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.ofundpr}}%</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">結構型商品</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.structp}}</div>
        <div v-if="tempData.acttype =='004'" class="col-4 text-right align-middle">{{Asset.osu.structpr}}%</div>
        <div v-if="tempData.acttype == '005'" class="col-4 text-right align-middle">國內期權</div>
        <div v-if="tempData.acttype == '005'" class="col-4 text-right align-middle">{{Asset.future.dfutp}}</div>
        <div v-if="tempData.acttype == '005'" class="col-4 text-right align-middle">{{Asset.future.dfutpr}}%</div>
        <div v-if="tempData.acttype == '005'" class="col-4 text-right align-middle">國外期貨</div>
        <div v-if="tempData.acttype == '005'" class="col-4 text-right align-middle">{{Asset.future.ofutp}}</div>
        <div v-if="tempData.acttype == '005'" class="col-4 text-right align-middle">{{Asset.future.ofutpr}}%</div>
        <div v-if="tempData.acttype =='006'" class="col-4 text-right align-middle">特定金錢信託</div>
        <div v-if="tempData.acttype =='006'" class="col-4 text-right align-middle">{{Asset.trust.sptrustp}}</div>
        <div v-if="tempData.acttype =='006'" class="col-4 text-right align-middle">{{Asset.trust.sptrustpr}}%</div>
        <div v-if="tempData.acttype =='006'" class="col-4 text-right align-middle">信託借券</div>
        <div v-if="tempData.acttype =='006'" class="col-4 text-right align-middle">{{Asset.trust.trustloanp}}</div>
        <div v-if="tempData.acttype =='006'" class="col-4 text-right align-middle">{{Asset.trust.trustloanpr}}%</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name:'AssetList',
  props:{
    FilterData:{
      type:Object
    },
    Asset:{
      type:Object
    },
    Key:{
      type:Number
    }
  },
  data(){
    return{
      tempData:{}
    }
  },
  watch:{
    FilterData:function(){
      console.log('watch!!!');
      this.ConvertData();
    }
  },
  methods:{
    ConvertData(){
      console.log('convert',this.FilterData,this.Asset)
      var twStock = this.Asset.twstock;
      var sBroke = this.Asset.sbrokerage;
      var osu = this.Asset.osu;
      var future = this.Asset.future;
      var trust = this.Asset.trust;
      
      this.tempData.acttype = this.FilterData.acttype;
      this.tempData.acttypenm = this.FilterData.acttypenm;
      this.tempData.price = this.FilterData.price;
      this.tempData.pr = this.FilterData.pr;
      twStock.stockpr = twStock.totalp===0?0:Math.round(this.FilterData.pr/twStock.totalp * twStock.stockp*10)/10;
      twStock.marginpr = twStock.totalp===0?0:Math.round(this.FilterData.pr/twStock.totalp * twStock.marginp*10)/10;
      twStock.ssellpr = twStock.totalp===0?0:Math.round(this.FilterData.pr/twStock.totalp * twStock.ssellp*10)/10;
      twStock.loanpr = twStock.totalp===0?0:Math.round(this.FilterData.pr/twStock.totalp * twStock.loanp*10)/10;
      twStock.nrecpr = twStock.totalp===0?0:Math.round(this.FilterData.pr/twStock.totalp * twStock.nrecp*10)/10;
      twStock.dfinpr = twStock.totalp===0?0:Math.round(this.FilterData.pr/twStock.totalp * twStock.dfinp*10)/10;

      sBroke.ostockpr = sBroke.totalp===0?0:Math.round(this.FilterData.pr/sBroke.totalp * sBroke.ostockp*10)/10;
      sBroke.odebtpr = sBroke.totalp===0? 0:Math.round(this.FilterData.pr/sBroke.totalp * sBroke.odebtp*10)/10;
      sBroke.ocreditpr = sBroke.totalp===0?0:Math.round(this.FilterData.pr/sBroke.totalp * sBroke.ocreditp*10)/10;
      sBroke.ofundpr = sBroke.totalp===0?0:Math.round(this.FilterData.pr/sBroke.totalp * sBroke.ofundp*10)/10;
      sBroke.structpr = sBroke.totalp===0?0:Math.round(this.FilterData.pr/sBroke.totalp * sBroke.structp*10)/10;

      osu.ostockpr = osu.totalp===0? 0: Math.round(this.FilterData.pr/osu.totalp * osu.ostockp*10)/10;
      osu.odebtpr = osu.totalp===0? 0:Math.round(this.FilterData.pr/osu.totalp * osu.odebtp*10)/10;
      osu.ofundpr = osu.totalp===0? 0:Math.round(this.FilterData.pr/osu.totalp * osu.ofundp*10)/10;
      osu.structpr = osu.totalp===0? 0:Math.round(this.FilterData.pr/osu.totalp * osu.structp*10)/10;

      future.dfutpr = future.totalp===0? 0:Math.round(this.FilterData.pr/future.totalp * future.dfutp*10)/10;
      future.ofutpr = future.totalp===0? 0:Math.round(this.FilterData.pr/future.totalp * future.ofutp*10)/10;

      trust.sptrustpr = trust.totalp===0? 0:Math.round(this.FilterData.pr/trust.totalp * trust.sptrustp*10)/10;
      trust.trustloanpr = trust.totalp===0? 0:Math.round(this.FilterData.pr/trust.totalp * trust.trustloanp*10)/10;
    }
  },
  beforeMount(){
    console.log('asset mounted');
    this.ConvertData();
  }
}
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
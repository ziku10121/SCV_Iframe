<template>
  <div class="card mb-4 shadow-sm bg-body rounded">
    <div class="card-header text-start CardColor CardTitleText">
      {{tempData.acttypenm}}
    </div>
    <div class="card-body cardbodybg">
      <div class="row pt-2 ps-sm-4 pe-sm-4 g-4">
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.BasicInfo.actno}}</div>
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.BasicInfo.idnomsk}}</div>
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.BasicInfo.gender}}</div>
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.BasicInfo.age}}</div>
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">{{tempData.BasicInfo.bday}}</div>
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.BasicInfo.mate}}</div>
        <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
        <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">{{tempData.BasicInfo.cusgrptype}}</div>

        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">開戶日</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">{{tempData.actopndate}}</div>
        <div v-if="tempData.acttype == 5" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">銷戶日期</div>
        <div v-if="tempData.acttype == 5" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">{{tempData.actclsdate}}</div>
        
        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子郵件</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate"
          data-bs-toggle="tooltip" data-bs-placement="top" :title="tempData.email">
          {{tempData.email}}
        </div>
        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">行動電話</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="0912456753">
          {{tempData.mobile}}
        </div>

        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">家庭電話</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.phone}}</div>
        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">戶籍地址</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" :title="tempData.hraddr">{{tempData.hraddr}}</div>
        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">聯絡地址</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" :title="tempData.caddr">{{tempData.caddr}}</div>
        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">緊急聯絡人</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.contact}}</div>

        <div v-if="tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">代理人</div>
        <div v-if="tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.agent}}</div>
        <div v-if="tempData.acttype != 5 && tempData.acttype != 7" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客戶來源</div>
        <div v-if="tempData.acttype != 5 && tempData.acttype != 7" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.cussrctyp}}</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">買賣額度</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.translmt}}</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子戶</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.evalidflag}}</div>

        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">交割銀行</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.banknm}}</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">興櫃額度</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.emgstklmt}}</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">信用戶</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.crvalidflag}}</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">融資額度</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.mgntralmt}}</div>

        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">融券額度</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.sselllmt}}</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">當沖資格</div>
        <div v-if="tempData.acttype == 1 || tempData.acttype == 2" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.dtrdflag}}</div>

        <div v-if="tempData.acttype == 5" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">當沖額度</div>
        <div v-if="tempData.acttype == 5" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.dtrdlmt}}</div>
        <div v-if="tempData.acttype == 5" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子開戶日期</div>
        <div v-if="tempData.acttype == 5" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.eopndate}}</div>

        <div v-if="tempData.acttype==3||tempData.acttype==4" class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">PI資格</div>
        <div v-if="tempData.acttype==3||tempData.acttype==4" class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{tempData.piflag}}</div>
      </div>
    </div>
  </div>
</template>

<script>
import format from "../utility/format";

export default {
  name:'ActList',
  props:{
    ActData:{
      type:Object,
    }
  },
  data(){
    return{
      tempData:{}
    }
  },
  watch:{
    ActData:function(){
      console.log("AcList",this.ActData);
      this.ConvertData();
    }
  },
  methods:{
    //資料convert
    ConvertData(){
      this.tempData = JSON.parse(JSON.stringify(this.ActData));
      
      //帳戶類型
      this.tempData.acttypenm = format.acttype[parseInt(this.ActData.acttype)];
      //電子信箱
      this.tempData.email = (this.ActData.email==''||this.ActData.email==null)?'未填寫':this.ActData.email;
      //更新性別
      this.tempData.BasicInfo.gender = format.gender[this.ActData.BasicInfo.gender];
      //電子戶 信用戶 當沖資格 PI資格
      this.tempData.evalidflag=format.flag[this.ActData.evalidflag];
      this.tempData.crvalidflag=format.flag[this.ActData.crvalidflag];
      this.tempData.dtrdflag=format.flag[this.ActData.dtrdflag];
      this.tempData.piflag=format.flag[this.ActData.piflag];
      //時間
      this.tempData.BasicInfo.bday = format.Dayformat(this.ActData.BasicInfo.bday);
      this.tempData.actopndate = format.Dayformat(this.ActData.actopndate);
      this.tempData.actclsdate=format.Dayformat(this.ActData.actclsdate);
      this.tempData.eopndate = format.Dayformat(this.ActData.eopndate);
      console.log('temp',this.tempData);
    }
  },
  beforeMount(){
    console.log('ActList mounted');
    this.ConvertData();
  }
}
</script>
<style scoped>
.col {
  font-size: 16px;
}

.CardColor{
  color: #f5f5f5;
  background-color: #5fa9f8;
}

.InfoColor{
  color: #f5f5f5;
  background-color: #f8af39;
}

.CardTitleText{
  font-size: 20px;
  font-weight: 800;
}

.cardbodybg{
  background-color: #f1f8ff;
}

.InfoBodyColor{
  background-color: #fff5d0;
}
</style>
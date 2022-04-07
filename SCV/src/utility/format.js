export default{
  gender:{
    1:"男",
    2:"女",
    3:"法人",
    99:"無法判斷"
  },

  flag:{
    1:"是",
    2:"否"
  },

  acttypeno:{
    1:'001',  //台股
    2:'003',  //複委託
    3:'004',  //OSU
    4:'005',  //期權
    5:'007',  //資金管理
    6:'006',  //信託
    7:'002',  //自營
    8:'008',  //壽險
  },

  acttype:{
    1:"台股帳戶",
    2:"自營帳戶",
    3:"複委託帳戶",
    4:"OSU帳戶",
    5:"期權帳戶",
    6:"信託帳戶",
    7:"資金管理帳戶",
    8:"壽險帳戶",
  },

  level(name){
    console.log(name);
    if(name=="undefined") return 99;
    if(name==="全公司") return 1;
    if(name==="部長") return 2;
    if(name==="區長") return 3;
    if(name==="分公司經理人") return 4;
    if(name==="分公司副經理人") return 5;
    if(name==="組長") return 6;
    if(name==="營業員") return 7;
    return null;
  },
  dayformat(dateString) {
    console.log(dateString, Date.parse(dateString));
    if (
      dateString != undefined &&
      dateString != null &&
      this.TrimEmpt(dateString) != ""
    ) {
      var date = this.ParseJsonDate(dateString);
      var YY = date.getFullYear();
      var MM = (date.getMonth() + 1 < 10 ? "0" : "") + (date.getMonth() + 1);
      var dd = (date.getDate() < 10 ? "0" : "") + date.getDate();
      var day = YY + "/" + MM + "/" + dd;
      var dateStringResult = day;
      return dateStringResult;
    }
    return "";
  },
  Dayformat(dateString) {
    if (
        dateString != undefined &&
        dateString != null &&
        this.TrimEmpt(dateString) != ""
    ) {
        var date = this.ParseJsonDate(dateString);
        var YY = date.getFullYear();
        var MM = (date.getMonth() + 1 < 10 ? "0" : "") + (date.getMonth() + 1);
        var dd = (date.getDate() < 10 ? "0" : "") + date.getDate();
        var day = YY + "/" + MM + "/" + dd;
        var dateStringResult = day;
        return dateStringResult;
    }
    return "";
  },
  ParseJsonDate(dateString) {
      var date = new Date(dateString);
      return date;
  },
  TrimEmpt(_str) {
      if (typeof _str == "number") return _str;
      if (typeof _str == "undefined") return "";
      return _str.replace(/^\s+|\s+$/g, "");
  },
  DyanmicSort(attr){
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
  }
}
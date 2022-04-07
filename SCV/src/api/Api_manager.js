//檔案路徑 src/api/檔案名.js
//先行安裝 AXIOS，終端機輸入 npm install axios
const axios = require('axios').default;

const SCVAPI = axios.create({
    baseURL: SCVAPIUrl,
    // baseURL:'http://10.10.53.210:8081/',
    // baseURL:'https://crmdemo.crmdev.com:8082/',
    headers: {
        'Content-Type': 'application/json'
    },
});

//取的客戶帳戶
export const GetAccountList = (_id) => SCVAPI.post('api/Account/get',_id);
export const GetAssestList = (_id) => SCVAPI.post('api/QueryAsset/get',_id);
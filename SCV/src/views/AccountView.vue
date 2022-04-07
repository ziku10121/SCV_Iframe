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
  <div v-if='!isError' class="container-fluid pt-4 pb-5 mt-5">
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
    <!-- #region 帳務總攬 -->
    <div v-if="!isLoading">
      <div v-for="item in filterData" :key="item">
        <ActList :ActData="item"></ActList>
      </div>
    </div>
    <!-- #endregion -->

    <!-- #region  -->
    <!-- 台股帳戶 -->
    <!-- <div v-for="item in actData" :key="item" class="card mt-5 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        台股帳戶{{item.acttype}}
      </div>
      <div class="card-body cardbodybg">
        <div class="row pt-2 ps-sm-4 pe-sm-4 g-4">
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.BasicInfo.account}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.BasicInfo.id}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.BasicInfo.gender}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.BasicInfo.age}}</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">{{item.BasicInfo.birthday}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.BasicInfo.spouse}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">{{item.BasicInfo.cusgroup}}</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">開戶日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">{{item.opendate}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子郵件</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate"
            data-bs-toggle="tooltip" data-bs-placement="top" title="SYSTEX@example.com">
            {{item.email}}
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">行動電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="0912456753">
            {{item.cellphone}}
          </div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">家庭電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.homephone}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">戶籍地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">聯絡地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 fs- text-start">緊急聯絡人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.contact}}</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">代理人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.agent}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客戶來源</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.cuspltfm}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">買賣額度</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.bs_limit}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子戶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.e_account}}</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">交割銀行</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.settle_bank}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">興櫃額度</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.emerg_limit}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">信用戶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.credit_account}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">融資額度</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.margin_limit}}</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">融券額度</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.stock_limit}}</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">當沖資格</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">{{item.dtrdflag}}</div>
        </div>
      </div>
    </div> -->
    <!-- 複委託帳戶 -->
    <!-- <div v-if="authority.subBrokerage" class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        複委託帳戶
      </div>
      <div class="card-body cardbodybg">
        <div class="row pt-2 ps-sm-4 pe-sm-4 g-4">
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">0000000</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">2222223123</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">男</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">20</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王XX</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">高貢獻客戶</div>

          
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">開戶日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">
            2020/09/06
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子郵件</div>
          <div
            class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="SYSTEX@example.com"
          >
            SYSTEX@example.com
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">行動電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="0912456753">
            0912456753
          </div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">家庭電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="1263156">1263156</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">戶籍地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">聯絡地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">緊急聯絡人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">代理人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客戶來源</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">自開</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">PI資格</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">Y</div>
        </div>
      </div>
    </div> -->
    <!-- OSU帳戶 -->
    <!-- <div v-if="authority.osu" class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        OSU帳戶
      </div>
      <div class="card-body cardbodybg">
        <div class="row pt-2 ps-sm-4 pe-sm-4 g-4">
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">0000000</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">2222223123</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">男</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">20</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王XX</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">高貢獻客戶</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">開戶日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">
            2020/09/06
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子郵件</div>
          <div
            class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="SYSTEX@example.com"
          >
            SYSTEX@example.com
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">行動電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="0912456753">
            0912456753
          </div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">家庭電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="1263156">1263156</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">戶籍地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">聯絡地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">緊急聯絡人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">代理人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客戶來源</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">自開</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">PI資格</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">Y</div>
        </div>
      </div>
    </div> -->
    <!-- 期貨帳戶 -->
    <!-- <div v-if="authority.future" class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        期貨帳戶
      </div>
      <div class="card-body cardbodybg">
        <div class="row pt-2 ps-sm-4 pe-sm-4 g-4">
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">0000000</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">2222223123</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">男</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">20</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王XX</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">高貢獻客戶</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">開戶日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">銷戶日期</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子郵件</div>
          <div
            class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="SYSTEX@example.com"
          >
            SYSTEX@example.com
          </div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">行動電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="0912456753">0912456753</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">家庭電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="1263156">1263156</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">戶籍地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">
            台中市西屯區惠來路二段101號
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">聯絡地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">
            台中市西屯區惠來路二段101號
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">緊急聯絡人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">當沖資格</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">Y</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">當沖額度</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">999999</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子開戶日期</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>
        </div>
      </div>
    </div> -->
    <!-- 信託帳戶 -->
    <!-- <div v-if="authority.trust" class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        信託帳戶
      </div>
      <div class="card-body cardbodybg">
        <div class="row pt-2 ps-sm-4 pe-sm-4 g-4">
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">0000000</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">2222223123</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">男</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">20</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王XX</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">高貢獻客戶</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">開戶日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">2020/09/06</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">電子郵件</div>
          <div
            class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate"
            data-bs-toggle="tooltip"
            data-bs-placement="top"
            title="SYSTEX@example.com"
          >
            SYSTEX@example.com
          </div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">行動電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="0912456753">0912456753</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">家庭電話</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">1263156</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">戶籍地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">聯絡地址</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="台中市西屯區惠來路二段101號">台中市西屯區惠來路二段101號</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">緊急聯絡人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">代理人</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王小明</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客戶來源</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">自開</div>
        </div>
      </div>
    </div> -->
    <!-- 資金管理帳戶 -->
    <!-- <div v-if="authority.cma" class="card mt-4 shadow-sm bg-body rounded">
      <div class="card-header text-start CardColor CardTitleText">
        資金管理帳戶
      </div>
      <div class="card-body cardbodybg">
        <div class="row pt-2 ps-4 pe-4 g-4">
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">帳號</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">0000000</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">ID</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">2222223123</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">性別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">男</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">年齡</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">20</div>

          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">出生年月日</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="2020/09/06">2020/09/06</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">配偶</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start">王XX</div>
          <div class="col-4 col-sm-2 col-md-2 col-lg-1 text-start">客群類別</div>
          <div class="col-8 col-sm-4 col-md-2 col-lg-2 text-start text-truncate" title="高貢獻客戶">高貢獻客戶</div>
        </div>
      </div>
    </div> -->
    <!-- #endregion -->
  </div>
</template>
<script>
// var comData=[
//     {
//       company:1,
//       authority:{
//         twStock:true,
//         subBrokerage:true,
//         osu:true,
//         future:true,
//         trust:true,
//         cma:true
//       },
//     },
//     {
//       company:2,
//       authority:{
//         twStock:true,
//         subBrokerage:true,
//         osu:true,
//         future:true,
//         trust:false,
//         cma:false
//       }
//     },
//     {
//       company:3,
//       authority:{
//         twStock:true,
//         subBrokerage:true,
//         osu:false,
//         future:false,
//         trust:false,
//         cma:false
//       }
//     }
//   ]
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
    firmno:"9647",
    acttype:"001",
    orign:"001",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"111111",
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
    firmno:"9647",
    acttype:"002",
    orign:"002",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"222222",
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
    firmno:"9647",
    acttype:"003",
    orign:"003",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"33333",
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
    firmno:"9647",
    acttype:"004",
    orign:"004",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"44444444",
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
    firmno:"9647",
    acttype:"005",
    orign:"005",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"555555555",
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
    firmno:"9647",
    acttype:"006",
    orign:"006",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"6666666",
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
    firmno:"9647",
    acttype:"007",
    orign:"007",
    actopendate:"2020/09/06",
    email:"SYSTEX@example.com",
    mobile:"0912456753",
    phone:"7777777",
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
  }
]

import { defineComponent } from 'vue';
import {GetAccountList} from '../api/Api_manager.js';

import ActList from '../components/ActList.vue';
import format from '../utility/format';

export default defineComponent({
    name: 'App',
    components: { ActList },
    data () {
      return {
        isLoading:true,
        isError:false,
        company:"",
        comData:[],
        actData:[],
        filterData:[],
        isFirm:false,
        quest:{
          "idno":'',
          "userid":'',
          "staffno":'',
          "dep":'',
          "gr":'',
          "bh":'',
          "level":'',
        }
      }
    },
    watch:{
      company:function(){
        console.log('firm change');
        this.filterData = this.FilterActData();
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
          console.log(this.quest);
          this.PostAccountApi();
        }
      },
      async PostAccountApi(){
        try{
          var data = JSON.stringify(this.quest);
          console.log(data);
          var rawdata = await GetAccountList(data);
          console.log(rawdata);
          if(rawdata.status==200){
            if(rawdata.data.status==1){
              this.comData = rawdata.data.AllCompany;
              this.company = this.comData[0].firmno;
              this.actData = rawdata.data.actData;
              this.isFirm = !rawdata.data.isFirm;
              this.isLoading=false;
              this.isError=false;
              this.ActtypeFormat();
            }else{
              this.isError=true;
              console.log(rawdata.data.errorMsg);
            }
          }
          console.log(rawdata.data.AllCompany);
        }catch(err){
          this.isError=true;
          console.log(err);
        }
      },
      FilterActData(){
        console.log("enter filter data");
        let result = [];
        if(this.actData.length === 0) return null;
        this.actData.filter(item => {
          if(item.firmno==this.company){
            result.push(item);
          }
        })
        result.sort(format.DyanmicSort('acttype'));
        // console.log('filter have',result,this.actData);
        return result;
      },
      FilterActDataDemo(){
        console.log("DEMO enter filter data");
        let result = [];
        if(actData.length === 0) return null;
        actData.filter(item => {
          if(item.firmno==this.company){
            result.push(item);
          }
        })
        result.sort(format.DyanmicSort('acttype'));
        // console.log('filter have',result,this.actData);
        return result;
      },
      ActtypeFormat(){
        if(this.actData!=null){
          this.actData.forEach(item => {
            item.acttype = format.acttypeno[parseInt(item.acttype)];
          });
        }
        if(actData!=null){
          actData.forEach(item => {
            item.acttype = format.acttypeno[parseInt(item.acttype)];
          });
        }
        console.log(actData);
      }
    }
})
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
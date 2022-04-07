<template>
    <div class="col-2 align-self-center">
        <div class="text-center" style="writing-mode: vertical-rl">{{title}}</div>
    </div>
    <div class="col-10">
        <ul class="list-group">
            <li class="list-group-item border-0" >
                <div class="row" v-for="(item,index) in Data" :key="index">
                        <div class="col-6">{{item.name}}</div>
                        <div class="col-6">{{item.value}}</div>          
                </div>
            </li>
        </ul>
    </div>
</template>

<script>
export default {
    name: 'list',
    props: {
        title:{
            type:String,
            default:"123456"
        },
        Data:{
            type:Object
        }
    }
}
</script>

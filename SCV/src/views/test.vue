<template>
    <MonthlyChart v-bind:chartData="state.chartData" v-bind:chartOptions="state.chartOptions" />
</template>
<script>
import { defineComponent } from 'vue'
import MonthlyChart from '../components/chart.vue'

export default defineComponent({
    name: 'App',
    components: {
        MonthlyChart
    },
    data () {
        return {
        state: {
            chartData: {
            datasets: [
                {
                data: [1, 2, 3, 4],
                backgroundColor: ['Red', 'Yellow', 'Blue', 'Green', 'black']
                }
            ],
            // These labels appear in the legend and in the tooltips when hovering different arcs
            labels: ['Red', 'Yellow', 'Blue', 'Green']
            },
            chartOptions: {
            responsive: false
            }
        }
        }
    }
})
</script>
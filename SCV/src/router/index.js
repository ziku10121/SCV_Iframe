import { createRouter, createWebHistory } from 'vue-router'
import AccountView from '../views/AccountView.vue'
import AssetsView from '../views/AssetsView.vue'
import ActivityView from '../views/ActivityResultView.vue'
import ManagerView from '../views/ManagerView.vue'
import MarketingView from '../views/MarketingView.vue'
import Home from '../views/Home.vue'
import test from '../views/test.vue'

const routes = [{
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/AccountView',
        name: 'AccountView',
        component: AccountView
    },
    {
        path: '/AssetsView',
        name: 'AssetsView',
        component: AssetsView
    },
    {
        path: '/test',
        name: 'test',
        component: test
    },
    {
        path: '/ActivityResultView',
        name: 'ActivityResultView',
        component: ActivityView
    },
    {
        path: '/ManagerView',
        name: 'ManagerView',
        component: ManagerView
    },
    {
        path: '/MarketingView',
        name: 'MarketingView',
        component: MarketingView
    },
    {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () =>
            import ( /* webpackChunkName: "about" */ '../views/About.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
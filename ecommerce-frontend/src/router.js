import { createRouter, createWebHistory } from "vue-router";
import ProductList from "./components/ProductList.vue";
import ProductDetails from "./components/ProductDetails.vue";

const routes = [
  { path: "/", name: "Home", component: ProductList },
  { path: "/products/:id", name: "ProductDetails", component: ProductDetails, props: true },
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
});

<template>
  <section class="container">
    <h1>Products</h1>

    <div class="filters">
      <input v-model="search" placeholder="Search products..." @input="reload" />

      <select v-model="category" @change="reload">
        <option value="">All Categories</option>
        <option v-for="c in categories" :key="c" :value="c">{{ c }}</option>
      </select>

      <label>
        Min Price: <input type="number" v-model.number="minPrice" @input="reload" />
      </label>
      <label>
        Max Price: <input type="number" v-model.number="maxPrice" @input="reload" />
      </label>

      <select v-model.number="pageSize" @change="reload">
        <option :value="4">4</option>
        <option :value="8">8</option>
        <option :value="12">12</option>
      </select>
    </div>

    <div class="grid">
      <article v-for="p in products" :key="p.id" class="card">
        <router-link :to="{ name: 'ProductDetails', params: { id: p.id } }">
          <img :src="p.imageUrl" alt="" />
          <h3>{{ p.name }}</h3>
          <p>Rs:{{ p.price }}</p>
        </router-link>

        <button @click="addToCart(p)" :disabled="!p.isInStock">
          {{ p.isInStock ? "Add to Cart" : "Out of Stock" }}
        </button>
      </article>
    </div>

    <div class="pagination">
      <button :disabled="page===1" @click="go(page-1)">Prev</button>
      <span>Page {{ page }} / {{ totalPages }}</span>
      <button :disabled="page===totalPages" @click="go(page+1)">Next</button>
    </div>
  </section>
</template>

<script>
import { api } from "../api";

export default {
  data() {
    return {
      products: [],
      totalItems: 0,
      categories: ["Electronics", "Fashion", "Home", "Furniture", "Books"],

      // filters
      search: "",
      category: "",
      minPrice: null,
      maxPrice: null,

      // pagination
      page: 1,
      pageSize: 8,
    };
  },
  computed: {
    totalPages() {
      return Math.max(1, Math.ceil(this.totalItems / this.pageSize));
    },
  },
  methods: {
    async fetchProducts() {
      const params = {
        search: this.search || undefined,
        category: this.category || undefined,
        minPrice: this.minPrice ?? undefined,
        maxPrice: this.maxPrice ?? undefined,
        page: this.page,
        pageSize: this.pageSize,
      };

      const { data } = await api.get("/products", { params });
      this.products = data.products;
      this.totalItems = data.totalItems;
    },
    reload() {
      this.page = 1;
      this.fetchProducts();
    },
    go(p) {
      this.page = p;
      this.fetchProducts();
    },
    addToCart(p) {
      alert(`Added ${p.name} to cart!`);
    },
  },
  mounted() {
    this.fetchProducts();
  },
};
</script>

<style>
/* Base styles and container setup */
.container {
  max-width: 1200px;
  margin: auto;
  padding: 2rem;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
  color: #333;
  background-color: #f4f7f6;
  border-radius: 12px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.05);
}

h1 {
  font-size: 2.5rem;
  color: #2c3e50;
  text-align: center;
  margin-bottom: 2rem;
}

/* --- */

/* Filters section styling */
.filters {
  display: flex;
  gap: 1rem;
  flex-wrap: wrap;
  align-items: center;
  justify-content: center;
  background-color: #fff;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
  margin-bottom: 2rem;
}

.filters input,
.filters select {
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 1rem;
  transition: border-color 0.3s, box-shadow 0.3s;
  width: 75;
  max-width: 250px;
}

.filters input:focus,
.filters select:focus {
  border-color: #4a90e2;
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.2);
  outline: none;
  width:50%;
}

.filters label {
  display: flex;
  flex-direction: column;
  font-size: 0.9rem;
  color: #555;
  width: 100%;
  max-width: 150px;
}

.filters label input {
  margin-top: 0.25rem;
}


.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 2rem;
}

.card {
  border: 1px solid #eee;
  padding: 1rem;
  border-radius: 12px;
  text-align: center;
  background-color: #fff;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  overflow: hidden;
}

.card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.12);
}

.card img {
  width: 100%;
  max-height: 200px;
  object-fit: contain;
  border-radius: 8px;
  margin-bottom: 1rem;
}

.card h3 {
  font-size: 1.25rem;
  color: #2c3e50;
  margin: 0.5rem 0;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.card p {
  font-size: 1.5rem;
  color: #4a90e2;
  font-weight: bold;
  margin: 0.5rem 0 1rem;
}

.card button {
  width: 50%;
  padding: 0.75rem;
  border: none;
  border-radius: 6px;
  background-color: #4a90e2;
  color: #fff;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s;
}

.card button:hover:not(:disabled) {
  background-color: #357abd;
  transform: translateY(-2px);
}

.card button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}


.pagination {
  margin-top: 2rem;
  display: flex;
  gap: 1rem;
  justify-content: center;
  align-items: center;
}

.pagination button {
  padding: 0.75rem 1.5rem;
  border: 1px solid #ddd;
  border-radius: 6px;
  background-color: #fff;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s, border-color 0.3s;
}

.pagination button:hover:not(:disabled) {
  background-color: #f0f0f0;
  border-color: #ccc;
}

.pagination button:disabled {
  color: #aaa;
  cursor: not-allowed;
}

.pagination span {
  font-size: 1rem;
  color: #555;
  font-weight: 500;
}
</style>
<template>
  <section v-if="product" class="product-details-container">
    <div class="product-header">
      <button @click="goBack" class="back-arrow-btn">&larr; Back</button>
      <img v-if="product.imageUrl" :src="product.imageUrl" class="product-image" :alt="product.name" />
      <div class="product-info">
        <h2 class="product-name">{{ product.name }}</h2>
        <p class="product-price">Rs:{{ product.price }}</p>
        <p class="product-category"><strong>Category:</strong> {{ product.category }}</p>
        <button
          class="add-to-cart-btn"
          :disabled="!product.isInStock"
          @click="addToCart(product)"
          :title="product.isInStock ? 'Add to Cart' : 'Out of Stock'"
        >
          {{ product.isInStock ? 'Add to Cart' : 'Out of Stock' }}
        </button>
      </div>
    </div>

    <details class="product-description-details">
      <summary class="summary-title">More Details</summary>
     <p class="description-content">{{ product.description }}</p>
        </details>

    <h3 class="related-products-title">Related Products</h3>
    <div class="carousel-container">
      <button class="carousel-arrow carousel-arrow-left" @click="scrollCarousel(-1)" :disabled="disableLeftArrow">&larr;</button>
      <div class="related-products-carousel" ref="carousel">
        <router-link
          v-for="r in related"
          :key="r.id"
          :to="{ name: 'ProductDetails', params: { id: r.id } }"
          class="related-product-card"
        >
          <img v-if="r.imageUrl" :src="r.imageUrl" class="related-product-image" :alt="r.name" />
          <div class="related-product-name">{{ r.name }}</div>
          <div class="related-product-price">Rs:{{ r.price }}</div>
          <div class="related-product-stock" :class="{ 'out-of-stock': !r.isInStock }">
            {{ r.isInStock ? 'In Stock' : 'Out of Stock' }}
          </div>
        </router-link>
      </div>
      <button class="carousel-arrow carousel-arrow-right" @click="scrollCarousel(1)" :disabled="disableRightArrow">&rarr;</button>
    </div>

    <div v-if="showToast" class="toast">Added {{ product.name }} to cart!</div>
  </section>
  <div v-else class="loading-state">Loading product...</div>
</template>

<script>
import { api } from "../api";

export default {
  props: {
    id: {
      type: [String, Number],
      required: true,
    },
  },
  data() {
    return {
      product: null,
      related: [],
      showToast: false,
      disableLeftArrow: true,
      disableRightArrow: false,
    };
  },
  watch: {
    id(newId) {
      if (newId) {
        this.fetchData();
      }
    },
  },
  async created() {
    await this.fetchData();
  },
  mounted() {
    if (this.$refs.carousel) {
      this.$refs.carousel.addEventListener("scroll", this.updateArrowStates);
      this.updateArrowStates();
    }
  },
  beforeDestroy() { // Vue 2 (use 'beforeUnmount' for Vue 3)
    if (this.$refs.carousel) {
      this.$refs.carousel.removeEventListener("scroll", this.updateArrowStates);
    }
  },
  methods: {
    goBack() {
      this.$router.back();
    },
    async fetchData() {
      try {
        this.product = null; // Show loading state
        // Fetch product by id (aligned with pagination for consistency)
        const { data } = await api.get("/products", { params: { page: 1, pageSize: 100 } });
        this.product = data.products.find(p => p.id == this.id);
        console.log("Product:", this.product);

        // Fetch related products
        const relatedRes = await api.get(`/products/${this.id}/related`);
        this.related = relatedRes.data;
        console.log("Related products:", this.related);

        // Update carousel arrow states
        this.$nextTick(() => {
          this.updateArrowStates();
        });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    },
    addToCart(product) {
      if (!product.isInStock) return; // Prevent adding out-of-stock items
      const cart = JSON.parse(localStorage.getItem("cart") || "[]");
      cart.push({ id: product.id, name: product.name, price: product.price, imageUrl: product.imageUrl });
      localStorage.setItem("cart", JSON.stringify(cart));
      this.showToast = true;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
    },
    scrollCarousel(direction) {
      const carousel = this.$refs.carousel;
      if (carousel) {
        const scrollAmount = 220; // Width of one card (200px) + gap (20px)
        carousel.scrollBy({ left: direction * scrollAmount, behavior: "smooth" });
        setTimeout(() => this.updateArrowStates(), 300);
      }
    },
    updateArrowStates() {
      const carousel = this.$refs.carousel;
      if (carousel) {
        this.disableLeftArrow = carousel.scrollLeft <= 0;
        this.disableRightArrow = carousel.scrollLeft + carousel.clientWidth >= carousel.scrollWidth - 1;
      }
    },
  },
};
</script>

<style scoped>
.product-details-container {
  max-width: 900px;
  margin: 2rem auto;
  padding: 2.5rem;
  background-color: #fff;
  border-radius: 12px;
  box-shadow: 0 6px 30px rgba(0, 0, 0, 0.08);
  font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
  color: #333;
}

.product-header {
  display: flex;
  gap: 2rem;
  margin-bottom: 2.5rem;
  flex-wrap: wrap;
  align-items: flex-start;
}

.product-image {
  width: 100%;
  max-width: 350px;
  height: auto;
  object-fit: contain;
  border-radius: 10px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05);
  flex-shrink: 0;
}

.product-info {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.product-name {
  font-size: 2.5rem;
  color: #2c3e50;
  margin-bottom: 0.75rem;
  line-height: 1.2;
}

.product-price {
  font-size: 2rem;
  color: #e44d26;
  font-weight: bold;
  margin-bottom: 1rem;
}

.product-category {
  font-size: 1.1rem;
  color: #555;
  margin-bottom: 1.5rem;
}

.add-to-cart-btn {
  background-color: #4a90e2;
  color: #fff;
  padding: 1rem 1.75rem;
  border: none;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
  align-self: flex-start;
}

.add-to-cart-btn:hover:not(:disabled) {
  background-color: #357abd;
  transform: translateY(-2px);
}

.add-to-cart-btn:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.product-description-details {
  background-color: #f9f9f9;
  border: 1px solid #eee;
  border-radius: 10px;
  margin-bottom: 3rem;
  padding: 1.5rem;
}

.summary-title {
  font-size: 1.3rem;
  font-weight: bold;
  color: #2c3e50;
  cursor: pointer;
  padding-bottom: 0.5rem;
}

.description-content {
  font-size: 1rem;
  line-height: 1.6;
  color: #666;
  padding-top: 1rem;
}

.related-products-title {
  font-size: 2rem;
  color: #2c3e50;
  margin-bottom: 1.5rem;
  text-align: center;
}

.carousel-container {
  position: relative;
  display: flex;
  align-items: center;
}

.related-products-carousel {
  display: flex;
  overflow-x: auto;
  gap: 1.5rem;
  padding-bottom: 1rem;
  scroll-behavior: smooth;
  -webkit-overflow-scrolling: touch;
}

.related-products-carousel::-webkit-scrollbar {
  height: 8px;
}

.related-products-carousel::-webkit-scrollbar-thumb {
  background-color: #ddd;
  border-radius: 10px;
}

.related-products-carousel::-webkit-scrollbar-track {
  background-color: #f0f0f0;
}

.related-product-card {
  flex: 0 0 auto;
  width: 200px;
  padding: 1rem;
  border: 1px solid #eee;
  border-radius: 10px;
  background-color: #fff;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05);
  text-align: center;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  text-decoration: none;
  color: inherit;
}

.related-product-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1);
}

.related-product-image {
  width: 100%;
  height: 120px;
  object-fit: contain;
  border-radius: 8px;
  margin-bottom: 0.75rem;
}

.related-product-name {
  font-size: 1.1rem;
  color: #2c3e50;
  margin-bottom: 0.5rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  font-weight: 600;
}

.related-product-price {
  font-size: 1.2rem;
  color: #4a90e2;
  font-weight: bold;
  margin-bottom: 0.5rem;
}

.related-product-stock {
  font-size: 0.9rem;
  color: #555;
}

.related-product-stock.out-of-stock {
  color: #e44d26;
  font-weight: bold;
}

.toast {
  position: fixed;
  top: 20px;
  right: 20px;
  background-color: #4caf50;
  color: white;
  padding: 1rem 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  z-index: 1000;
  animation: slideIn 0.3s ease-out, fadeOut 0.3s ease-in 2.7s;
}

@keyframes slideIn {
  from { transform: translateX(100%); opacity: 0; }
  to { transform: translateX(0); opacity: 1; }
}

@keyframes fadeOut {
  from { opacity: 1; }
  to { opacity: 0; }
}

.carousel-arrow {
  background-color: #4a90e2;
  color: white;
  border: none;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  font-size: 1.2rem;
  cursor: pointer;
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  transition: background-color 0.3s ease;
}

.carousel-arrow:hover:not(:disabled) {
  background-color: #357abd;
}

.carousel-arrow:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.carousel-arrow-left {
  left: -50px;
}

.carousel-arrow-right {
  right: -50px;
}

.back-arrow-btn {
  background: none;
  border: none;
  font-size: 1.2rem;
  font-weight: bold;
  color: #333;
  cursor: pointer;
  padding: 0.5rem 1rem;
  margin-bottom: 1.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  transition: color 0.3s;
}

.back-arrow-btn:hover {
  color: #4a90e2;
}


@media (max-width: 768px) {
  .product-details-container {
    padding: 1.5rem;
    margin: 1rem;
  }
  .product-header {
    flex-direction: column;
    align-items: center;
  }
  .product-image {
    max-width: 80%;
  }
  .product-name {
    font-size: 2rem;
    text-align: center;
  }
  .product-price, .product-category {
    text-align: center;
  }
  .add-to-cart-btn {
    align-self: center;
    width: 80%;
  }
  .related-products-title {
    font-size: 1.8rem;
  }
  .carousel-arrow-left {
    left: 10px;
  }
  .carousel-arrow-right {
    right: 10px;
  }
}
</style>
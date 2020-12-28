<template>
  <div>
    <navbar />
    <header class="bg-white shadow">
      <div class="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8">
        <h1 class="text-3xl font-bold leading-tight text-gray-900">
          {{ pageTitle }} - {{ currentDate }}
        </h1>
      </div>
    </header>
    <main>
      <div class="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <slot />
      </div>
    </main>
  </div>
</template>
<script>
import Navbar from './Navbar.vue'
export default {
  props: {
    pageTitle: String,
  },
  data() {
    return {
      currentDate: '',
    }
  },
  created() {
    const today = new Date()
    this.currentDate = `${today.getDate()}.${
      today.getMonth() + 1
    }.${today.getFullYear()}`
  },
  components: {
    Navbar,
  },
}
</script>

<template>
  <main-layout pageTitle="Dashboard">
    <div class="flex flex-col sm:flex-row">
      <time-today class="flex-1"></time-today>
    </div>
  </main-layout>
</template>
<script>
import MainLayout from '../components/MainLayout.vue'
import TimeToday from '../components/TimeEntry/TimeToday.vue'
export default {
  components: {
    MainLayout,
    TimeToday
  }
}
</script>

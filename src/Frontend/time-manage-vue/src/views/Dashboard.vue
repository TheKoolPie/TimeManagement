<template>
  <main-layout pageTitle="Dashboard">
    <div
      class="flex flex-col sm:flex-row bg-gray-50 m-3 p-2 align-bottom rounded-lg shadow-xl"
    >
      <time-today class="flex-1"></time-today>
    </div>
    <div
      class="flex flex-col sm:flex-row bg-gray-50 m-3 p-2 align-bottom rounded-lg shadow-xl"
    >
      <overview-today class="flex-1"></overview-today>
    </div>
  </main-layout>
</template>
<script>
import MainLayout from '../components/MainLayout.vue'
import TimeToday from '../components/TimeEntry/TimeToday.vue'
import OverviewToday from '../components/TimeEntry/OverviewToday.vue'

export default {
  components: {
    MainLayout,
    TimeToday,
    OverviewToday
  },
}
</script>

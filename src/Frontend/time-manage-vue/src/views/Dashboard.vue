<template>
  <main-layout pageTitle="Dashboard">
    <div class="flex flex-wrap justify-between">
      <div class="w-full md:w-4/12 lg:w-6/12 p-1">
        <div class="overflow-hidden rounded-lg shadow-lg bg-gray-50 p-2">
          <time-today
            v-on:time-entry-created="onTimeEntryWasCreated"
          ></time-today>
        </div>
      </div>
      <div class="w-full md:w-4/12 lg:w-6/12 p-1">
        <div class="overflow-hidden  rounded-lg shadow-lg bg-gray-50 p-2">
          <overview-today></overview-today>
        </div>
      </div>
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
  methods: {
    onTimeEntryWasCreated() {
      this.refreshDashboard()
    },
    refreshDashboard(){
      location.reload()
    }
  }
}
</script>

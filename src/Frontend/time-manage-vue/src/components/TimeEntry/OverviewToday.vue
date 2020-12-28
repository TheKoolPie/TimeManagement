<template>
  <div>
    <div
      class="flex items-center justify-between justify-between px-4 py-5 sm:px-6"
    >
      <div>
        <h3 class="text-lg leading-6 font-medium text-gray-900">
          Time of today
        </h3>
        <p class="mt-1 max-w-2xl text-sm text-gray-500">
          Details of current time stats
        </p>
      </div>
      <button
        v-show="comingTime != '' || goingTime != ''"
        class="bg-primary-cyan text-gray-700 hover:bg-primary-cyan-lightest border-transparent p-2 rounded-full focus:outline-none focus:ring-2 focus:ring-offset-2 focus:bg-primary-cyan-darkest focus:ring-white"
      >
        <span class="sr-only">Edit time for today</span>
        <svg
          class="w-6 h-6"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"
          ></path>
        </svg>
      </button>
    </div>
    <div class="border-t border-gray-200">
      <dl>
        <div class="bg-gray-50 px-4 py-5 grid grid-cols-3 gap-4">
          <dt class="font-medium text-gray-500">Coming</dt>
          <dd class="mt-1 text-gray-900">
            {{ comingTime }}
          </dd>
        </div>
        <div class="bg-white px-4 py-5 grid grid-cols-3 gap-4">
          <dt class="font-medium text-gray-500">Going</dt>
          <dd class="mt-1 text-gray-900">
            {{ goingTime }}
          </dd>
        </div>
      </dl>
    </div>
  </div>
</template>
<script>
import { TimeEntryType } from '../../models/enums/TimeEntryType'
import { timeService } from '../../services/time.service'
export default {
  data() {
    return {
      currentDate: '',
      comingTime: '',
      goingTime: '',
    }
  },
  created() {
    this.getEntriesOfToday()
  },
  methods: {
    getEntriesOfToday() {
      timeService.getTimeEntriesOfToday().then((data) => {
        if (data && data.isSuccess) {
          if (data.entries && data.entries.length > 0) {
            const entries = data.entries
            for (var i = 0; i < entries.length; i++) {
              const entry = entries[i]
              const timeString = `${entry.hours}:${entry.minutes}:${entry.seconds}`
              if (entry.entryType === TimeEntryType.COMING) {
                this.comingTime = timeString
              } else if (entry.entryType === TimeEntryType.GOING) {
                this.goingTime = timeString
              }
            }
          }
        }
      })
    },
  },
}
</script>
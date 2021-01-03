<template>
  <div>
    <div v-if="isComingEntryAvailable && isGoingEntryAvailable">
      <h3 class="text-center text-lg leading-6 font-medium text-gray-900">
        Times for today already booked
      </h3>
    </div>
    <div v-else>
      <div class="sm:text-left">
        <h3 class="text-lg leading-6 font-medium text-gray-900">
          Book time for today
        </h3>
        <div class="mt-2">
          <p class="text-sm text-gray-500">
            Book your time for the current day and time. The set time can be
            changed later at any time.
          </p>
        </div>
        <div class="mt-2 text-center text-2xl leading-8">
          {{ currentTime }}
        </div>
      </div>
      <div class="px-4 py-3 sm:px-6 sm:flex sm:flex-row rounded-lg">
        <button
          type="button"
          :disabled="isComingEntryAvailable"
          :class="{
            'hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500': !isComingEntryAvailable
          }"
          class="disabled:opacity-50 sm:mr-2 w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-green-600 text-base font-medium text-white"
          @click="createComingEntry"
        >
          COMING
        </button>
        <button
          type="button"
          :disabled="isGoingEntryAvailable"
          :class="{
            'hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500': !isGoingEntryAvailable
          }"
          class="disabled:opacity-50 mt-3 sm:mt-0 sm:ml-2 w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white"
          @click="createGoingEntry"
        >
          GOING
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { TimeEntryType } from '../../models/enums/TimeEntryType'
import { TimeEntryModel } from '../../models/TimeEntryModel'
import { timeService, dateTimeFormatService } from '../../services'

export default {
  data() {
    return {
      currentTime: '',
      currentTimeStamp: null,
      isComingEntryAvailable: false,
      isGoingEntryAvailable: false
    }
  },
  created() {
    this.nowTimes()
    this.checkComingEntryAvailable()
  },
  mounted() {
    this.nowTimes()
    this.checkComingEntryAvailable()
  },
  methods: {
    nowTimes() {
      this.currentTimeStamp = new Date()
      this.currentTime = dateTimeFormatService.getTimeString(
        this.currentTimeStamp.getHours(),
        this.currentTimeStamp.getMinutes()
      )
      setInterval(this.nowTimes, 30 * 1000)
    },
    createComingEntry() {
      this.createTimeEntry(TimeEntryType.COMING)
    },
    createGoingEntry() {
      this.createTimeEntry(TimeEntryType.GOING)
    },
    createTimeEntry(timeEntryType) {
      const timeModel = new TimeEntryModel(this.currentTimeStamp, timeEntryType)
      timeService.createTimeEntry(timeModel).then(
        data => {
          switch (timeEntryType) {
            case TimeEntryType.COMING:
              this.isComingEntryAvailable = true
              break
            case TimeEntryType.GOING:
              this.isGoingEntryAvailable = true
              break
          }
          this.$emit('time-entry-created')
        },
        error => {
          console.log(error)
        }
      )
    },
    checkComingEntryAvailable() {
      const entriesOfThisDay = timeService.getTimeEntriesOfDate(
        this.currentTimeStamp.getMonth() + 1,
        this.currentTimeStamp.getDate(),
        this.currentTimeStamp.getFullYear()
      )
      entriesOfThisDay.then(e => {
        if (e.entries && e.entries.length > 0) {
          for (var i = 0; i < e.entries.length; i++) {
            const currentItem = e.entries[i]
            if (currentItem.entryType === TimeEntryType.COMING) {
              this.isComingEntryAvailable = true
            }
            if (currentItem.entryType === TimeEntryType.GOING) {
              this.isGoingEntryAvailable = true
            }
          }
        }
      })
    }
  }
}
</script>

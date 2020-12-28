import { authService } from './auth.service'

export const timeService = {
    getTimeEntries,
    getTimeEntriesOfMonth,
    getTimeEntriesOfMonthAndDay,
    getTimeEntriesOfDate,
    createTimeEntry,
    getTimeEntriesOfToday
}

function getTimeEntries() {
    const o = {
        method: 'GET',
        headers: authService.getAuthHeader()
    }
    return fetch(`${process.env.VUE_APP_ROOT_API}/api/timeEntry`, o)
        .then(authService.handleApiResponse)
}
function getTimeEntriesOfMonth(month) {
    const o = {
        method: 'GET',
        headers: authService.getAuthHeader()
    }
    return fetch(`${process.env.VUE_APP_ROOT_API}/api/timeEntry/${month}`, o)
        .then(authService.handleApiResponse)
}
function getTimeEntriesOfMonthAndDay(month, day) {
    const o = {
        method: 'GET',
        headers: authService.getAuthHeader()
    }
    return fetch(`${process.env.VUE_APP_ROOT_API}/api/timeEntry/${month}/${day}`, o)
        .then(authService.handleApiResponse)
}
function getTimeEntriesOfDate(month, day, year) {
    const o = {
        method: 'GET',
        headers: authService.getAuthHeader()
    }
    return fetch(`${process.env.VUE_APP_ROOT_API}/api/timeEntry/${month}/${day}/${year}`, o)
        .then(authService.handleApiResponse)
}
function getTimeEntriesOfToday() {
    const today = new Date()
    return getTimeEntriesOfDate(today.getMonth() + 1, today.getDate(), today.getFullYear())
}

function createTimeEntry(timeEntryModel) {
    const requestOptions = {
        method: 'POST',
        headers: authService.getAuthHeader(),
        body: JSON.stringify(timeEntryModel)
    }
    return fetch(`${process.env.VUE_APP_ROOT_API}/api/timeEntry`, requestOptions)
        .then(authService.handleApiResponse)
}


export const dateTimeFormatService = {
    getTimeString
}

function getTimeString(hours, minutes, seconds) {
    let timeString = `${getLeadingZero(hours)}:${getLeadingZero(minutes)}`

    if (seconds) {
        timeString += `:${getLeadingZero(seconds)}`
    }

    return timeString
}

function getLeadingZero(value) {
    return value < 10 ? '0' + value : value
}
export class TimeSpan {
    hours = 0;
    minutes = 0;
    seconds = 0;

    constructor() {
        let currentDate = new Date();
        this.hours = currentDate.getHours();
        this.minutes = currentDate.getMinutes();
        this.seconds = currentDate.getSeconds();
    }
    constructor(currentDate){
        this.hours = currentDate.getHours();
        this.minutes = currentDate.getMinutes();
        this.seconds = currentDate.getSeconds();
    }
    constructor(hours, minutes, seconds) {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }
}
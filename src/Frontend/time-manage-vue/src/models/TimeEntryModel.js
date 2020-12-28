export class TimeEntryModel {
    date;
    hours;
    minutes;
    seconds;
    entryType;

    constructor(timeStamp, entryType){
        this.date = timeStamp;
        this.hours = this.date.getHours();
        this.minutes = this.date.getMinutes();
        this.seconds = this.date.getSeconds();
        this.entryType = entryType;
    }
}
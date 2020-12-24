import { TimeSpan } from "./TimeSpan";

export class TimeEntryModel {
    date;
    time;
    entryType;

    constructor(entryType) {
        this.date = new Date();
        this.time = new TimeSpan(this.date);
        this.entryType = entryType;
    }
    constructor(year, month, date, hours, minutes, seconds, entryType) {
        this.date = new Date(year, month, date);
        this.time = new TimeSpan(hours, minutes, seconds);
        this.entryType = entryType;
    }
}
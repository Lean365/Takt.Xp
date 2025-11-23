declare module 'lunar-javascript' {
  export default class Lunar {
    static fromDate(date: Date): Lunar
    static fromLunar(year: number, month: number, day: number): Lunar
    getSolar(): {
      toDate(): Date
    }
  }
}
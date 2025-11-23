/*
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 工具类（基于 OpenAuth.Net CreatedFlow）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
 */
import { flowConfig } from '../config/args-config'

// ID 类型
type IdType = string | [string, number] | [string, () => string]

// ZFSN 工具类接口
export interface ZFSNInterface {
  seqNo: number
  consoleLog: (strArr: string[]) => void
  getId: () => string | undefined
  getUUID: () => string
  getTimeStamp: () => number
  getSequence: (seqNoLength: number) => string
  getTimeStampAndSequence: (seqNoLength: number) => string
  add: (a: number, b: number) => number
  sub: (a: number, b: number) => number
  mul: (a: number, b: number) => number
  div: (a: number, b: number) => number
}

// ZFSN 工具类
export const ZFSN: ZFSNInterface = {
  seqNo: 1,
  consoleLog: function (strArr: string[]): void {
    let log = ''
    for (let i = 0, len = strArr.length; i < len; i++) {
      log += strArr[i] + '\n'
    }
    console.log('%c' + log, 'color: red; font-weight: bold;')
  },
  getId: function (): string | undefined {
    const idType = flowConfig.idType
    if (typeof idType === 'string') {
      if (idType === 'uuid') {
        return this.getUUID()
      } else if (idType === 'time_stamp') {
        return this.getTimeStamp().toString()
      }
    } else if (Array.isArray(idType)) {
      if (idType[0] === 'time_stamp_and_sequence') {
        return this.getSequence(idType[1] as number)
      } else if (idType[0] === 'time_stamp_and_sequence') {
        return this.getTimeStampAndSequence(idType[1] as number).toString()
      } else if (idType[0] === 'custom') {
        const customFn = idType[1] as () => string
        return customFn()
      }
    }
    return undefined
  },
  getUUID: function (): string {
    const s: string[] = []
    const hexDigits = '0123456789abcdef'
    for (let i = 0; i < 36; i++) {
      s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1)
    }
    s[14] = '4'
    // 设置版本号位（第19位）
    const char19 = parseInt(s[19] || '0', 16)
    s[19] = hexDigits.substr((char19 & 0x3) | 0x8, 1)
    s[8] = s[13] = s[18] = s[23] = '-'

    const uuid = s.join('')
    return uuid.replace(/-/g, '')
  },
  getTimeStamp: function (): number {
    return new Date().getTime()
  },
  getSequence: function (seqNoLength: number): string {
    const zeroStr = new Array(seqNoLength).fill('0').join('')
    return (zeroStr + (this.seqNo++)).slice(-seqNoLength)
  },
  getTimeStampAndSequence: function (seqNoLength: number): string {
    return this.getTimeStamp() + this.getSequence(seqNoLength)
  },
  add: function (a: number, b: number): number {
    let c: number, d: number
    try {
      c = a.toString().split('.')[1].length
    } catch (f) {
      c = 0
    }
    try {
      d = b.toString().split('.')[1].length
    } catch (f) {
      d = 0
    }
    const e = Math.pow(10, Math.max(c, d))
    return (this.mul(a, e) + this.mul(b, e)) / e
  },
  sub: function (a: number, b: number): number {
    let c: number, d: number
    try {
      c = a.toString().split('.')[1].length
    } catch (f) {
      c = 0
    }
    try {
      d = b.toString().split('.')[1].length
    } catch (f) {
      d = 0
    }
    const e = Math.pow(10, Math.max(c, d))
    return (this.mul(a, e) - this.mul(b, e)) / e
  },
  mul: function (a: number, b: number): number {
    let c = 0
    const d = a.toString()
    const e = b.toString()
    try {
      c += d.split('.')[1].length
    } catch (f) {
      // console.log(f)
    }
    try {
      c += e.split('.')[1].length
    } catch (f) {
      // console.log(f)
    }
    return Number(d.replace('.', '')) * Number(e.replace('.', '')) / Math.pow(10, c)
  },
  div: function (a: number, b: number): number {
    let e = 0
    let f = 0
    try {
      e = a.toString().split('.')[1].length
    } catch (g) {
      console.log(f)
    }
    try {
      f = b.toString().split('.')[1].length
    } catch (g) {
      console.log(f)
    }
    const c = Number(a.toString().replace('.', ''))
    const d = Number(b.toString().replace('.', ''))
    return this.mul(c / d, Math.pow(10, f - e))
  }
}


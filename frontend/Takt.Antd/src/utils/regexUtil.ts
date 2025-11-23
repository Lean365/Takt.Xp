import { isValidPhoneNumber, parsePhoneNumber, CountryCode } from 'libphonenumber-js'

/**
 * 正则表达式工具类
 */
export class RegexUtils {
  /**
   * 手机号码正则表达式
   * 只允许+开头的国际号码（8-15位）或中国大陆手机号（1开头11位）
   */
  static readonly PHONE = /^(\+[1-9]\d{7,14}|1[3-9]\d{9})$/

  /**
   * 电话号码正则表达式（包含座机）
   * 支持以下格式：
   * 1. 座机号码：区号-号码，如：010-12345678
   * 2. 手机号码：1开头的11位数字
   * 3. 400/800号码：400/800开头的10位数字
   */
  static readonly TELEPHONE = /^((0\d{2,3}-?)?\d{7,8}|1[3-9]\d{9}|[48]00\d{7})$/

  /**
   * 邮箱正则表达式
   * 长度在6-100位之间
   * 支持字母、数字、点、下划线、连字符
   * 域名部分至少2位
   */
  static readonly EMAIL = /^[a-zA-Z0-9._%+-]{1,64}@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/

  /**
   * 用户名正则表达式
   * 必须以小写字母开头
   * 长度在4-50位之间
   * 只能包含小写字母、数字和连字符，不允许点和下划线
   */
  static readonly USERNAME = /^[a-z][a-z0-9-]{3,49}$/

  /**
   * 昵称正则表达式
   * 2-50位字符，允许中文、日语、韩语、英文、数字、英文句点和连字符，不允许下划线和空格
   */
  static readonly NICKNAME = /^[\u4e00-\u9fa5\u3040-\u30ff\uac00-\ud7afA-Za-z0-9.-]{2,50}$/
  /**
   * 姓名（全名）正则表达式
   * 2-100位字符，允许中文、日语、韩语、英文、数字、英文句点和连字符，不允许下划线和空格
   */
  static readonly FULLNAME = /^[\u4e00-\u9fa5\u3040-\u30ff\uac00-\ud7afA-Za-z0-9.-]{2,100}$/

  /**
   * 姓名（真实姓名）正则表达式
   * 2-100位字符，允许中文、日语、韩语、英文、数字、英文句点和连字符，不允许下划线和空格
   */
  static readonly REALNAME = /^[\u4e00-\u9fa5\u3040-\u30ff\uac00-\ud7afA-Za-z0-9.-]{2,100}$/
  /**
   * 英文名称正则表达式
   * 2-100位字符，必须字母开头，允许字母、连字符、英文句点和空格
   */
  static readonly ENGLISH_NAME = /^[A-Za-z][A-Za-z\s\-.]{1,99}$/

  /**
   * 密码正则表达式
   * 6-20位，必须字母开头，包含大写、小写、数字、特殊字符，不允许连续重复字符
   */
  static readonly PASSWORD = /^(?!.*([A-Za-z0-9@$!%*?&])\1)[A-Za-z](?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,19}$/

  /**
   * 身份证号码正则表达式
   * 支持15位和18位
   */
  static readonly ID_CARD = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/

  /**
   * URL正则表达式
   */
  static readonly URL = /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*\/?$/

  /**
   * IP地址正则表达式
   */
  static readonly IP = /^(\d{1,3}\.){3}\d{1,3}$/

  /**
   * 邮政编码正则表达式
   */
  static readonly POSTAL_CODE = /^[1-9]\d{5}$/

  /**
   * 使用 libphonenumber-js 校验手机号（支持全球）
   * @param phone 手机号
   * @param country 国家码（如 'CN', 'US'，可选，建议传递）
   */
  static isValidPhone(phone: string, country: CountryCode = 'CN'): boolean {
    try {
      return isValidPhoneNumber(phone, country)
    } catch {
      return false
    }
  }

  /**
   * 使用 libphonenumber-js 校验座机/固话（支持全球）
   * @param phone 电话号码
   * @param country 国家码（如 'CN', 'US'，可选，建议传递）
   */
  static isValidTelephone(phone: string, country: CountryCode = 'CN'): boolean {
    try {
      return isValidPhoneNumber(phone, country)
    } catch {
      return false
    }
  }

  /**
   * 获取电话号码归属国家
   */
  static getTelephoneCountry(phone: string): string | undefined {
    try {
      const pn = parsePhoneNumber(phone)
      return pn.country
    } catch {
      return undefined
    }
  }

  /**
   * 验证邮箱
   * @param email 邮箱地址
   * @returns boolean
   */
  static isValidEmail(email: string): boolean {
    return this.EMAIL.test(email)
  }

  /**
   * 验证用户名
   * @param username 用户名
   * @returns boolean
   */
  static isValidUsername(username: string): boolean {
    return this.USERNAME.test(username)
  }

  /**
   * 验证密码
   * @param password 密码
   * @returns boolean
   */
  static isValidPassword(password: string): boolean {
    return this.PASSWORD.test(password)
  }

  /**
   * 验证身份证号码
   * @param idCard 身份证号码
   * @returns boolean
   */
  static isValidIdCard(idCard: string): boolean {
    return this.ID_CARD.test(idCard)
  }

  /**
   * 验证URL
   * @param url URL地址
   * @returns boolean
   */
  static isValidUrl(url: string): boolean {
    return this.URL.test(url)
  }

  /**
   * 验证IP地址
   * @param ip IP地址
   * @returns boolean
   */
  static isValidIp(ip: string): boolean {
    return this.IP.test(ip)
  }

  /**
   * 验证邮政编码
   * @param code 邮政编码
   * @returns boolean
   */
  static isValidPostalCode(code: string): boolean {
    return this.POSTAL_CODE.test(code)
  }

  /**
   * 验证昵称
   * @param nickname 昵称
   * @returns boolean
   */
  static isValidNickname(nickname: string): boolean {
    return this.NICKNAME.test(nickname)
  }

  /**
   * 验证英文名称
   * @param name 英文名称
   * @returns boolean
   */
  static isValidEnglishName(name: string): boolean {
    return this.ENGLISH_NAME.test(name)
  }

  /**
   * 提取字符串中的数字
   * @param str 字符串
   * @returns string[]
   */
  static extractNumbers(str: string): string[] {
    return str.match(/\d+/g) || []
  }

  /**
   * 提取字符串中的中文
   * @param str 字符串
   * @returns string[]
   */
  static extractChinese(str: string): string[] {
    return str.match(/[\u4e00-\u9fa5]+/g) || []
  }

  /**
   * 提取字符串中的英文
   * @param str 字符串
   * @returns string[]
   */
  static extractEnglish(str: string): string[] {
    return str.match(/[a-zA-Z]+/g) || []
  }

  /**
   * 提取字符串中的特殊字符
   * @param str 字符串
   * @returns string[]
   */
  static extractSpecialChars(str: string): string[] {
    return str.match(/[^a-zA-Z0-9\u4e00-\u9fa5\s]/g) || []
  }

  /**
   * 移除字符串中的HTML标签
   * @param str 字符串
   * @returns string
   */
  static removeHtmlTags(str: string): string {
    return str.replace(/<[^>]+>/g, '')
  }

  /**
   * 移除字符串中的空格
   * @param str 字符串
   * @returns string
   */
  static removeSpaces(str: string): string {
    return str.replace(/\s+/g, '')
  }

  /**
   * 格式化手机号码
   * @param phone 手机号码
   * @returns string
   */
  static formatPhone(phone: string): string {
    return phone.replace(/(\d{3})(\d{4})(\d{4})/, '$1-$2-$3')
  }

  /**
   * 格式化身份证号码
   * @param idCard 身份证号码
   * @returns string
   */
  static formatIdCard(idCard: string): string {
    return idCard.replace(/(\d{6})(\d{4})(\d{4})(\d{4})/, '$1-$2-$3-$4')
  }

  /**
   * 格式化银行卡号
   * @param cardNo 银行卡号
   * @returns string
   */
  static formatBankCard(cardNo: string): string {
    return cardNo.replace(/(\d{4})(?=\d)/g, '$1-')
  }

  /**
   * 校验全名
   */
  static isValidFullName(fullName: string): boolean {
    return this.FULLNAME.test(fullName)
  }

  /**
   * 校验真实姓名
   */
  static isValidRealName(realName: string): boolean {
    return this.REALNAME.test(realName)
  }
} 
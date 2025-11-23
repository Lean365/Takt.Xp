/**
 * 数据脱敏工具
 * 用于处理敏感数据的显示
 */

/**
 * 手机号脱敏
 * @param phone 手机号
 * @returns 脱敏后的手机号，如：138****8888
 */
export const maskPhone = (phone: string): string => {
  if (!phone) return '';
  return phone.replace(/(\d{3})\d{4}(\d{4})/, '$1****$2');
};

/**
 * 身份证号脱敏
 * @param idCard 身份证号
 * @returns 脱敏后的身份证号，如：110101********1234
 */
export const maskIdCard = (idCard: string): string => {
  if (!idCard) return '';
  return idCard.replace(/(\d{6})\d{8}(\d{4})/, '$1********$2');
};

/**
 * 银行卡号脱敏
 * @param bankCard 银行卡号
 * @returns 脱敏后的银行卡号，如：6222 **** **** 1234
 */
export const maskBankCard = (bankCard: string): string => {
  if (!bankCard) return '';
  return bankCard.replace(/(\d{4})\d+(\d{4})/, '$1 **** **** $2');
};

/**
 * 邮箱脱敏
 * @param email 邮箱
 * @returns 脱敏后的邮箱，如：t***@example.com
 */
export const maskEmail = (email: string): string => {
  if (!email) return '';
  const [name, domain] = email.split('@');
  if (!domain) return email;
  const maskedName = name.charAt(0) + '*'.repeat(name.length - 1);
  return `${maskedName}@${domain}`;
};

/**
 * 姓名脱敏
 * @param name 姓名
 * @returns 脱敏后的姓名，如：张**
 */
export const maskName = (name: string): string => {
  if (!name) return '';
  if (name.length <= 1) return name;
  return name.charAt(0) + '*'.repeat(name.length - 1);
};

/**
 * 地址脱敏
 * @param address 地址
 * @returns 脱敏后的地址，如：北京市海淀区****
 */
export const maskAddress = (address: string): string => {
  if (!address) return '';
  if (address.length <= 4) return address;
  return address.substring(0, 4) + '*'.repeat(address.length - 4);
};

/**
 * 自定义脱敏
 * @param str 原始字符串
 * @param start 开始保留的字符数
 * @param end 结束保留的字符数
 * @param mask 脱敏字符，默认为 *
 * @returns 脱敏后的字符串
 */
export const maskCustom = (str: string, start: number, end: number, mask: string = '*'): string => {
  if (!str) return '';
  if (str.length <= start + end) return str;
  const prefix = str.substring(0, start);
  const suffix = str.substring(str.length - end);
  const maskedLength = str.length - start - end;
  return prefix + mask.repeat(maskedLength) + suffix;
};

/**
 * 密码脱敏
 * @param password 密码
 * @returns 脱敏后的密码，如：********
 */
export const maskPassword = (password: string): string => {
  if (!password) return '';
  return '*'.repeat(password.length);
};

/**
 * 连接ID脱敏
 * @param connectionId 连接ID
 * @returns 脱敏后的连接ID，如：abc123****def456
 */
export const maskConnectionId = (connectionId: string): string => {
  if (!connectionId) return '';
  if (connectionId.length <= 8) return connectionId;
  return maskCustom(connectionId, 6, 6);
};

/**
 * 通用脱敏方法
 * @param value 需要脱敏的值
 * @param type 脱敏类型
 * @returns 脱敏后的值
 */
export const mask = (value: string, type: 'phone' | 'idCard' | 'bankCard' | 'email' | 'name' | 'address' | 'password' | 'connectionId'): string => {
  if (!value) return '';
  
  switch (type) {
    case 'phone':
      return maskPhone(value);
    case 'idCard':
      return maskIdCard(value);
    case 'bankCard':
      return maskBankCard(value);
    case 'email':
      return maskEmail(value);
    case 'name':
      return maskName(value);
    case 'address':
      return maskAddress(value);
    case 'password':
      return maskPassword(value);
    case 'connectionId':
      return maskConnectionId(value);
    default:
      return value;
  }
};

/**
 * 对象属性脱敏
 * @param obj 需要脱敏的对象
 * @param rules 脱敏规则，key为属性名，value为脱敏类型
 * @returns 脱敏后的对象
 */
export const maskObject = <T extends { [key: string]: string }>(
  obj: T,
  rules: Partial<Record<keyof T, 'phone' | 'idCard' | 'bankCard' | 'email' | 'name' | 'address' | 'password'>>
): T => {
  if (!obj) return obj;
  
  const result = { ...obj };
  Object.entries(rules).forEach(([key, type]) => {
    if (result[key] && type) {
      (result as any)[key] = mask(result[key], type);
    }
  });
  
  return result;
};

/**
 * 数组对象脱敏
 * @param arr 需要脱敏的数组
 * @param rules 脱敏规则，key为属性名，value为脱敏类型
 * @returns 脱敏后的数组
 */
export const maskArray = <T extends { [key: string]: string }>(
  arr: T[],
  rules: Partial<Record<keyof T, 'phone' | 'idCard' | 'bankCard' | 'email' | 'name' | 'address' | 'password'>>
): T[] => {
  if (!Array.isArray(arr)) return arr;
  return arr.map(item => maskObject(item, rules));
}; 
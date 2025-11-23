/**
 * 物料相关正则表达式工具类
 * 基于国际标准产业分类(ISIC) Rev.4
 */
export class MaterialRegexUtils {
  /**
   * 通用物料编码正则表达式
   * 格式：公司代码4位+产地代码3位+ISIC分类+产品类型-序号4位+版本号2位
   * 示例：COMP001CNAC10-RAW-0001-01
   * 说明：
   * - 公司代码：4位字母数字组合
   * - 产地代码：3位字母数字组合
   * - ISIC分类：2位字母+2位数字
   * - 产品类型：2-4位字母
   * - 序号：4位数字
   * - 版本号：2位数字
   */
  static readonly MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}[A-Z]\d{2}-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 通用物料名称正则表达式
   * 支持中文、英文、数字、下划线、连字符
   * 长度在2-100位之间
   */
  static readonly MATERIAL_NAME = /^[\u4e00-\u9fa5a-zA-Z0-9_\-]{2,100}$/

  /**
   * 通用物料规格正则表达式
   * 支持中文、英文、数字、下划线、连字符、空格
   * 长度在2-200位之间
   */
  static readonly MATERIAL_SPEC = /^[\u4e00-\u9fa5a-zA-Z0-9_\- ]{2,200}$/

  /**
   * 通用物料单位正则表达式
   * 支持中文、英文、数字
   * 长度在1-10位之间
   */
  static readonly MATERIAL_UNIT = /^[\u4e00-\u9fa5a-zA-Z0-9]{1,10}$/

  /**
   * 农业、林业和渔业物料编码
   * ISIC A: 01-03
   * 示例：COMP001CNAA01-SEE-0001-01、COMP001CNAA02-TRE-0001-01、COMP001CNAA03-FIS-0001-01
   */
  static readonly AGRICULTURE_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}A(01|02|03)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 采矿和采石物料编码
   * ISIC B: 05-09
   * 示例：COMP001CNAB05-COA-0001-01、COMP001CNAB07-MET-0001-01、COMP001CNAB08-QUA-0001-01
   */
  static readonly MINING_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}B(05|06|07|08|09)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 制造业物料编码
   * ISIC C: 10-33
   * 示例：COMP001CNAC10-FOO-0001-01、COMP001CNAC13-TEX-0001-01、COMP001CNAC20-CHE-0001-01
   */
  static readonly MANUFACTURING_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}C(1[0-9]|2[0-9]|3[0-3])-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 电力、燃气、蒸汽和空调供应物料编码
   * ISIC D: 35
   * 示例：COMP001CNAD35-POW-0001-01、COMP001CNAD35-GAS-0001-01
   */
  static readonly UTILITY_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}D35-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 供水、污水处理、废物管理和补救活动物料编码
   * ISIC E: 36-39
   * 示例：COMP001CNAE36-WAT-0001-01、COMP001CNAE37-WAS-0001-01
   */
  static readonly WATER_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}E(36|37|38|39)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 建筑业物料编码
   * ISIC F: 41-43
   * 示例：COMP001CNAF41-BUI-0001-01、COMP001CNAF42-CIV-0001-01、COMP001CNAF43-SPE-0001-01
   */
  static readonly CONSTRUCTION_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}F(41|42|43)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 批发和零售贸易物料编码
   * ISIC G: 45-47
   * 示例：COMP001CNAG45-WHO-0001-01、COMP001CNAG46-RET-0001-01、COMP001CNAG47-ONL-0001-01
   */
  static readonly TRADE_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}G(45|46|47)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 运输和储存物料编码
   * ISIC H: 49-53
   * 示例：COMP001CNAH49-TRA-0001-01、COMP001CNAH50-SHI-0001-01、COMP001CNAH51-AIR-0001-01
   */
  static readonly TRANSPORT_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}H(49|50|51|52|53)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 住宿和食品服务活动物料编码
   * ISIC I: 55-56
   * 示例：COMP001CNAI55-HOT-0001-01、COMP001CNAI56-FOO-0001-01
   */
  static readonly HOSPITALITY_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}I(55|56)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 信息和通信物料编码
   * ISIC J: 58-63
   * 示例：COMP001CNAJ58-PUB-0001-01、COMP001CNAJ59-FIL-0001-01、COMP001CNAJ61-TEL-0001-01
   */
  static readonly ICT_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}J(58|59|60|61|62|63)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 金融和保险活动物料编码
   * ISIC K: 64-66
   * 示例：COMP001CNAK64-FIN-0001-01、COMP001CNAK65-INS-0001-01、COMP001CNAK66-FUN-0001-01
   */
  static readonly FINANCE_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}K(64|65|66)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 房地产活动物料编码
   * ISIC L: 68
   * 示例：COMP001CNAL68-REA-0001-01
   */
  static readonly REAL_ESTATE_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}L68-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 专业、科学和技术活动物料编码
   * ISIC M: 69-75
   * 示例：COMP001CNAM69-LEG-0001-01、COMP001CNAM70-ACC-0001-01、COMP001CNAM71-ARC-0001-01
   */
  static readonly PROFESSIONAL_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}M(69|70|71|72|73|74|75)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 行政和支助服务活动物料编码
   * ISIC N: 77-82
   * 示例：COMP001CNAN77-ADM-0001-01、COMP001CNAN78-STA-0001-01、COMP001CNAN79-TRA-0001-01
   */
  static readonly ADMIN_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}N(77|78|79|80|81|82)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 公共管理和国防物料编码
   * ISIC O: 84
   * 示例：COMP001CNAO84-GOV-0001-01
   */
  static readonly PUBLIC_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}O84-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 教育物料编码
   * ISIC P: 85
   * 示例：COMP001CNAP85-EDU-0001-01
   */
  static readonly EDUCATION_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}P85-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 人体健康和社会工作活动物料编码
   * ISIC Q: 86-88
   * 示例：COMP001CNAQ86-HOS-0001-01、COMP001CNAQ87-CAR-0001-01、COMP001CNAQ88-SOC-0001-01
   */
  static readonly HEALTH_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}Q(86|87|88)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 艺术、娱乐和文娱活动物料编码
   * ISIC R: 90-93
   * 示例：COMP001CNAR90-ART-0001-01、COMP001CNAR91-ENT-0001-01、COMP001CNAR92-GAM-0001-01
   */
  static readonly ARTS_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}R(90|91|92|93)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 其他服务活动物料编码
   * ISIC S: 94-96
   * 示例：COMP001CNAS94-ORG-0001-01、COMP001CNAS95-REP-0001-01、COMP001CNAS96-PER-0001-01
   */
  static readonly OTHER_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}S(94|95|96)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 家庭作为雇主的活动物料编码
   * ISIC T: 97-98
   * 示例：COMP001CNAT97-HOM-0001-01、COMP001CNAT98-UND-0001-01
   */
  static readonly HOUSEHOLD_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}T(97|98)-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 国际组织和机构的活动物料编码
   * ISIC U: 99
   * 示例：COMP001CNAU99-INT-0001-01
   */
  static readonly INTERNATIONAL_MATERIAL_CODE = /^[A-Z0-9]{4}[A-Z0-9]{3}U99-[A-Z]{2,4}-\d{4}-\d{2}$/

  /**
   * 验证通用物料编码
   * @param code 物料编码
   * @returns boolean
   */
  static isValidMaterialCode(code: string): boolean {
    return this.MATERIAL_CODE.test(code)
  }

  /**
   * 验证通用物料名称
   * @param name 物料名称
   * @returns boolean
   */
  static isValidMaterialName(name: string): boolean {
    return this.MATERIAL_NAME.test(name)
  }

  /**
   * 验证通用物料规格
   * @param spec 物料规格
   * @returns boolean
   */
  static isValidMaterialSpec(spec: string): boolean {
    return this.MATERIAL_SPEC.test(spec)
  }

  /**
   * 验证通用物料单位
   * @param unit 物料单位
   * @returns boolean
   */
  static isValidMaterialUnit(unit: string): boolean {
    return this.MATERIAL_UNIT.test(unit)
  }

  /**
   * 根据ISIC代码验证物料编码
   * @param code 物料编码
   * @param isicCode ISIC代码
   * @returns boolean
   */
  static isValidMaterialCodeByISIC(code: string, isicCode: string): boolean {
    switch (isicCode) {
      case 'A01':
      case 'A02':
      case 'A03':
        return this.AGRICULTURE_MATERIAL_CODE.test(code)
      case 'B05':
      case 'B06':
      case 'B07':
      case 'B08':
      case 'B09':
        return this.MINING_MATERIAL_CODE.test(code)
      case 'C10':
      case 'C11':
      case 'C12':
      case 'C13':
      case 'C14':
      case 'C15':
      case 'C16':
      case 'C17':
      case 'C18':
      case 'C19':
      case 'C20':
      case 'C21':
      case 'C22':
      case 'C23':
      case 'C24':
      case 'C25':
      case 'C26':
      case 'C27':
      case 'C28':
      case 'C29':
      case 'C30':
      case 'C31':
      case 'C32':
      case 'C33':
        return this.MANUFACTURING_MATERIAL_CODE.test(code)
      case 'D35':
        return this.UTILITY_MATERIAL_CODE.test(code)
      case 'E36':
      case 'E37':
      case 'E38':
      case 'E39':
        return this.WATER_MATERIAL_CODE.test(code)
      case 'F41':
      case 'F42':
      case 'F43':
        return this.CONSTRUCTION_MATERIAL_CODE.test(code)
      case 'G45':
      case 'G46':
      case 'G47':
        return this.TRADE_MATERIAL_CODE.test(code)
      case 'H49':
      case 'H50':
      case 'H51':
      case 'H52':
      case 'H53':
        return this.TRANSPORT_MATERIAL_CODE.test(code)
      case 'I55':
      case 'I56':
        return this.HOSPITALITY_MATERIAL_CODE.test(code)
      case 'J58':
      case 'J59':
      case 'J60':
      case 'J61':
      case 'J62':
      case 'J63':
        return this.ICT_MATERIAL_CODE.test(code)
      case 'K64':
      case 'K65':
      case 'K66':
        return this.FINANCE_MATERIAL_CODE.test(code)
      case 'L68':
        return this.REAL_ESTATE_MATERIAL_CODE.test(code)
      case 'M69':
      case 'M70':
      case 'M71':
      case 'M72':
      case 'M73':
      case 'M74':
      case 'M75':
        return this.PROFESSIONAL_MATERIAL_CODE.test(code)
      case 'N77':
      case 'N78':
      case 'N79':
      case 'N80':
      case 'N81':
      case 'N82':
        return this.ADMIN_MATERIAL_CODE.test(code)
      case 'O84':
        return this.PUBLIC_MATERIAL_CODE.test(code)
      case 'P85':
        return this.EDUCATION_MATERIAL_CODE.test(code)
      case 'Q86':
      case 'Q87':
      case 'Q88':
        return this.HEALTH_MATERIAL_CODE.test(code)
      case 'R90':
      case 'R91':
      case 'R92':
      case 'R93':
        return this.ARTS_MATERIAL_CODE.test(code)
      case 'S94':
      case 'S95':
      case 'S96':
        return this.OTHER_MATERIAL_CODE.test(code)
      case 'T97':
      case 'T98':
        return this.HOUSEHOLD_MATERIAL_CODE.test(code)
      case 'U99':
        return this.INTERNATIONAL_MATERIAL_CODE.test(code)
      default:
        return false
    }
  }

  /**
   * 获取ISIC代码对应的行业名称
   * @param isicCode ISIC代码
   * @returns string
   */
  static getISICIndustryName(isicCode: string): string {
    const industryMap: Record<string, string> = {
      'A01': '作物和动物生产、狩猎和相关服务活动',
      'A02': '林业和伐木活动',
      'A03': '渔业和水产养殖',
      'B05': '煤炭和褐煤开采',
      'B06': '原油和天然气开采',
      'B07': '金属矿开采',
      'B08': '其他采矿和采石',
      'B09': '采矿支持服务活动',
      'C10': '食品制造',
      'C11': '饮料制造',
      'C12': '烟草制品制造',
      'C13': '纺织品制造',
      'C14': '服装制造',
      'C15': '皮革和相关制品制造',
      'C16': '木材、木材制品和软木制品制造',
      'C17': '纸和纸制品制造',
      'C18': '印刷和记录媒介复制',
      'C19': '焦炭和精炼石油产品制造',
      'C20': '化学品和化学产品制造',
      'C21': '基本医药产品和医药制剂制造',
      'C22': '橡胶和塑料制品制造',
      'C23': '其他非金属矿物制品制造',
      'C24': '基本金属制造',
      'C25': '金属制品制造',
      'C26': '计算机、电子和光学产品制造',
      'C27': '电气设备制造',
      'C28': '机械设备制造',
      'C29': '汽车、拖车和半拖车制造',
      'C30': '其他运输设备制造',
      'C31': '家具制造',
      'C32': '其他制造业',
      'C33': '修理和安装机械和设备',
      'D35': '电力、燃气、蒸汽和空调供应',
      'E36': '水的收集、处理和供应',
      'E37': '污水处理',
      'E38': '废物收集、处理和处置活动；材料回收',
      'E39': '补救活动和其他废物管理服务',
      'F41': '建筑物建造',
      'F42': '土木工程',
      'F43': '专业建筑活动',
      'G45': '批发和零售贸易以及汽车和摩托车修理',
      'G46': '批发贸易，汽车和摩托车除外',
      'G47': '零售贸易，汽车和摩托车除外',
      'H49': '陆路运输和管道运输',
      'H50': '水上运输',
      'H51': '航空运输',
      'H52': '仓储和运输辅助活动',
      'H53': '邮政和快递活动',
      'I55': '住宿',
      'I56': '食品和饮料服务活动',
      'J58': '出版活动',
      'J59': '电影、视频和电视节目制作、录音和音乐出版活动',
      'J60': '节目和广播活动',
      'J61': '电信',
      'J62': '计算机编程、咨询和相关活动',
      'J63': '信息服务活动',
      'K64': '金融服务活动，保险和养恤基金除外',
      'K65': '保险、再保险和养恤基金活动',
      'K66': '金融保险活动辅助服务',
      'L68': '房地产活动',
      'M69': '法律和会计活动',
      'M70': '管理咨询活动',
      'M71': '建筑和工程活动；技术测试和分析',
      'M72': '科学研究和发展',
      'M73': '广告和市场研究',
      'M74': '其他专业、科学和技术活动',
      'M75': '兽医活动',
      'N77': '租赁活动',
      'N78': '就业活动',
      'N79': '旅行社、旅游经营者、预订服务及相关活动',
      'N80': '安全和调查活动',
      'N81': '建筑物和景观活动服务',
      'N82': '办公室行政、办公室支持和其他商业支持活动',
      'O84': '公共管理和国防；强制性社会保障',
      'P85': '教育',
      'Q86': '人体健康活动',
      'Q87': '居民护理活动',
      'Q88': '社会工作活动',
      'R90': '创意、艺术和娱乐活动',
      'R91': '图书馆、档案馆、博物馆和其他文化活动',
      'R92': '赌博和投注活动',
      'R93': '体育活动和娱乐及休闲活动',
      'S94': '会员组织活动',
      'S95': '计算机、个人和家庭用品修理',
      'S96': '其他个人服务活动',
      'T97': '家庭作为雇主的活动',
      'T98': '家庭自用未分类商品和服务的生产活动',
      'U99': '国际组织和机构的活动'
    }
    return industryMap[isicCode] || '未知行业'
  }
} 
//===================================================================
// 项目名 : Lean.Takt
// 文件名 : sync-version.js
// 创建者 : Claude
// 创建时间: 2024-01-16
// 版本号 : v1.0.0
// 描述    : 版本信息同步脚本
//===================================================================

import fs from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

/**
 * 从Directory.Build.props读取版本信息
 */
function readVersionFromProps() {
  const propsPath = path.join(__dirname, '../backend/Directory.Build.props')
  
  if (!fs.existsSync(propsPath)) {
    console.error('Directory.Build.props 文件不存在:', propsPath)
    return null
  }
  
  const content = fs.readFileSync(propsPath, 'utf8')
  
  // 解析XML获取版本信息
  const versionMatch = content.match(/<Version>([^<]+)<\/Version>/)
  const productMatch = content.match(/<Product>([^<]+)<\/Product>/)
  const companyMatch = content.match(/<Company>([^<]+)<\/Company>/)
  
  if (!versionMatch) {
    console.error('无法从Directory.Build.props中读取版本信息')
    return null
  }
  
  return {
    version: versionMatch[1],
    product: productMatch ? productMatch[1] : 'Lean.Takt',
    company: companyMatch ? companyMatch[1] : 'Takt365.Inc'
  }
}

/**
 * 更新package.json
 */
function updatePackageJson(versionInfo) {
  const packagePath = path.join(__dirname, '../frontend/package.json')
  
  if (!fs.existsSync(packagePath)) {
    console.error('package.json 文件不存在:', packagePath)
    return false
  }
  
  const packageJson = JSON.parse(fs.readFileSync(packagePath, 'utf8'))
  
  // 更新版本信息
  packageJson.version = versionInfo.version
  packageJson.name = `lean-${versionInfo.product.toLowerCase().replace(/\s+/g, '-')}`
  
  // 添加额外的元信息
  packageJson.product = versionInfo.product
  packageJson.company = versionInfo.company
  
  // 写回文件
  fs.writeFileSync(packagePath, JSON.stringify(packageJson, null, 2) + '\n')
  
  console.log('✅ package.json 已更新:')
  console.log(`   版本: ${packageJson.version}`)
  console.log(`   名称: ${packageJson.name}`)
  console.log(`   产品: ${packageJson.product}`)
  console.log(`   公司: ${packageJson.company}`)
  
  return true
}

/**
 * 更新前端配置文件
 */
function updateFrontendConfig(versionInfo) {
  const settingPath = path.join(__dirname, '../frontend/src/setting.ts')
  
  if (!fs.existsSync(settingPath)) {
    console.error('setting.ts 文件不存在:', settingPath)
    return false
  }
  
  let content = fs.readFileSync(settingPath, 'utf8')
  
  // 更新标题
  content = content.replace(
    /title: '[^']*'/,
    `title: '${versionInfo.product}'`
  )
  
  // 更新副标题
  content = content.replace(
    /subtitle: '[^']*'/,
    `subtitle: '${versionInfo.company} 企业级管理系统'`
  )
  
  // 写回文件
  fs.writeFileSync(settingPath, content)
  
  console.log('✅ setting.ts 已更新')
  return true
}

/**
 * 生成环境变量文件
 */
function generateEnvFile(versionInfo) {
  const envPath = path.join(__dirname, '../frontend/.env.version')
  
  const envContent = `# 版本信息环境变量
# 此文件由版本同步脚本自动生成，请勿手动修改

VITE_APP_VERSION=${versionInfo.version}
VITE_APP_NAME=${versionInfo.product}
VITE_APP_COMPANY=${versionInfo.company}
VITE_BUILD_TIME=${new Date().toISOString()}
`
  
  fs.writeFileSync(envPath, envContent)
  
  console.log('✅ .env.version 已生成')
  return true
}

/**
 * 主函数
 */
function main() {
  console.log('🔄 开始同步版本信息...')
  
  // 读取版本信息
  const versionInfo = readVersionFromProps()
  if (!versionInfo) {
    process.exit(1)
  }
  
  console.log('📖 从Directory.Build.props读取到版本信息:')
  console.log(`   版本: ${versionInfo.version}`)
  console.log(`   产品: ${versionInfo.product}`)
  console.log(`   公司: ${versionInfo.company}`)
  
  // 更新package.json
  if (!updatePackageJson(versionInfo)) {
    process.exit(1)
  }
  
  // 更新前端配置
  if (!updateFrontendConfig(versionInfo)) {
    process.exit(1)
  }
  
  // 生成环境变量文件
  if (!generateEnvFile(versionInfo)) {
    process.exit(1)
  }
  
  console.log('🎉 版本信息同步完成！')
}

// 执行主函数
main() 

//===================================================================
// 项目名 : Lean.Takt
// 文件名 : pre-commit.js
// 创建者 : Claude
// 创建时间: 2024-01-16
// 版本号 : v1.0.0
// 描述    : Git pre-commit钩子，自动同步版本信息
//===================================================================

import { execSync } from 'child_process'
import path from 'path'
import { fileURLToPath } from 'url'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

/**
 * 检查是否有版本相关文件变更
 */
function hasVersionChanges() {
  try {
    // 检查Directory.Build.props是否有变更
    const propsChanges = execSync('git diff --cached --name-only | grep Directory.Build.props', { encoding: 'utf8' })
    if (propsChanges.trim()) {
      return true
    }
    
    // 检查package.json是否有变更
    const packageChanges = execSync('git diff --cached --name-only | grep package.json', { encoding: 'utf8' })
    if (packageChanges.trim()) {
      return true
    }
    
    return false
  } catch (error) {
    // 如果没有找到相关文件，返回false
    return false
  }
}

/**
 * 执行版本同步
 */
function syncVersion() {
  try {
    console.log('🔄 检测到版本相关文件变更，正在同步版本信息...')
    
    // 执行版本同步脚本
    execSync('node scripts/sync-version.js', { 
      cwd: path.join(__dirname, '..'),
      stdio: 'inherit'
    })
    
    // 将同步后的文件添加到暂存区
    execSync('git add frontend/package.json frontend/src/setting.ts', { 
      cwd: path.join(__dirname, '..'),
      stdio: 'inherit'
    })
    
    console.log('✅ 版本信息同步完成并已添加到暂存区')
  } catch (error) {
    console.error('❌ 版本同步失败:', error.message)
    process.exit(1)
  }
}

/**
 * 主函数
 */
function main() {
  console.log('🔍 检查版本相关文件变更...')
  
  if (hasVersionChanges()) {
    syncVersion()
  } else {
    console.log('ℹ️ 没有检测到版本相关文件变更，跳过同步')
  }
}

// 执行主函数
main() 

# TypeScript 开发规范

## 1. 基本规范

### 类型声明
- 始终使用类型声明，避免使用 `any`
- 优先使用接口（interface）而不是类型别名（type）
- 为函数参数和返回值添加类型注解

```typescript
// 推荐
interface UserInfo {
  id: number;
  name: string;
  role: string;
}

// 避免
type UserInfo = {
  id: any;
  name: any;
  role: any;
}
```

## 2. 与 Ant Design Vue 配合使用

### 组件 Props 类型定义
```typescript
import { PropType } from 'vue';
import { TableProps } from 'ant-design-vue';

interface TableData {
  id: number;
  name: string;
}

// 定义组件 Props
const props = defineProps({
  dataSource: {
    type: Array as PropType<TableData[]>,
    required: true
  },
  columns: {
    type: Array as PropType<TableProps['columns']>,
    required: true
  }
});
```

### 事件处理函数类型
```typescript
import { TableProps } from 'ant-design-vue';

// 分页事件处理
const handleTableChange: TableProps['onChange'] = (
  pagination,
  filters,
  sorter
) => {
  // 处理逻辑
};
```

## 3. 接口规范

### API 接口定义
```typescript
// 请求接口
interface ApiRequest<T = any> {
  url: string;
  method: 'GET' | 'POST' | 'PUT' | 'DELETE';
  data?: T;
  params?: Record<string, unknown>;
}

// 响应接口
interface ApiResponse<T = any> {
  code: number;
  data: T;
  message: string;
}
```

### 业务模型接口
```typescript
// 用户模型
interface User {
  id: number;
  username: string;
  email: string;
  role: string;
  status: 0 | 1;
  createTime: string;
}

// 分页请求参数
interface PaginationParams {
  page: number;
  pageSize: number;
  total?: number;
}
```

## 4. 类型断言规范

### 类型断言使用
- 优先使用 `as` 语法而不是尖括号语法
- 必要时才使用类型断言
- 避免使用 `!` 非空断言

```typescript
// 推荐
const element = event.target as HTMLInputElement;

// 避免
const element = <HTMLInputElement>event.target;
const value = element!.value;
```

## 5. 泛型使用规范

### 泛型命名
- 使用单个大写字母表示泛型参数
- 常用泛型参数命名：
  - T：Type 的缩写，表示类型
  - K：Key 的缩写，表示对象的键类型
  - V：Value 的缩写，表示对象的值类型
  - E：Element 的缩写，表示元素类型

```typescript
// 推荐
function getFirstElement<T>(arr: T[]): T | undefined {
  return arr[0];
}

// 避免
function getFirstElement<Type>(arr: Type[]): Type | undefined {
  return arr[0];
}
```

## 6. 注释规范

### 文档注释
- 为接口、类、方法添加 JSDoc 注释
- 说明参数类型和返回值
- 添加示例代码

```typescript
/**
 * 用户服务类
 */
class UserService {
  /**
   * 获取用户信息
   * @param id 用户ID
   * @returns 用户信息
   * @throws 用户不存在时抛出错误
   */
  async getUserInfo(id: number): Promise<User> {
    // 实现逻辑
  }
}
```

## 7. 文件组织规范

### 目录结构
```
src/
├── types/           # 类型定义文件
│   ├── user.ts      # 用户相关类型
│   ├── api.ts       # API 相关类型
│   └── index.ts     # 类型导出
├── api/             # API 请求
│   └── user.ts      # 用户相关接口
└── utils/           # 工具函数
    └── type-guards.ts # 类型守卫
```

### 导入导出规范
```typescript
// 推荐
export interface User {
  // ...
}

// 避免
export default interface User {
  // ...
}
```

## 8. 类型守卫规范

### 自定义类型守卫
```typescript
interface Admin {
  role: 'admin';
  permissions: string[];
}

interface User {
  role: 'user';
  groups: string[];
}

// 类型守卫函数
function isAdmin(user: Admin | User): user is Admin {
  return user.role === 'admin';
}
```

## 9. 与 Ant Design Vue 组件类型配合

### 表单类型
```typescript
import { FormProps } from 'ant-design-vue';

interface LoginForm {
  username: string;
  password: string;
  remember: boolean;
}

// 表单验证规则
const rules: FormProps['rules'] = {
  username: [{ required: true, message: '请输入用户名' }],
  password: [{ required: true, message: '请输入密码' }]
};
```

### 表格类型
```typescript
import { TableProps } from 'ant-design-vue';

// 表格列定义
const columns: TableProps['columns'] = [
  {
    title: '用户名',
    dataIndex: 'username',
    key: 'username'
  },
  {
    title: '角色',
    dataIndex: 'role',
    key: 'role'
  }
];
```

## 10. 最佳实践提示

1. 严格遵循 Ant Design Vue 的类型定义
2. 使用 IDE 的类型提示功能
3. 保持类型定义的一致性
4. 避免过度使用类型断言
5. 合理使用类型推断
6. 定期更新依赖包以获取最新的类型定义 
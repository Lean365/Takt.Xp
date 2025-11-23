<!--
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: LogicFlow 流程属性面板（基于 OpenAuth.Net CreatedFlow，使用 Ant Design Vue）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
-->
<template>
  <div style="height: 100%" class="flow-attr-box">
    <a-tabs size="small" v-model:activeKey="activeKey" style="height: 100%">
      <a-tab-pane key="node-attr" :disabled="currentSelect.type === 'edge'">
        <template #tab>
          <FileTextOutlined />
          节点属性
        </template>
        <template v-if="currentSelect && currentSelect.id">
          <a-form :model="currentSelect" :label-col="{ span: 8 }" :wrapper-col="{ span: 16 }">
            <a-divider>
              <a-input size="small" style="width: 200px" v-model:value="currentSelect.id" :disabled="true" />
            </a-divider>

            <a-form-item label="名称" name="text">
              <a-input v-model:value="currentSelect.text" @change="updateNodeText" />
            </a-form-item>

            <template v-if="currentSelect.properties && currentSelect.properties.setInfo">
              <a-form-item
                label="网关/会签类型"
                v-if="currentSelect.properties.originalType == 'multiInstance' || currentSelect.properties.originalType == 'fork'"
                name="NodeConfluenceType"
              >
                <a-select
                  class="filter-item"
                  style="width: 100%"
                  v-model:value="currentSelect.properties.setInfo.NodeConfluenceType"
                  placeholder="请选择"
                  @change="updateNodeProperty"
                >
                  <a-select-option v-for="item in MultiInstanceTypes" :key="item.id" :value="item.id">
                    {{ item.name }}
                  </a-select-option>
                </a-select>
              </a-form-item>

              <a-form-item label="执行权限" name="NodeDesignate">
                <a-select
                  class="filter-item"
                  style="width: 100%"
                  v-model:value="currentSelect.properties.setInfo.NodeDesignate"
                  placeholder="请选择"
                  @change="handleChangeRoles"
                >
                  <a-select-option v-for="item in NodeDesignates" :key="item.id" :value="item.id">
                    {{ item.name }}
                  </a-select-option>
                </a-select>
              </a-form-item>

              <a-form-item
                label="指定用户"
                v-if="currentSelect.properties.setInfo.NodeDesignate === 'SPECIAL_USER'"
                name="NodeDesignateUsers"
              >
                <a-select
                  v-model:value="currentSelect.properties.setInfo.NodeDesignateData.datas"
                  mode="multiple"
                  :options="userOptions"
                  :loading="userLoading"
                  @search="handleUserSearch"
                  placeholder="请选择用户"
                  style="width: 100%"
                  @change="updateNodeProperty"
                />
              </a-form-item>

              <a-form-item
                label="指定角色"
                v-if="currentSelect.properties.setInfo.NodeDesignate === 'SPECIAL_ROLE'"
                name="NodeDesignateUsers"
              >
                <a-select
                  v-model:value="currentSelect.properties.setInfo.NodeDesignateData.datas"
                  mode="multiple"
                  :options="roleOptions"
                  :loading="roleLoading"
                  @search="handleRoleSearch"
                  placeholder="请选择角色"
                  style="width: 100%"
                  @change="updateNodeProperty"
                />
              </a-form-item>

              <a-form-item label="SQL" v-if="currentSelect.properties.setInfo.NodeDesignate === 'SPECIAL_SQL'">
                <a-textarea v-model:value="selSql" @change="sqlChange" :rows="5" />
                <div class="sql-tags-tip">
                  <div class="tip-content">
                    <InfoCircleOutlined />
                    <span>点击标签可快速插入，当流程运行时，会将标签自动替换成对应登录人的信息</span>
                  </div>
                </div>
                <div class="sql-tags-container">
                  <a-tag
                    v-for="tag in sqltags"
                    :key="tag.columnName"
                    @click="handleTagClick(tag)"
                    class="sql-tag"
                    style="cursor: pointer"
                  >
                    {{ tag.remark }}
                  </a-tag>
                </div>
              </a-form-item>

              <a-form-item
                label="多级主管审批终点"
                v-if="currentSelect.properties.setInfo.NodeDesignate === 'RUNTIME_MANY_PARENTS'"
                name="NodeDesignateUsers"
              >
                <a-select
                  v-model:value="currentSelect.properties.setInfo.NodeDesignateData.datas"
                  mode="multiple"
                  :options="roleOptions"
                  :loading="roleLoading"
                  @search="handleRoleSearch"
                  placeholder="请选择角色"
                  style="width: 100%"
                  @change="updateNodeProperty"
                />
              </a-form-item>

              <a-form-item
                label="所属部门"
                v-if="currentSelect.properties.setInfo.NodeDesignate === 'RUNTIME_CHAIRMAN'"
                name="NodeDesignateUsers"
              >
                <TaktTreeSelect
                  v-model:value="currentSelect.properties.setInfo.NodeDesignateData.datas"
                  :multiple="true"
                  :tree-data="deptTreeData"
                  :loading="deptLoading"
                  placeholder="请选择部门"
                  style="width: 100%"
                  @change="updateNodeProperty"
                />
              </a-form-item>

              <a-form-item label="可编辑字段" name="CanWriteFormItemIds">
                <a-select
                  multiple
                  class="filter-item"
                  style="width: 100%"
                  v-model:value="currentSelect.properties.setInfo.CanWriteFormItemIds"
                  placeholder="请选择"
                  @change="updateNodeProperty"
                >
                  <a-select-option v-for="item in formTemplate" :key="item.name" :value="item.name">
                    {{ item.title }}
                  </a-select-option>
                </a-select>
              </a-form-item>
            </template>
          </a-form>
        </template>
      </a-tab-pane>
      <a-tab-pane key="link-attr" :disabled="currentSelect.type !== 'edge'">
        <template #tab>
          <ShareAltOutlined />
          连线属性
        </template>
        <a-form :label-col="{ span: 8 }" :wrapper-col="{ span: 16 }">
          <a-divider>
            <a-input size="small" style="width: 200px" v-model:value="currentSelect.id" :disabled="true" />
          </a-divider>
          <a-form-item label="源节点">
            <a-input :value="currentSelect.sourceNodeId" disabled />
          </a-form-item>
          <a-form-item label="目标节点">
            <a-input :value="currentSelect.targetNodeId" disabled />
          </a-form-item>
          <a-form-item label="文本">
            <a-input v-model:value="currentSelect.text" @change="updateEdgeText" />
          </a-form-item>

          <a-divider>设置分支条件</a-divider>

          <a-row v-for="(compare, index) in Compares" :key="index" :gutter="8" style="margin-bottom: 8px">
            <a-col :span="8">
              <a-select size="small" v-model:value="compare.FieldName" style="width: 100%" placeholder="请选择">
                <a-select-option v-for="item in formTemplate" :key="item.name" :value="item.name">
                  {{ item.title }}
                </a-select-option>
              </a-select>
            </a-col>
            <a-col :span="6">
              <a-select
                size="small"
                :disabled="!compare.FieldName"
                v-model:value="compare.Operation"
                style="width: 100%"
                placeholder="请选择"
              >
                <a-select-option value=">">></a-select-option>
                <a-select-option value=">=">>=</a-select-option>
                <a-select-option value="<"><</a-select-option>
                <a-select-option value="<="><=</a-select-option>
                <a-select-option value="=">=</a-select-option>
                <a-select-option value="!=">!=</a-select-option>
              </a-select>
            </a-col>
            <a-col :span="6">
              <a-input
                size="small"
                v-model:value="compare.Value"
                :disabled="!compare.FieldName"
                placeholder="值"
              />
            </a-col>
            <a-col :span="4">
              <a-button type="primary" size="small" v-if="index === 0" title="并且" @click="btnAddCompare">
                <template #icon><PlusOutlined /></template>
              </a-button>
              <a-button
                type="primary"
                danger
                size="small"
                v-if="index != 0"
                title="删除"
                @click="btnDelCompare(index)"
              >
                <template #icon><DeleteOutlined /></template>
              </a-button>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script setup>
import { ref, watch, onMounted, computed } from 'vue'
import { FileTextOutlined, ShareAltOutlined, InfoCircleOutlined, PlusOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { getDeptTree } from '@/api/identity/dept'
import { getRoleList } from '@/api/identity/role'
import { getUserList } from '@/api/identity/user'
import { debounce } from 'lodash-es'

const props = defineProps({
  lf: Object,
  currentSelect: Object,
  formTemplate: Array
})

const selSql = ref('')
const deptTreeData = ref([])
const deptLoading = ref(false)
const userOptions = ref([])
const userLoading = ref(false)
const roleOptions = ref([])
const roleLoading = ref(false)
const Compares = ref([])

const MultiInstanceTypes = ref([])
const NodeDesignates = ref([])

const sqltags = [
  { remark: '{当前登录用户的角色}', columnName: '{loginRole}' },
  { remark: '{当前登录的用户}', columnName: '{loginUser}' },
  { remark: '{当前登录用户的部门}', columnName: '{loginOrg}' }
]

const activeKey = ref('node-attr')

// 更新节点文本
const updateNodeText = () => {
  if (props.lf && props.currentSelect && props.currentSelect.id) {
    props.lf.setProperties(props.currentSelect.id, {
      ...props.currentSelect.properties,
      text: props.currentSelect.text
    })
  }
}

// 更新节点属性
const updateNodeProperty = () => {
  if (props.lf && props.currentSelect && props.currentSelect.id) {
    props.lf.setProperties(props.currentSelect.id, props.currentSelect.properties)
  }
}

// 更新边文本
const updateEdgeText = () => {
  if (props.lf && props.currentSelect && props.currentSelect.id) {
    props.lf.setProperties(props.currentSelect.id, {
      ...props.currentSelect.properties,
      text: props.currentSelect.text
    })
  }
}

// 加载部门树
const loadDeptTree = async () => {
  deptLoading.value = true
  try {
    const res = await getDeptTree({})
    deptTreeData.value = res.data || []
  } catch (error) {
    console.error('加载部门树失败', error)
  } finally {
    deptLoading.value = false
  }
}

// 加载角色列表
const loadRoleList = debounce(async (keyword = '') => {
  roleLoading.value = true
  try {
    const res = await getRoleList({ pageIndex: 1, pageSize: 100, roleName: keyword })
    roleOptions.value =
      res.data?.items?.map((item) => ({
        label: item.roleName,
        value: item.roleId
      })) || []
  } catch (error) {
    console.error('加载角色列表失败', error)
  } finally {
    roleLoading.value = false
  }
}, 300)

// 加载用户列表
const loadUserList = debounce(async (keyword = '') => {
  userLoading.value = true
  try {
    const res = await getUserList({ pageIndex: 1, pageSize: 100, userName: keyword })
    userOptions.value =
      res.data?.items?.map((item) => ({
        label: item.userName + (item.nickName ? `(${item.nickName})` : ''),
        value: item.userId
      })) || []
  } catch (error) {
    console.error('加载用户列表失败', error)
  } finally {
    userLoading.value = false
  }
}, 300)

const handleUserSearch = (value) => {
  loadUserList(value)
}

const handleRoleSearch = (value) => {
  loadRoleList(value)
}

const btnAddCompare = () => {
  Compares.value.push({
    FieldName: '',
    Operation: '',
    Value: ''
  })
}

const handleTagClick = (tag) => {
  selSql.value += ' ' + tag.columnName
  sqlChange()
}

const btnDelCompare = (index) => {
  Compares.value.splice(index, 1)
}

const sqlChange = () => {
  if (props.currentSelect?.properties?.setInfo?.NodeDesignateData) {
    if (!props.currentSelect.properties.setInfo.NodeDesignateData.datas) {
      props.currentSelect.properties.setInfo.NodeDesignateData.datas = []
    }
    props.currentSelect.properties.setInfo.NodeDesignateData.datas[0] = selSql.value
    updateNodeProperty()
  }
}

const handleChangeRoles = () => {
  if (props.currentSelect?.properties?.setInfo?.NodeDesignateData) {
    props.currentSelect.properties.setInfo.NodeDesignateData.Texts = ''
    props.currentSelect.properties.setInfo.NodeDesignateData.datas = []
    updateNodeProperty()
  }
}

const changeAttrType = () => {
  if (props.currentSelect?.type === 'edge') {
    activeKey.value = 'link-attr'
  } else {
    activeKey.value = 'node-attr'
  }
}

// 监听 currentSelect 变化
watch(
  () => props.currentSelect,
  (val) => {
    if (!val || !val.id) return

    changeAttrType()

    if (val.properties?.setInfo?.NodeDesignate === 'SPECIAL_SQL') {
      selSql.value = val.properties.setInfo.NodeDesignateData?.datas?.[0] || ''
    }

    if (val.properties?.Compares) {
      Compares.value = val.properties.Compares
    } else if (val.type === 'edge') {
      Compares.value = val.properties?.Compares || []
    }

    const originalType = val.properties?.originalType || val.type

    if (originalType == 'multiInstance') {
      NodeDesignates.value = [
        { id: 'SPECIAL_ROLE', name: '指定角色' },
        { id: 'SPECIAL_USER', name: '指定用户' },
        { id: 'SPECIAL_SQL', name: '指定SQL' }
      ]
      MultiInstanceTypes.value = [
        { id: 'sequential', name: '依次' },
        { id: 'all', name: '全部通过' },
        { id: 'one', name: '至少一个通过' }
      ]
    } else {
      MultiInstanceTypes.value = [
        { id: 'all', name: '全部通过' },
        { id: 'one', name: '至少一个通过' }
      ]

      if (originalType == 'fork' || originalType == 'join') {
        NodeDesignates.value = [{ id: 'ALL_USER', name: '所有人' }]
      } else {
        NodeDesignates.value = [
          { id: 'ALL_USER', name: '所有人' },
          { id: 'SPECIAL_ROLE', name: '指定角色' },
          { id: 'SPECIAL_USER', name: '指定用户' },
          { id: 'SPECIAL_SQL', name: '指定SQL' },
          { id: 'RUNTIME_PARENT', name: '上一节点执行人的直属上级' },
          { id: 'RUNTIME_MANY_PARENTS', name: '连续多级直属上级' },
          { id: 'RUNTIME_CHAIRMAN', name: '部门负责人' },
          { id: 'RUNTIME_SPECIAL_ROLE', name: '运行时选定角色' },
          { id: 'RUNTIME_SPECIAL_USER', name: '运行时选定用户' }
        ]
      }
    }
  },
  { deep: true, immediate: true }
)

onMounted(() => {
  loadDeptTree()
  loadRoleList()
  loadUserList()
})
</script>

<style lang="scss">
@import '../style/flow-attr.scss';

.flow-attr-box {
  .ant-tabs-content-holder {
    height: calc(100% - 40px);
    overflow: auto;
    padding: 0 10px;
  }

  .sql-tags-tip {
    margin: 10px 0;
    padding: 8px;
    background-color: #f4f4f5;
    border-radius: 4px;

    .tip-content {
      display: flex;
      align-items: center;

      .anticon {
        color: #909399;
        margin-right: 5px;
      }

      span {
        color: #606266;
        font-size: 12px;
      }
    }
  }

  .sql-tags-container {
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
    margin-top: 8px;

    .sql-tag {
      cursor: pointer;
      transition: all 0.3s;

      &:hover {
        background-color: #ecf5ff;
        color: #1890ff;
      }
    }
  }
}
</style>


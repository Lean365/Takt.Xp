export default {
  generator: {
    tableDefine: {
      title: '表定义',
      dbType: '数据库类型',
      connectionString: '连接字符串',
      databaseName: '数据库名称',
      tableName: '表名',
      tableComment: '表注释',
      author: '作者',
      remark: '备注',
      server: '服务器',
      port: '端口',
      userId: '用户名',
      password: '密码',
      tablePrefix: '表前缀',
      tableNameFirst: '模块名称',
      tableNameSecond: '表名2',
      tableNameThird: '表名3',
      fields: {
        server: {
          placeholder: '请输入服务器'
        },
        port: {
          placeholder: '请输入端口'
        },
        databaseName: {
          placeholder: '请选择数据库名称'
        },
        userId: {
          placeholder: '请输入用户名'
        },
        password: {
          placeholder: '请输入密码'
        },
        tablePrefix: {
          placeholder: '请选择表前缀'
        },
        tableNameFirst: {
          placeholder: '请选择模块名称'
        },
        tableNameSecond: {
          placeholder: '请输入表名2'
        },
        tableNameThird: {
          placeholder: '请输入表名3'
        },
        tableName: {
          placeholder: '请输入表名'
        },
        connectionString: {
          placeholder: '请输入连接字符串'
        },
        tableComment: {
          placeholder: '请输入表注释'
        },
        author: {
          placholder: '请输入作者'
        },  
      }
    },
    ColumnDefine: {
      title: '列定义',
      tableId: '表ID',
      columnName: '列名',
      columnComment: '列注释',
      dbColumnType: '数据类型',
      isPrimaryKey: '主键',
      isRequired: '必填',
      isIncrement: '自增',
      columnLength: '列长度',
      decimalDigits: '小数位数',
      defaultValue: '默认值',
      orderNum: '排序',
      fields: {
        columnName: {
          placeholder: '请输入列名'
        },
        columnComment: {
          placeholder: '请输入列注释'
        },
        dbColumnType: {
          placeholder: '请选择数据类型'
        },
        isPrimaryKey: {
          placeholder: '请选择是否为主键'
        },
        isRequired: {
          placeholder: '请选择是否为必填'
        },
        isIncrement: {
          placeholder: '请选择是否为自增'
        },
        columnLength: {
          placeholder: '请输入列长度'
        },
        decimalDigits: {
          placeholder: '请输入小数位数'
        },
        defaultValue: {
          placeholder: '请输入默认值'
        },
        orderNum: {
          placeholder: '请输入排序'
        },       
      }
    }
  }
} 
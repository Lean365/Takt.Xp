export default {
  admin: {
    dicttype: {
      title: 'Dictionary Type',
      form: {
        name: 'Dictionary Name',
        namePlaceholder: 'Please enter dictionary name',
        type: 'Dictionary Type',
        typePlaceholder: 'Please enter dictionary type',
        category: 'Dictionary Category',
        categoryPlaceholder: 'Please select dictionary category',
        status: 'Status',
        statusPlaceholder: 'Please select status',
        builtin:'Builtin',
        builtinPlaceholder:'Please select builtin',
        orderNum: 'Order Number',
        sqlScript: 'SQL Script',
        sqlScriptPlaceholder: 'Please enter SQL script',
        remark: 'Remark',
        remarkPlaceholder: 'Please enter remark'
      },
      category: {
        system: 'System',
        sql: 'SQL'
      },
      rules: {
        nameRequired: 'Dictionary name cannot be empty',
        namePattern: 'Dictionary name can only contain letters, numbers, Chinese characters, underscores and hyphens, length between 2-100',
        typeRequired: 'Dictionary type cannot be empty',
        typePattern: 'Dictionary type must start with a letter and can only contain letters, numbers and underscores, length between 2-100',
        categoryRequired: 'Dictionary category cannot be empty',
        orderNumRequired: 'Order number cannot be empty',
        orderNumRange: 'Order number must be between 0-9999',
        statusRequired: 'Status cannot be empty',
        sqlPattern: 'SQL script must contain SELECT and FROM statements'
      },
      sqlHelp: {
        title: 'Standard SQL Example:',
        description: 'Field Description:',
        example: `SELECT
  name as label,    -- Display text
  code as value,    -- Key value
  css_class,        -- CSS class name
  list_class,       -- List class name
  status,          -- Status (0 normal 1 disabled)
  ext_label,       -- Extended label
  ext_value,       -- Extended value
  trans_key,       -- Translation key
  order_num,       -- Order number
  remark           -- Remark
FROM your_table 
WHERE status = 0 
ORDER BY order_num`,
        fields: {
          label: 'label/name/text: Display text',
          value: 'value/key/code: Key value',
          cssClass: 'css_class: CSS class name',
          listClass: 'list_class: List class name',
          status: 'status: Status (0 normal 1 disabled)',
          extLabel: 'ext_label: Extended label',
          extValue: 'ext_value: Extended value',
          transKey: 'trans_key: Translation key',
          orderNum: 'order_num: Order number',
          remark: 'remark: Remark'
        }
      }
    }
  }
} 
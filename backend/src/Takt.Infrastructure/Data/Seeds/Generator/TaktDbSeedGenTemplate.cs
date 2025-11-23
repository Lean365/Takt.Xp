//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedGenTemplate.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成模板种子数据 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Generator;

namespace Takt.Infrastructure.Data.Seeds.Generator;

/// <summary>
/// 代码生成模板种子数据
/// </summary>
/// <remarks>
/// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
/// </remarks>
public class TaktDbSeedGenTemplate
{

    /// <summary>
    /// 获取种子数据
    /// </summary>
    public List<TaktGenTemplate> GetDefaultTemplate()
    {
        return new List<TaktGenTemplate>
        {
            // 1. 实体类
            new TaktGenTemplate
            {
                TemplateName = "Entity",
                TemplateCodeType = 1,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace {{base_namespace}}.Domain.Entities.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}
    /// </summary>
    [SugarTable(""{{table_name}}"", ""{{table_comment}}"")]
    public class {{className}} : TaktBaseEntity
    {
        {{for column in columns}}
        /// <summary>
        /// {{column.column_comment}}
        /// </summary>
        [SugarColumn(ColumnName = ""{{column.column_name}}"", ColumnDescription = ""{{column.column_comment}}"", {{if column.is_primary_key}}IsPrimaryKey = true, {{end}}{{if column.is_identity}}IsIdentity = true, {{end}}{{if column.length > 0}}Length = {{column.length}}, {{end}}ColumnDataType = ""{{column.data_type}}"", IsNullable = {{column.is_nullable}})]
        public {{column.csharp_type}} {{column.property_name}} { get; set; }
        {{end}}
    }
}",
                FileName = "{{className}}.cs",
                Status = 0
            },

            // 2. 数据传输对象
            new TaktGenTemplate
            {
                TemplateName = "Dto",
                TemplateCodeType = 1,
                TemplateCategory = 2,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using System.ComponentModel.DataAnnotations;

namespace {{base_namespace}}.Domain.Dtos.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}数据传输对象
    /// </summary>
    public class {{className}}Dto
    {
        {{for column in columns}}
        /// <summary>
        /// {{column.column_comment}}
        /// </summary>
        [Display(Name = ""{{column.column_comment}}"")]
        public {{column.csharp_type}} {{column.property_name}} { get; set; }
        {{end}}
    }
}",
                FileName = "{{className}}Dto.cs",
                Status = 0
            },

            // 3. 仓储接口
            new TaktGenTemplate
            {
                TemplateName = "IRepository",
                TemplateCodeType = 1,
                TemplateCategory = 3,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using {{base_namespace}}.Domain.Entities.{{module_name}};

namespace {{base_namespace}}.Domain.IRepositories.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}仓储接口
    /// </summary>
    public interface I{{className}}Repository : ITaktRepository<{{className}}>
    {
    }
}",
                FileName = "I{{className}}Repository.cs",
                Status = 0
            },

            // 4. 仓储实现
            new TaktGenTemplate
            {
                TemplateName = "Repository",
                TemplateCodeType = 1,
                TemplateCategory = 4,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using {{base_namespace}}.Domain.Entities.{{module_name}};
using {{base_namespace}}.Domain.IRepositories.{{module_name}};

namespace {{base_namespace}}.Infrastructure.Repositories.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}仓储实现
    /// </summary>
    public class {{className}}Repository : TaktRepository<{{className}}>, I{{className}}Repository
    {
        public {{className}}Repository(ITaktDbContext dbContext) : base(dbContext)
        {
        }
    }
}",
                FileName = "{{className}}Repository.cs",
                Status = 0
            },

            // 5. 服务接口
            new TaktGenTemplate
            {
                TemplateName = "IService",
                TemplateCodeType = 1,
                TemplateCategory = 5,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using {{base_namespace}}.Domain.Dtos.{{module_name}};
using {{base_namespace}}.Domain.Entities.{{module_name}};

namespace {{base_namespace}}.Domain.IServices.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}服务接口
    /// </summary>
    public interface I{{className}}Service
    {
        /// <summary>
        /// 获取{{table_comment}}列表
        /// </summary>
        Task<List<{{className}}Dto>> GetListAsync();

        /// <summary>
        /// 获取{{table_comment}}详情
        /// </summary>
        Task<{{className}}Dto> GetInfoAsync(long id);

        /// <summary>
        /// 创建{{table_comment}}
        /// </summary>
        Task<{{className}}Dto> CreateAsync({{className}}Dto dto);

        /// <summary>
        /// 更新{{table_comment}}
        /// </summary>
        Task<{{className}}Dto> UpdateAsync({{className}}Dto dto);

        /// <summary>
        /// 删除{{table_comment}}
        /// </summary>
        Task DeleteAsync(long id);
    }
}",
                FileName = "I{{className}}Service.cs",
                Status = 0
            },

            // 6. 服务实现
            new TaktGenTemplate
            {
                TemplateName = "Service",
                TemplateCodeType = 1,
                TemplateCategory = 6,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using {{base_namespace}}.Domain.Dtos.{{module_name}};
using {{base_namespace}}.Domain.Entities.{{module_name}};
using {{base_namespace}}.Domain.IServices.{{module_name}};
using {{base_namespace}}.Domain.IServices.Extensions;

namespace {{base_namespace}}.Infrastructure.Services.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}服务实现
    /// </summary>
    public class {{className}}Service : I{{className}}Service
    {
        private readonly ITaktRepository<{{className}}> _repository;
        private readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public {{className}}Service(ITaktRepository<{{className}}> repository, ITaktLogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// 获取{{table_comment}}列表
        /// </summary>
        public async Task<List<{{className}}Dto>> GetListAsync()
        {
            var list = await _repository.GetListAsync();
            return list.Select(x => new {{className}}Dto
            {
                {{for column in columns}}
                {{column.property_name}} = x.{{column.property_name}},
                {{end}}
            }).ToList();
        }

        /// <summary>
        /// 获取{{table_comment}}详情
        /// </summary>
        public async Task<{{className}}Dto> GetInfoAsync(long id)
        {
            var entity = await _repository.GetFirstAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception($""未找到ID为{id}的{{table_comment}}"");
            }

            return new {{className}}Dto
            {
                {{for column in columns}}
                {{column.property_name}} = entity.{{column.property_name}},
                {{end}}
            };
        }

        /// <summary>
        /// 创建{{table_comment}}
        /// </summary>
        public async Task<{{className}}Dto> CreateAsync({{className}}Dto dto)
        {
            var entity = new {{className}}
            {
                {{for column in columns}}
                {{column.property_name}} = dto.{{column.property_name}},
                {{end}}
            };

            await _repository.CreateAsync(entity);
            return dto;
        }

        /// <summary>
        /// 更新{{table_comment}}
        /// </summary>
        public async Task<{{className}}Dto> UpdateAsync({{className}}Dto dto)
        {
            var entity = await _repository.GetFirstAsync(x => x.Id == dto.Id);
            if (entity == null)
            {
                throw new Exception($""未找到ID为{dto.Id}的{{table_comment}}"");
            }

            {{for column in columns}}
            entity.{{column.property_name}} = dto.{{column.property_name}};
            {{end}}

            await _repository.UpdateAsync(entity);
            return dto;
        }

        /// <summary>
        /// 删除{{table_comment}}
        /// </summary>
        public async Task DeleteAsync(long id)
        {
            var entity = await _repository.GetFirstAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception($""未找到ID为{id}的{{table_comment}}"");
            }

            await _repository.DeleteAsync(entity);
        }
    }
}",
                FileName = "{{className}}Service.cs",
                Status = 0
            },

            // 7. 控制器
            new TaktGenTemplate
            {
                TemplateName = "Controller",
                TemplateCodeType = 1,
                TemplateCategory = 7,
                TemplateLanguage = 1,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"using {{base_namespace}}.Domain.Dtos.{{module_name}};
using {{base_namespace}}.Domain.IServices.{{module_name}};
using Microsoft.AspNetCore.Mvc;

namespace {{base_namespace}}.Web.Controllers.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}控制器
    /// </summary>
    [Route(""api/[controller]"")]
    [ApiController]
    public class {{className}}Controller : ControllerBase
    {
        private readonly I{{className}}Service _service;
        private readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public {{className}}Controller(I{{className}}Service service, ITaktLogger logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// 获取{{table_comment}}列表
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<{{className}}Dto>>> GetList()
        {
            try
            {
                var list = await _service.GetListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Error(""获取{{table_comment}}列表失败"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 获取{{table_comment}}详情
        /// </summary>
        [HttpGet(""{id}"")]
        public async Task<ActionResult<{{className}}Dto>> GetInfo(long id)
        {
            try
            {
                var info = await _service.GetInfoAsync(id);
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.Error($""获取{{table_comment}}详情失败，ID：{id}"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 创建{{table_comment}}
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<{{className}}Dto>> Create([FromBody] {{className}}Dto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(""创建{{table_comment}}失败"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 更新{{table_comment}}
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<{{className}}Dto>> Update([FromBody] {{className}}Dto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($""更新{{table_comment}}失败，ID：{dto.Id}"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除{{table_comment}}
        /// </summary>
        [HttpDelete(""{id}"")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($""删除{{table_comment}}失败，ID：{id}"", ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}",
                FileName = "{{className}}Controller.cs",
                Status = 0
            },

            // 8. API
            new TaktGenTemplate
            {
                TemplateName = "api.ts",
                TemplateCodeType = 0,
                TemplateCategory = 8,
                TemplateLanguage = 2,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"import request from '@/utils/request';
import type { {{className}}Dto } from './type';

export function getList() {
  return request.get<{{className}}Dto[]>('/api/{{table_name}}');
}

export function getInfo(id: number) {
  return request.get<{{className}}Dto>(`/api/{{table_name}}/${id}`);
}

export function create(data: {{className}}Dto) {
  return request.post('/api/{{table_name}}', data);
}

export function update(data: {{className}}Dto) {
  return request.put('/api/{{table_name}}', data);
}

export function remove(id: number) {
  return request.delete(`/api/{{table_name}}/${id}`);
}",
                FileName = "{{className}}.ts",
                Status = 0
            },

            // 9. 类型
            new TaktGenTemplate
            {
                TemplateName = "type.d.ts",
                TemplateCodeType = 0,
                TemplateCategory = 9,
                TemplateLanguage = 2,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"export interface {{className}}Dto {
  {{for column in columns}}
  /** {{column.column_comment}} */
  {{column.property_name}}: {{column.ts_type}};
  {{end}}
}",
                FileName = "{{className}}.d.ts",
                Status = 0
            },

            // 10. 视图
            new TaktGenTemplate
            {
                TemplateName = "index.vue",
                TemplateCodeType = 0,
                TemplateCategory = 10,
                TemplateLanguage = 5,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"<template>
  <div class='{{table_name}}-manage'>
    <ManageForm />
  </div>
</template>
<script setup lang='ts'>
import ManageForm from './components/ManageForm.vue';
</script>
<style scoped>
.{{table_name}}-manage {
  padding: 16px;
}
</style>",
                FileName = "index.vue",
                Status = 0
            },

            // 11. 表单组件
            new TaktGenTemplate
            {
                TemplateName = "ManageForm.vue",
                TemplateCodeType = 0,
                TemplateCategory = 11,
                TemplateLanguage = 5,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"<template>
  <el-form :model='form'>
    <!-- 动态生成表单项 -->
    {{for column in columns}}
    <el-form-item label='{{column.column_comment}}'>
      <el-input v-model='form.{{column.property_name}}' />
    </el-form-item>
    {{end}}
  </el-form>
</template>
<script setup lang='ts'>
import { ref } from 'vue';
import type { {{className}}Dto } from '../type';

const form = ref<{{className}}Dto>({});
</script>
<style scoped>
</style>",
                FileName = "components/{{className}}Form.vue",
                Status = 0
            },

            // 12. 翻译
            new TaktGenTemplate
            {
                TemplateName = "Translate",
                TemplateCodeType = 0,
                TemplateCategory = 12,
                TemplateLanguage = 2,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"-- 翻译SQL
INSERT INTO Takt_core_translation (module_name, lang_code, trans_key, trans_value, status, order_num, create_by, create_time, update_by, update_time)
VALUES 
-- 简体中文
('{{module_name}}', 'zh-CN', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'zh-CN', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'zh-CN', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', '请输入{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'zh-CN', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}}不能为空', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'zh-CN', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', '{{column.column_comment}}长度不能超过{{column.csharp_length}}个字符', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 英文
('{{module_name}}', 'en-US', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'en-US', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'en-US', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', 'Please enter {{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'en-US', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}} is required', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'en-US', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', '{{column.column_comment}} length cannot exceed {{column.csharp_length}} characters', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 日语
('{{module_name}}', 'ja-JP', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'ja-JP', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'ja-JP', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', '{{column.column_comment}}を入力してください', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'ja-JP', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}}は必須です', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'ja-JP', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', '{{column.column_comment}}の長さは{{column.csharp_length}}文字を超えることはできません', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 韩语
('{{module_name}}', 'ko-KR', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'ko-KR', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'ko-KR', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', '{{column.column_comment}}을(를) 입력하세요', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'ko-KR', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}}은(는) 필수입니다', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'ko-KR', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', '{{column.column_comment}} 길이는 {{column.csharp_length}}자를 초과할 수 없습니다', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 繁体中文
('{{module_name}}', 'zh-TW', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'zh-TW', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'zh-TW', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', '請輸入{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'zh-TW', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}}不能為空', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'zh-TW', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', '{{column.column_comment}}長度不能超過{{column.csharp_length}}個字符', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 阿拉伯语
('{{module_name}}', 'ar-SA', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'ar-SA', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'ar-SA', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', 'الرجاء إدخال {{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'ar-SA', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}} مطلوب', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'ar-SA', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', 'لا يمكن أن يتجاوز طول {{column.column_comment}} {{column.csharp_length}} حرفًا', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 法语
('{{module_name}}', 'fr-FR', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'fr-FR', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'fr-FR', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', 'Veuillez saisir {{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'fr-FR', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}} est requis', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'fr-FR', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', 'La longueur de {{column.column_comment}} ne peut pas dépasser {{column.csharp_length}} caractères', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 西班牙语
('{{module_name}}', 'es-ES', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'es-ES', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'es-ES', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', 'Por favor ingrese {{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'es-ES', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}} es requerido', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'es-ES', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', 'La longitud de {{column.column_comment}} no puede exceder {{column.csharp_length}} caracteres', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}},

-- 俄语
('{{module_name}}', 'ru-RU', '{{module_name}}.{{className}}', '{{table_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{for column in columns}}
('{{module_name}}', 'ru-RU', '{{module_name}}.{{className}}.{{column.column_name}}', '{{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_type == 'string' && column.is_required == 1}}
('{{module_name}}', 'ru-RU', '{{module_name}}.{{className}}.{{column.column_name}}.placeholder', 'Пожалуйста, введите {{column.column_comment}}', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
('{{module_name}}', 'ru-RU', '{{module_name}}.{{className}}.{{column.column_name}}.validation.required', '{{column.column_comment}} обязательно', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{if column.csharp_length > 0}}
('{{module_name}}', 'ru-RU', '{{module_name}}.{{className}}.{{column.column_name}}.validation.length', 'Длина {{column.column_comment}} не может превышать {{column.csharp_length}} символов', 0, 0, 'admin', GETDATE(), 'admin', GETDATE()),
{{end}}
{{end}}
{{end}};",
                FileName = "{{className}}Trans.sql",
                Status = 0
            },

            // 13. 菜单
            new TaktGenTemplate
            {
                TemplateName = "Menu",
                TemplateCodeType = 0,
                TemplateCategory = 13,
                TemplateLanguage = 2,
                TemplateOrmType = 3,
                TemplateVersion = 1,
                TemplateContent = @"-- 菜单SQL
INSERT INTO Takt_identity_menu (menu_name, trans_key, icon, parent_id, order_num, path, component, query_params, is_external, is_cache, menu_type, visible, perms, status, create_by, create_time, update_by, update_time, remark)
VALUES 
('{{table_comment}}管理', 'menu.{{module_name}}.{{table_name}}', 'FileTextOutlined', {{table_parentmenuid}}, 1, '{{table_name}}', '{{module_name}}/{{table_name}}/index', null, 0, 0, 1, 0, '{{module_name}}:{{table_name}}:list', 0, 'admin', GETDATE(), 'admin', GETDATE(), '{{table_comment}}管理菜单');

-- 按钮父菜单ID
SELECT @parentId := LAST_INSERT_ID();

-- 按钮 SQL
INSERT INTO Takt_identity_menu (menu_name, trans_key, parent_id, order_num, path, component, query_params, is_external, is_cache, menu_type, visible, perms, status, create_by, create_time, update_by, update_time, remark)
VALUES 
('{{table_comment}}查询', 'button.query', @parentId, 1, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:query', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}新增', 'button.create', @parentId, 2, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:create', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}修改', 'button.update', @parentId, 3, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:update', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}删除', 'button.delete', @parentId, 4, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:delete', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}详情', 'button.detail', @parentId, 5, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:detail', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}预览', 'button.preview', @parentId, 6, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:preview', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}打印', 'button.print', @parentId, 7, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:print', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}导入', 'button.import', @parentId, 8, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:import', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}导出', 'button.export', @parentId, 9, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:export', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}模板', 'button.template', @parentId, 10, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:template', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}审核', 'button.Logging', @parentId, 11, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:audit', 0, 'admin', GETDATE(), 'admin', GETDATE(), ''),
('{{table_comment}}撤销', 'button.revoke', @parentId, 12, '#', '', null, 0, 0, 2, 0, '{{module_name}}:{{table_name}}:revoke', 0, 'admin', GETDATE(), 'admin', GETDATE(), '');",
                FileName = "{{className}}Menu.sql",
                Status = 0
            }
        };
    }
}






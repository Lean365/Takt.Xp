export default {
    generator: {
      template: {
        title: 'قالب توليد الكود',  
        fields: {
            templateName: 'اسم القالب',
            templateOrmType: 'إطار ORM',
            templateCodeType: 'نوع الكود',
            templateCategory: 'فئة القالب',
            templateLanguage: 'لغة البرمجة',
            templateVersion: 'رقم الإصدار',
            fileName: 'اسم الملف',
            templateContent: 'محتوى القالب',
            status: 'الحالة',
            remark: 'ملاحظة',
        },
        placeholder: {
            templateName: 'يرجى إدخال اسم القالب',
            templateOrmType: 'يرجى اختيار إطار ORM',
            templateCodeType: 'يرجى اختيار نوع الكود',
            templateCategory: 'يرجى اختيار فئة القالب',
            templateLanguage: 'يرجى اختيار لغة البرمجة',
            templateVersion: 'يرجى إدخال رقم الإصدار',
            fileName: 'يرجى إدخال اسم الملف',
            templateContent: 'يرجى إدخال محتوى القالب',
            remark: 'يرجى إدخال الملاحظة',
            validation: {
                required: 'يرجى إدخال {field}',
                length: 'لا يمكن أن يتجاوز طول {field} {length} حرف',
                minLength: 'لا يمكن أن يقل طول {field} عن {min} حرف',
                pascalCase: 'يجب أن يستخدم {field} تسمية باسكال (الحرف الأول كبير، أحرف فقط)'
            }
        },
        dialog: {
            create: 'إضافة قالب جديد',
            update: 'تعديل القالب'
        },
        messages: {
            success: 'تمت العملية بنجاح',
            error: 'فشلت العملية',
            warning: 'تحذير العملية',
            info: 'معلومات العملية',
            confirm: 'تأكيد',
        },
    }
} }
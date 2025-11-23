export default {
    generator: {
      config: {
        title: 'إعدادات توليد الكود',  
        fields: {
            genConfigName: 'اسم الإعداد',
            author: 'المؤلف',
            moduleName: 'اسم الوحدة',
            projectName: 'اسم المشروع',
            businessName: 'اسم العمل',
            functionName: 'اسم الوظيفة',
            genMethod: 'طريقة التوليد',
            genTplType: 'نوع القالب',
            genPath: 'المسار',
            options: 'الخيارات',
            status: 'الحالة',
            dateRange: 'الفترة الزمنية',
        },
        placeholder: {
            genConfigName: 'يرجى إدخال اسم الإعداد',
            author: 'يرجى إدخال اسم المؤلف',
            moduleName: 'يرجى إدخال اسم الوحدة',
            projectName: 'يرجى إدخال اسم المشروع',
            businessName: 'يرجى إدخال اسم العمل',
            functionName: 'يرجى إدخال اسم الوظيفة',
            genMethod: 'يرجى اختيار طريقة التوليد',
            genTplType: 'يرجى اختيار نوع القالب',
            genPath: 'يرجى إدخال المسار',
            options: 'يرجى إدخال الخيارات',
            status: 'يرجى اختيار الحالة',
            dateRange: 'يرجى اختيار الفترة الزمنية',
            validation: {
                length: 'لا يمكن أن يتجاوز طول {{field}} {{length}}',
                required: 'يرجى إدخال {{field}}',
                minlength: 'لا يمكن أن يقل طول {{field}} عن {{min}}',
                maxlength: 'لا يمكن أن يزيد طول {{field}} عن {{max}}',
                pattern: 'صيغة {{field}} غير صحيحة',
            }
        },
        messages: {
            success: 'تمت العملية بنجاح',
            error: 'فشلت العملية',
            warning: 'تحذير العملية',
            info: 'معلومات العملية',
        }
    }
} }
export default {
  routine: {
    file: {
      fileName: 'اسم الملف',
      fileType: 'نوع الملف',
      fileSize: 'حجم الملف',
      filePath: 'مسار الملف',
      fileUrl: 'رابط الملف',
      fileMd5: 'ملف MD5',
      fileOriginalName: 'اسم الملف الأصلي',
      fileExtension: 'امتداد الملف',
      fileStorageType: 'نوع التخزين',
      fileStorageLocation: 'موقع التخزين',
      status: 'الحالة',
      remark: 'ملاحظة',
      createTime: 'تاريخ الإنشاء',
      updateTime: 'تاريخ التحديث',
      operation: 'إجراء',
      placeholder: {
        fileName: 'يرجى إدخال اسم الملف',
        fileType: 'يرجى إدخال نوع الملف',
        fileSize: 'يرجى إدخال حجم الملف',
        status: 'يرجى اختيار الحالة',
        remark: 'يرجى إدخال ملاحظة'
      },
      validation: {
        fileName: {
          required: 'يرجى إدخال اسم الملف',
          maxLength: 'يجب ألا يتجاوز اسم الملف 100 حرف'
        },
        fileType: {
          required: 'يرجى إدخال نوع الملف',
          maxLength: 'يجب ألا يتجاوز نوع الملف 50 حرف'
        },
        status: {
          required: 'يرجى اختيار الحالة'
        },
        file: {
          required: 'يرجى اختيار ملف للرفع',
          size: 'يجب ألا يتجاوز حجم الملف المرفوع 2 ميجابايت!'
        }
      },
      message: {
        addSuccess: 'تمت إضافة الملف بنجاح',
        editSuccess: 'تم تحديث الملف بنجاح',
        deleteSuccess: 'تم حذف الملف بنجاح',
        deleteConfirm: 'هل أنت متأكد أنك تريد حذف الملف "{name}"؟',
        exportSuccess: 'تم تصدير الملف بنجاح',
        importSuccess: 'تم استيراد الملف بنجاح',
        uploadSuccess: 'تم رفع الملف بنجاح',
        downloadSuccess: 'تم تنزيل الملف بنجاح'
      },
      detail: {
        title: 'تفاصيل الملف'
      },
      upload: {
        success: 'تم رفع الملف بنجاح',
        failed: 'فشل رفع الملف',
        fileEmpty: 'يرجى اختيار ملف للرفع',
        fileType: 'نوع الملف غير مدعوم',
        fileSize: 'تجاوز حجم الملف الحد المسموح',
        fileExists: 'الملف موجود بالفعل',
        fileNameRequired: 'اسم الملف لا يمكن أن يكون فارغًا'
      }
    }
  }
} 
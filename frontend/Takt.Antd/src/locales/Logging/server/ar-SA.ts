export default {
  logging: {
    server: {
      title: 'مراقبة الخادم',
      refresh: 'تحديث',
      refreshResult: {
        success: 'تم تحديث البيانات بنجاح',
        failed: 'تم تحديث البيانات بنجاح'
      },
      resource: {
        title: 'موارد النظام',
        cpu: 'استخدام CPU',
        memory: 'استخدام الذاكرة',
        disk: 'استخدام القرص'
      },
      system: {
        title: 'معلومات النظام',
        os: 'نظام التشغيل',
        architecture: 'المعالج',
        version: 'الإصدار',
        processor: {
          name: 'اسم المعالج',
          count: 'عدد المعالجات',
          unit: 'وحدة'
        },
        startup: {
          time: 'وقت التشغيل',
          uptime: 'وقت التشغيل'
        }
      },
      dotnet: {
        title: '运行信息',
        runtime: {
          title: 'معلومات التشغيل',
          directory: 'المجلد',
          version: 'الإصدار',
          framework: 'المنصة'
        },
        clr: {
          title: 'معلومات CLR',
          version: 'الإصدار'
        }
      },
      network: {
        title: 'معلومات الشبكة',
        adapter: 'المحاكاة',
        mac: 'عنوان MAC',
        ip:{
          address: 'عنوان IP',
          location: 'الموقع',
        },
        rate:
        {
          send: 'إرسال',
          receive: 'استقبال'
        }
      }
    }
  }
}


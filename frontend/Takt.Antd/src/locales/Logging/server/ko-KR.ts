export default {
  logging: {
    server: {
      title: '서버 모니터링',
      refresh: '새로고침',
      refreshResult: {
        success: '데이터 새로고침 성공',
        failed: '데이터 새로고침 실패'
      },
      resource: {
        title: '시스템 리소스',
        cpu: 'CPU 사용률',
        memory: '메모리 사용률',
        disk: '디스크 사용률'
      },
      system: {
        title: '시스템 정보',
        os: '운영 체제',
        architecture: '아키텍처',
        version: '버전',
        processor: {
          name: '프로세서 이름',
          count: '프로세서 수',
          unit: '개'
        },
        startup: {
          time: '시작 시간',
          uptime: '실행 시간'
        }
      },
      dotnet: {
        title: '运行信息',
        runtime: {
          title: '실행 시간 정보',
          directory: '디렉토리',
          version: '버전',
          framework: '프레임워크'
        },
        clr: {
          title: 'CLR 정보',
          version: '버전'
        }
      },
      network: {
        title: '네트워크 정보',
        adapter: '네트워크 어댑터',
        mac: 'MAC 주소',
        ip:{
          address: 'IP 주소',
          location: '위치',
        },
        rate:
        {
          send: '전송',
          receive: '수신'
        }
      }
    }
  }
}


export default {
  routine: {
    core: {
      numberrule: {
        // 페이지 제목
        title: '번호 규칙 관리',
        
        // 탭
        tabs: {
          basicInfo: '기본 정보',
          numberConfig: '번호 설정',
          advancedConfig: '고급 설정',
          otherInfo: '기타 정보'
        },

        // 필드 라벨
        fields: {
          companyCode: {
            label: '회사 코드',
            placeholder: '회사 코드를 입력하세요',
            required: '회사 코드를 입력하세요',
            length: '회사 코드 길이는 1-20자 사이여야 합니다'
          },
          numberRuleName: {
            label: '번호 규칙명',
            placeholder: '번호 규칙명을 입력하세요',
            required: '번호 규칙명을 입력하세요',
            length: '번호 규칙명 길이는 1-100자 사이여야 합니다'
          },
          numberRuleCode: {
            label: '번호 규칙 코드',
            placeholder: '번호 규칙 코드를 입력하세요',
            required: '번호 규칙 코드를 입력하세요',
            length: '번호 규칙 코드 길이는 1-50자 사이여야 합니다'
          },
          deptCode: {
            label: '부서 코드',
            placeholder: '부서 코드를 입력하세요',
            required: '부서 코드를 입력하세요',
            length: '부서 코드 길이는 1-20자 사이여야 합니다'
          },
          numberRuleShortCode: {
            label: '번호 규칙 약어',
            placeholder: '번호 규칙 약어를 입력하세요',
            required: '번호 규칙 약어를 입력하세요',
            length: '번호 규칙 약어 길이는 1-4자 사이여야 합니다'
          },
          numberRuleType: {
            label: '번호 규칙 유형',
            placeholder: '번호 규칙 유형을 선택하세요',
            required: '번호 규칙 유형을 선택하세요'
          },
          status: {
            label: '상태',
            placeholder: '상태를 선택하세요',
            required: '상태를 선택하세요'
          },
          numberRuleDescription: {
            label: '번호 규칙 설명',
            placeholder: '번호 규칙 설명을 입력하세요'
          },
          numberPrefix: {
            label: '번호 접두사',
            placeholder: '번호 접두사를 입력하세요'
          },
          numberSuffix: {
            label: '번호 접미사',
            placeholder: '번호 접미사를 입력하세요'
          },
          dateFormat: {
            label: '날짜 형식',
            placeholder: '날짜 형식을 선택하세요',
            required: '날짜 형식을 선택하세요'
          },
          sequenceLength: {
            label: '시퀀스 길이',
            placeholder: '시퀀스 길이를 입력하세요',
            required: '시퀀스 길이를 입력하세요'
          },
          sequenceStart: {
            label: '시퀀스 시작값',
            placeholder: '시퀀스 시작값을 입력하세요',
            required: '시퀀스 시작값을 입력하세요'
          },
          sequenceStep: {
            label: '시퀀스 단계',
            placeholder: '시퀀스 단계를 입력하세요',
            required: '시퀀스 단계를 입력하세요'
          },
          currentSequence: {
            label: '현재 시퀀스',
            placeholder: '현재 시퀀스를 입력하세요'
          },
          sequenceResetRule: {
            label: '시퀀스 리셋 규칙',
            placeholder: '시퀀스 리셋 규칙을 선택하세요'
          },
          includeCompanyCode: {
            label: '회사 코드 포함',
            placeholder: '회사 코드 포함 여부를 선택하세요'
          },
          includeDepartmentCode: {
            label: '부서 코드 포함',
            placeholder: '부서 코드 포함 여부를 선택하세요'
          },
          includeRevisionNumber: {
            label: '개정 번호 포함',
            placeholder: '개정 번호 포함 여부를 선택하세요'
          },
          includeYear: {
            label: '연도 포함',
            placeholder: '연도 포함 여부를 선택하세요'
          },
          includeMonth: {
            label: '월 포함',
            placeholder: '월 포함 여부를 선택하세요'
          },
          includeDay: {
            label: '일 포함',
            placeholder: '일 포함 여부를 선택하세요'
          },
          allowDuplicate: {
            label: '중복 허용',
            placeholder: '중복 허용 여부를 선택하세요'
          },
          duplicateCheckScope: {
            label: '중복 확인 범위',
            placeholder: '중복 확인 범위를 선택하세요'
          },
          orderNum: {
            label: '순서 번호',
            placeholder: '순서 번호를 입력하세요'
          }
        },

        // 액션 버튼
        actions: {
          add: '번호 규칙 추가',
          edit: '번호 규칙 편집',
          delete: '번호 규칙 삭제',
          batchDelete: '일괄 삭제',
          export: '내보내기',
          import: '가져오기',
          downloadTemplate: '템플릿 다운로드',
          preview: '미리보기',
          generate: '번호 생성',
          validate: '규칙 검증'
        },

        // 폼 플레이스홀더
        placeholder: {
          search: '번호 규칙명 또는 코드를 입력하세요',
          selectAll: '전체 선택',
          clear: '지우기'
        },

        // 작업 결과 메시지
        message: {
          success: {
            add: '번호 규칙이 성공적으로 추가되었습니다',
            edit: '번호 규칙이 성공적으로 업데이트되었습니다',
            delete: '번호 규칙이 성공적으로 삭제되었습니다',
            batchDelete: '일괄 삭제가 성공적으로 완료되었습니다',
            export: '내보내기가 성공적으로 완료되었습니다',
            import: '가져오기가 성공적으로 완료되었습니다',
            generate: '번호 생성이 성공했습니다',
            validate: '검증이 성공했습니다'
          },
          failed: {
            add: '번호 규칙 추가에 실패했습니다',
            edit: '번호 규칙 업데이트에 실패했습니다',
            delete: '번호 규칙 삭제에 실패했습니다',
            batchDelete: '일괄 삭제에 실패했습니다',
            export: '내보내기에 실패했습니다',
            import: '가져오기에 실패했습니다',
            generate: '번호 생성에 실패했습니다',
            validate: '검증에 실패했습니다'
          },
          confirm: {
            delete: '선택한 번호 규칙을 삭제하시겠습니까?',
            batchDelete: '선택한 번호 규칙들을 일괄 삭제하시겠습니까?'
          }
        },

        // 상세 페이지
        detail: {
          title: '번호 규칙 상세',
          basicInfo: '기본 정보',
          numberConfig: '번호 설정',
          advancedConfig: '고급 설정',
          otherInfo: '기타 정보'
        },

        // 테이블 열 제목
        columns: {
          numberRuleName: '번호 규칙명',
          numberRuleCode: '번호 규칙 코드',
          numberRuleShortCode: '번호 규칙 약어',
          numberRuleType: '번호 규칙 유형',
          deptCode: '부서 코드',
          status: '상태',
          createTime: '생성 시간',
          updateTime: '업데이트 시간',
          remark: '비고',
          actions: '작업'
        },

        // 번호 규칙 유형
        types: {
          daily: '일상 업무',
          hr: '인사',
          finance: '재무 회계',
          logistics: '물류 관리',
          production: '생산 관리',
          workflow: '워크플로우'
        },

        // 상태
        status: {
          normal: '정상',
          disabled: '비활성화'
        },

        // 시퀀스 리셋 규칙
        resetRules: {
          none: '리셋 없음',
          yearly: '연간 리셋',
          monthly: '월간 리셋',
          daily: '일간 리셋'
        },

        // 중복 확인 범위
        checkScopes: {
          global: '전역',
          yearly: '연간',
          monthly: '월간',
          daily: '일간'
        },

        // 날짜 형식 옵션
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}

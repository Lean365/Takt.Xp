export default {
  accounting: {
    financial: {
      company: {
        title: '회사 관리',
        tabs: {
          basicInfo: '기본 정보',
          detailInfo: '상세 정보',
          financialInfo: '재무 정보'
        },
        fields: {
          companyCode: {
            label: '회사 코드',
            placeholder: '회사 코드를 입력하세요',
            required: '회사 코드를 입력하세요'
          },
          companyName: {
            label: '회사명',
            placeholder: '회사명을 입력하세요',
            required: '회사명을 입력하세요'
          },
          companyShortName: {
            label: '회사 약칭',
            placeholder: '회사 약칭을 입력하세요'
          },
          companyTaxNumber: {
            label: '세무 번호',
            placeholder: '세무 번호를 입력하세요'
          },
          companyNature: {
            label: '기업 성격',
            placeholder: '기업 성격을 선택하세요',
            required: '기업 성격을 선택하세요'
          },
          companyIndustryType: {
            label: '업종',
            placeholder: '업종을 선택하세요'
          },
          companyCurrency: {
            label: '통화',
            placeholder: '통화를 선택하세요',
            required: '통화를 선택하세요'
          },
          companyLanguage: {
            label: '언어',
            placeholder: '언어를 선택하세요',
            required: '언어를 선택하세요'
          },
          companyPhone: {
            label: '전화번호',
            placeholder: '전화번호를 입력하세요'
          },
          companyEmail: {
            label: '이메일',
            placeholder: '이메일을 입력하세요'
          },
          companyWebsite: {
            label: '웹사이트',
            placeholder: '웹사이트를 입력하세요'
          },
          companyScale: {
            label: '회사 규모',
            placeholder: '회사 규모를 입력하세요'
          },
          companyAddress: {
            label: '주소',
            placeholder: '주소를 입력하세요'
          },
          companyName1: {
            label: '회사명1',
            placeholder: '회사명1을 입력하세요'
          },
          companyName2: {
            label: '회사명2',
            placeholder: '회사명2를 입력하세요'
          },
          companyName3: {
            label: '회사명3',
            placeholder: '회사명3을 입력하세요'
          },
          companyLegalRepresentative: {
            label: '법정대표자',
            placeholder: '법정대표자를 입력하세요'
          },
          companyRegisteredCapital: {
            label: '등록자본금',
            placeholder: '등록자본금을 입력하세요'
          },
          companyBusinessLicense: {
            label: '사업자등록증',
            placeholder: '사업자등록증 번호를 입력하세요'
          },
          companyOrganizationCode: {
            label: '조직코드',
            placeholder: '조직코드를 입력하세요'
          },
          companyUnifiedCreditCode: {
            label: '통일신용코드',
            placeholder: '통일신용코드를 입력하세요'
          },
          companyCity: {
            label: '도시',
            placeholder: '도시를 입력하세요'
          },
          companyRegion: {
            label: '지역',
            placeholder: '지역을 입력하세요'
          },
          companyPostalCode: {
            label: '우편번호',
            placeholder: '우편번호를 입력하세요'
          },
          companyCountry: {
            label: '국가',
            placeholder: '국가를 입력하세요'
          },
          companyFax: {
            label: '팩스',
            placeholder: '팩스 번호를 입력하세요'
          },
          establishmentDate: {
            label: '설립일',
            placeholder: '설립일을 선택하세요'
          },
          dissolutionDate: {
            label: '해산일',
            placeholder: '해산일을 선택하세요'
          },
          orderNum: {
            label: '순서번호',
            placeholder: '순서번호를 입력하세요'
          },
          status: {
            label: '상태',
            placeholder: '상태를 선택하세요',
            required: '상태를 선택하세요'
          },
          companyFiscalYearVariant: {
            label: '회계연도 변형',
            placeholder: '회계연도 변형을 입력하세요'
          },
          companyChartOfAccounts: {
            label: '계정과목표',
            placeholder: '계정과목표를 입력하세요'
          },
          companyFinancialManagementArea: {
            label: '재무관리 영역',
            placeholder: '재무관리 영역을 입력하세요'
          },
          companyMaxExchangeRateDeviation: {
            label: '최대환율편차',
            placeholder: '최대환율편차를 입력하세요'
          },
          companyAllocationIdentifier: {
            label: '배분식별자',
            placeholder: '배분식별자를 입력하세요'
          },
          companyRelatedCompany: {
            label: '관련회사',
            placeholder: '관련회사를 입력하세요'
          },
          companyRelatedPlant: {
            label: '관련공장',
            placeholder: '관련공장을 입력하세요'
          },
          companyAddressNumber: {
            label: '주소번호',
            placeholder: '주소번호를 입력하세요'
          },
          remark: {
            label: '비고',
            placeholder: '비고를 입력하세요'
          }
        },
        actions: {
          add: '회사 추가',
          edit: '회사 편집',
          delete: '회사 삭제',
          batchDelete: '일괄 삭제',
          export: '내보내기',
          import: '가져오기',
          downloadTemplate: '템플릿 다운로드',
          reset: '초기화',
          search: '검색'
        },
        columns: {
          companyCode: '회사 코드',
          companyName: '회사명',
          companyShortName: '회사 약칭',
          companyTaxNumber: '세무 번호',
          companyNature: '기업 성격',
          companyIndustryType: '업종',
          companyCurrency: '통화',
          companyLanguage: '언어',
          companyPhone: '전화번호',
          companyEmail: '이메일',
          companyWebsite: '웹사이트',
          companyScale: '회사 규모',
          companyAddress: '주소',
          companyName1: '회사명1',
          companyName2: '회사명2',
          companyName3: '회사명3',
          companyLegalRepresentative: '법정대표자',
          companyRegisteredCapital: '등록자본금',
          companyBusinessLicense: '사업자등록증',
          companyOrganizationCode: '조직코드',
          companyUnifiedCreditCode: '통일신용코드',
          companyCity: '도시',
          companyRegion: '지역',
          companyPostalCode: '우편번호',
          companyCountry: '국가',
          companyFax: '팩스',
          establishmentDate: '설립일',
          dissolutionDate: '해산일',
          orderNum: '순서번호',
          status: '상태',
          companyFiscalYearVariant: '회계연도 변형',
          companyChartOfAccounts: '계정과목표',
          companyFinancialManagementArea: '재무관리 영역',
          companyMaxExchangeRateDeviation: '최대환율편차',
          companyAllocationIdentifier: '배분식별자',
          companyRelatedCompany: '관련회사',
          companyRelatedPlant: '관련공장',
          companyAddressNumber: '주소번호',
          remark: '비고',
          createBy: '생성자',
          createTime: '생성시간',
          updateBy: '수정자',
          updateTime: '수정시간',
          deleteBy: '삭제자',
          deleteTime: '삭제시간'
        },
        messages: {
          addSuccess: '회사가 성공적으로 추가되었습니다',
          updateSuccess: '회사가 성공적으로 업데이트되었습니다',
          deleteSuccess: '회사가 성공적으로 삭제되었습니다',
          batchDeleteSuccess: '회사가 성공적으로 일괄 삭제되었습니다',
          deleteConfirm: '선택한 회사를 삭제하시겠습니까?',
          batchDeleteConfirm: '선택한 회사를 일괄 삭제하시겠습니까?',
          loadFailed: '데이터 로드에 실패했습니다',
          submitFailed: '제출에 실패했습니다',
          exportSuccess: '내보내기가 성공했습니다',
          importSuccess: '가져오기가 성공했습니다',
          importFailed: '가져오기에 실패했습니다',
          downloadTemplateSuccess: '템플릿 다운로드가 성공했습니다'
        },
        query: {
          companyCode: '회사 코드',
          companyName: '회사명',
          companyNature: '기업 성격',
          companyIndustryType: '업종',
          companyCurrency: '통화',
          status: '상태'
        }
      }
    }
  }
}

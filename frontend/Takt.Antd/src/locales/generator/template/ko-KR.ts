export default {
    generator: {
      template: {
        title: '코드 생성 템플릿',  
        fields: {
            templateName: '템플릿 이름',
            templateOrmType: 'ORM 프레임워크',
            templateCodeType: '코드 유형',
            templateCategory: '템플릿 분류',
            templateLanguage: '프로그래밍 언어',
            templateVersion: '버전 번호',
            fileName: '파일 이름',
            templateContent: '템플릿 내용',
            status: '상태',
            remark: '비고',
        },
        placeholder: {
            templateName: '템플릿 이름을 입력하세요',
            templateOrmType: 'ORM 프레임워크를 선택하세요',
            templateCodeType: '코드 유형을 선택하세요',
            templateCategory: '템플릿 분류를 선택하세요',
            templateLanguage: '프로그래밍 언어를 선택하세요',
            templateVersion: '버전 번호를 입력하세요',
            fileName: '파일 이름을 입력하세요',
            templateContent: '템플릿 내용을 입력하세요',
            remark: '비고를 입력하세요',
            validation: {
                required: '{field}을 입력하세요',
                length: '{field} 길이는 {length}자를 초과할 수 없습니다',
                minLength: '{field} 길이는 {min}자보다 적을 수 없습니다',
                pascalCase: '{field}는 파스칼 케이스(첫 글자 대문자, 문자만)를 사용해야 합니다'
            }
        },
        dialog: {
            create: '템플릿 추가',
            update: '템플릿 수정'
        },
        messages: {
            success: '작업이 성공했습니다',
            error: '작업이 실패했습니다',
            warning: '작업 경고',
            info: '작업 정보',
            confirm: '확인',
        },
    }
} }
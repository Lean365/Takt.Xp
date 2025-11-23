export default {
    generator: {
      config: {
        title: '코드 생성 설정',  
        fields: {
            genConfigName: '설정 이름',
            author: '작성자',
            moduleName: '모듈 이름',
            projectName: '프로젝트 이름',
            businessName: '비즈니스 이름',
            functionName: '기능 이름',
            genMethod: '생성 방식',
            genTplType: '템플릿 유형',
            genPath: '경로',
            options: '옵션',
            status: '상태',
            dateRange: '날짜 범위',
        },
        placeholder: {
            genConfigName: '설정 이름을 입력하세요',
            author: '작성자를 입력하세요',
            moduleName: '모듈 이름을 입력하세요',
            projectName: '프로젝트 이름을 입력하세요',
            businessName: '비즈니스 이름을 입력하세요',
            functionName: '기능 이름을 입력하세요',
            genMethod: '생성 방식을 선택하세요',
            genTplType: '템플릿 유형을 선택하세요',
            genPath: '경로를 입력하세요',
            options: '옵션을 입력하세요',
            status: '상태를 선택하세요',
            dateRange: '날짜 범위를 선택하세요',
            validation: {
                length: '{{field}} 길이는 {{length}}을 초과할 수 없습니다',
                required: '{{field}}을 입력하세요',
                minlength: '{{field}} 길이는 {{min}}보다 작을 수 없습니다',
                maxlength: '{{field}} 길이는 {{max}}보다 클 수 없습니다',
                pattern: '{{field}} 형식이 올바르지 않습니다',
            }
        },
        messages: {
            success: '작업이 성공했습니다',
            error: '작업이 실패했습니다',
            warning: '작업 경고',
            info: '작업 정보',
        }
    }
} }
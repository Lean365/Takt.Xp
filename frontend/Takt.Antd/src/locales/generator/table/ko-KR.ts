export default {
  generator: {
    table: {
      title: '코드 생성',
      list: {
        title: '코드 생성 목록',
        search: {
          name: '테이블명',
          comment: '테이블 설명'
        },
        table: {
          tableId: '테이블 ID',
          databaseName: '데이터베이스명',
          tableName: '테이블명',
          tableComment: '테이블 설명',
          className: '클래스명',
          namespace: '네임스페이스',
          baseNamespace: '기본 네임스페이스',
          csharpTypeName: 'C# 타입명',
          parentTableName: '부모 테이블명',
          parentTableFkName: '부모 테이블 외래키',
          status: '상태',
          templateType: '템플릿 유형',
          moduleName: '모듈명',
          businessName: '비즈니스명',
          functionName: '기능명',
          author: '작성자',
          genMode: '생성 모드',
          genPath: '생성 경로',
          options: '옵션',
          createBy: '생성자',
          createTime: '생성 시간',
          updateBy: '수정자',
          updateTime: '수정 시간',
          remark: '비고',
          isDeleted: '삭제됨'
        },
        actions: {
          create: '신규',
          edit: '수정',
          delete: '삭제',
          view: '보기',
          generate: '코드 생성',
          sync: '테이블 동기화',
          import: '가져오기',
          export: '내보내기',
          template: '템플릿 다운로드',
          refresh: '새로고침'
        },
        status: {
          enabled: '활성화',
          disabled: '비활성화'
        }
      },
      form: {
        title: '코드 생성 양식',
        tab: {
          basic: '기본 정보',
          generate: '생성 정보',
          field: '필드 정보'
        },
        basic: {
          title: '기본 정보',
          tableName: '테이블명',
          tableComment: '테이블 설명',
          className: '클래스명',
          namespace: '네임스페이스',
          baseNamespace: '기본 네임스페이스',
          csharpTypeName: 'C# 타입명',
          parentTableName: '부모 테이블명',
          parentTableFkName: '부모 테이블 외래키',
          status: '상태',
          author: '작성자',
          remark: '비고'
        },
        generate: {
          title: '생성 정보',
          moduleName: '모듈명',
          packageName: '패키지 경로',
          businessName: '비즈니스명',
          functionName: '기능명',
          parentMenuId: '상위 메뉴',
          tplCategory: '템플릿 유형',
          genPath: '생성 경로',
          options: '생성 옵션',
          tplCategoryOptions: {
            crud: '단일 테이블 (CRUD)',
            tree: '트리 테이블 (CRUD)',
            sub: '마스터-디테일 테이블 (CRUD)'
          },
          optionsItems: {
            treeCode: '트리 코드 필드',
            treeParentCode: '트리 부모 코드 필드',
            treeName: '트리 이름 필드',
            parentMenuId: '상위 메뉴',
            query: '조회',
            add: '추가',
            edit: '수정',
            delete: '삭제',
            import: '가져오기',
            export: '내보내기'
          }
        },
        field: {
          title: '필드 정보',
          columnName: '컬럼명',
          columnComment: '컬럼 설명',
          columnType: '컬럼 유형',
          csharpType: 'C# 타입',
          csharpField: 'C# 필드',
          isRequired: '필수',
          isInsert: '삽입',
          isEdit: '수정',
          isList: '목록',
          isQuery: '조회',
          queryType: '조회 유형',
          htmlType: '표시 유형',
          dictType: '사전 유형',
          queryTypeOptions: {
            EQ: '같음',
            NE: '같지 않음',
            GT: '보다 큼',
            GTE: '보다 크거나 같음',
            LT: '보다 작음',
            LTE: '보다 작거나 같음',
            LIKE: '포함',
            BETWEEN: '범위',
            IN: '포함됨'
          },
          htmlTypeOptions: {
            input: '입력 필드',
            textarea: '텍스트 영역',
            select: '드롭다운',
            radio: '라디오 버튼',
            checkbox: '체크박스',
            datetime: '날짜 시간',
            imageUpload: '이미지 업로드',
            fileUpload: '파일 업로드',
            editor: '리치 텍스트 에디터'
          }
        },
        buttons: {
          submit: '제출',
          cancel: '취소'
        },
        name: '테이블명',
        comment: '테이블 설명',
        className: '클래스명',
        namespace: '네임스페이스',
        baseNamespace: '기본 네임스페이스',
        csharpTypeName: 'C# 타입명',
        parentTableName: '부모 테이블명',
        parentTableFkName: '부모 테이블 외래키',
        moduleName: '모듈명',
        businessName: '비즈니스명',
        functionName: '기능명',
        author: '작성자',
        genMode: '생성 모드',
        genPath: '생성 경로',
        options: '옵션'
      },
      detail: {
        title: '코드 생성 상세',
        basic: {
          title: '기본 정보',
          tableName: '테이블명',
          tableComment: '테이블 설명',
          className: '클래스명',
          namespace: '네임스페이스',
          baseNamespace: '기본 네임스페이스',
          csharpTypeName: 'C# 타입명',
          parentTableName: '부모 테이블명',
          parentTableFkName: '부모 테이블 외래키',
          status: '상태',
          createTime: '생성 시간',
          updateTime: '수정 시간'
        },
        generate: {
          title: '생성 정보',
          moduleName: '모듈명',
          packageName: '패키지 경로',
          businessName: '비즈니스명',
          functionName: '기능명',
          parentMenuId: '상위 메뉴',
          tplCategory: '템플릿 유형',
          genPath: '생성 경로',
          options: '생성 옵션'
        },
        field: {
          title: '필드 정보',
          columnName: '컬럼명',
          columnComment: '컬럼 설명',
          columnType: '컬럼 유형',
          csharpType: 'C# 타입',
          csharpField: 'C# 필드',
          isRequired: '필수',
          isInsert: '삽입',
          isEdit: '수정',
          isList: '목록',
          isQuery: '조회',
          queryType: '조회 유형',
          htmlType: '표시 유형',
          dictType: '사전 유형'
        },
        actions: {
          edit: '수정',
          back: '뒤로'
        },
        columnInfo: '컬럼 정보',
        javaType: 'Java 타입',
        javaField: 'Java 필드',
        yes: '예',
        no: '아니오'
      },
      name: '테이블명',
      comment: '테이블 설명',
      databaseName: '데이터베이스명',
      className: '클래스명',
      namespace: '네임스페이스',
      baseNamespace: '기본 네임스페이스',
      csharpTypeName: 'C# 타입명',
      parentTableName: '부모 테이블명',
      parentTableFkName: '부모 테이블 외래키',
      status: '상태',
      templateType: '템플릿 유형',
      moduleName: '모듈명',
      businessName: '비즈니스명',
      functionName: '기능명',
      author: '작성자',
      genMode: '생성 모드',
      genPath: '생성 경로',
      options: '옵션',
      createBy: '생성자',
      createTime: '생성 시간',
      updateBy: '수정자',
      updateTime: '수정 시간',
      remark: '비고',
      isDeleted: '삭제됨',
      placeholder: {
        name: '테이블명을 입력하세요',
        comment: '테이블 설명을 입력하세요'
      },
      preview: {
        title: '코드 미리보기',
        copy: '코드 복사',
        download: '코드 다운로드',
        showLineNumbers: '줄 번호 표시',
        hideLineNumbers: '줄 번호 숨기기',
        copySuccess: '복사 성공',
        copyFailed: '복사 실패',
        downloadSuccess: '다운로드 성공',
        downloadFailed: '다운로드 실패',
        tab: {
          java: 'Java 코드',
          vue: 'Vue 코드',
          sql: 'SQL 코드',
          domain: '엔티티 클래스',
          mapper: 'Mapper 인터페이스',
          mapperXml: 'Mapper XML',
          service: '서비스 인터페이스',
          serviceImpl: '서비스 구현',
          controller: '컨트롤러',
          api: 'API 인터페이스',
          index: '목록 페이지',
          form: '양식 페이지'
        },
        entities: {
          title: '엔티티 클래스 코드'
        },
        services: {
          title: '서비스 인터페이스 코드'
        },
        controllers: {
          title: '컨트롤러 코드'
        },
        vue: {
          title: 'Vue 코드'
        },
        dtos: {
          title: 'DTO 코드'
        },
        types: {
          title: '타입 정의 코드'
        },
        locales: {
          title: '번역 코드'
        }
      },
      import: {
        title: '테이블 가져오기',
        database: '데이터베이스',
        table: {
          name: '테이블명',
          comment: '테이블 설명',
          action: '작업'
        },
        column: {
          title: '컬럼 가져오기',
          tableName: '테이블명',
          tableId: '테이블 ID',
          columnName: '컬럼명',
          propertyName: '속성명',
          columnType: '컬럼 유형',
          propertyType: '속성 유형',
          isNullable: 'NULL 허용',
          isPrimaryKey: '기본키',
          isAutoIncrement: '자동 증가',
          defaultValue: '기본값',
          columnComment: '컬럼 설명',
          value: '값',
          decimalDigits: '소수점 자릿수',
          scale: '스케일',
          isArray: '배열',
          isJson: 'Json',
          isUnsigned: '부호 없음',
          createTableFieldSort: '테이블 필드 정렬',
          insertServerTime: '서버 삽입 시간',
          insertSql: '삽입 SQL',
          updateServerTime: '서버 수정 시간',
          updateSql: '수정 SQL',
          sqlParameterDbType: 'SQL 매개변수 DB 유형'
        }
      },
      message: {
        generateSuccess: '코드 생성 성공',
        generateFailed: '코드 생성 실패',
        syncSuccess: '테이블 동기화 성공',
        syncFailed: '테이블 동기화 실패',
        importSuccess: '가져오기 성공',
        importFailed: '가져오기 실패',
        exportSuccess: '내보내기 성공',
        exportFailed: '내보내기 실패',
        templateSuccess: '템플릿 다운로드 성공',
        templateFailed: '템플릿 다운로드 실패',
        selectDatabase: '먼저 데이터베이스를 선택하세요',
        selectTable: '가져올 테이블을 선택하세요',
        tableNameRequired: '테이블명은 필수입니다',
        importTimeout: '가져오기 시간 초과, 나중에 다시 시도하세요'
      },
      tab: {
        basic: '기본 정보',
        generate: '생성 정보',
        field: '필드 정보'
      },
      required: {
        name: '테이블명을 입력하세요',
        comment: '테이블 설명을 입력하세요',
        className: '클래스명을 입력하세요',
        namespace: '네임스페이스를 입력하세요',
        baseNamespace: '기본 네임스페이스를 입력하세요',
        csharpTypeName: 'C# 타입명을 입력하세요',
        moduleName: '모듈명을 입력하세요',
        businessName: '비즈니스명을 입력하세요',
        functionName: '기능명을 입력하세요',
        author: '작성자명을 입력하세요',
        genMode: '생성 모드를 선택하세요',
        genPath: '생성 경로를 입력하세요'
      }
    }
  }
} 
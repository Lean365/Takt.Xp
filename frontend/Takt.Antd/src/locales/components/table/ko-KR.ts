export default {
  table: {
    viewMode: {
      normal: '전통 테이블',
      transpose: '전치 테이블'
    },
    columns: {
      id: 'ID',
      remark: '비고',
      createBy: '생성자',
      createTime: '생성 시간',
      updateBy: '수정자',
      updateTime: '수정 시간',
      deleteBy: '삭제자',
      deleteTime: '삭제 시간',
      isDeleted: '삭제됨',
      operation: '작업',
    },
    config: {
      density: {
        default: '기본값',
        middle: '중간',
        small: '컴팩트'
      },
      columnSetting: '열 설정'
    },
    pagination: {
      total: '총 {total} 항목',
      current: '{current} 페이지',
      pageSize: '페이지당 {pageSize} 항목',
      jump: '이동'
    },
    empty: '데이터 없음',
    loading: '로딩 중...',
    selectAll: '전체 선택',
    selected: '{total} 항목 선택됨'
  }
}
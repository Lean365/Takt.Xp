export default {
  signalr: {
    online: {
      title: 'オンラインユーザー',
      refresh: '更新',
      refreshResult: {
        success: 'データの更新に成功しました',
        failed: 'データの更新に失敗しました'
      },
      table: {
        username: 'ユーザー名',
        nickname: 'ニックネーム',
        loginTime: 'ログイン時間',
        loginIp: 'IPアドレス',
        location: '場所',
        browser: 'ブラウザ',
        os: 'OS',
        status: '状態',
        operation: '操作'
      },
      status: {
        online: 'オンライン',
        offline: 'オフライン',
        busy: '取り込み中',
        away: '離席中'
      },
      operation: {
        message: 'メッセージ送信',
        forceLogout: '強制ログアウト'
      },
      empty: 'オンラインユーザーはいません'
    }
  }
} 
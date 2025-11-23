export default {
  signalr: {
    online: {
      title: 'Online User Management',
      
      // Field definitions
      fields: {
        userId: {
          label: 'User ID',
          placeholder: 'Please enter user ID'
        },
        startTime: {
          label: 'Start Time',
          placeholder: 'Please select start time'
        },
        endTime: {
          label: 'End Time',
          placeholder: 'Please select end time'
        },
        onlineStatus: {
          label: 'Online Status',
          placeholder: 'Please select online status',
          options: {
            online: 'Online',
            offline: 'Offline'
          }
        },
        // Table column fields (page specific)
        userName: {
          label: 'Username'
        },
        nickName: {
          label: 'Nickname'
        },
        connectionId: {
          label: 'Connection ID'
        },
        deviceId: {
          label: 'Device ID'
        },
        ipAddress: {
          label: 'Client IP'
        },
        userAgent: {
          label: 'User Agent'
        },
        lastActivity: {
          label: 'Last Activity'
        },
        deviceType: {
          label: 'Device Type',
          options: {
            pc: 'PC',
            mobile: 'Mobile',
            tablet: 'Tablet',
            other: 'Other',
            unknown: 'Unknown'
          }
        },
        deviceName: {
          label: 'Device Name'
        },
        deviceModel: {
          label: 'Device Model'
        },
        osType: {
          label: 'Operating System',
          options: {
            windows: 'Windows',
            macos: 'macOS',
            linux: 'Linux',
            android: 'Android',
            ios: 'iOS',
            other: 'Other',
            unknown: 'Unknown'
          }
        },
        osVersion: {
          label: 'OS Version'
        },
        browserType: {
          label: 'Browser',
          options: {
            chrome: 'Chrome',
            firefox: 'Firefox',
            safari: 'Safari',
            edge: 'Edge',
            ie: 'IE',
            other: 'Other',
            unknown: 'Unknown'
          }
        },
        browserVersion: {
          label: 'Browser Version'
        },
        resolution: {
          label: 'Resolution'
        },
        deviceMemory: {
          label: 'Device Memory'
        },
        webGLRenderer: {
          label: 'WebGL Renderer'
        },
        location: {
          label: 'Location'
        },
        deviceFingerprint: {
          label: 'Device Fingerprint'
        },
        timezone: {
          label: 'Timezone'
        },
        language: {
          label: 'Language'
        },
        remark: {
          label: 'Remark'
        }
      },

      // Actions
      actions: {
        refresh: 'Refresh',
        forceLogout: 'Force Logout',
        batchLogout: 'Batch Logout',
        sendMessage: 'Send Message',
        export: 'Export Data'
      },

      // Messages
      messages: {
        confirmForceLogout: 'Are you sure to force logout this user?',
        confirmBatchLogout: 'Are you sure to batch logout selected users?',
        forceLogoutSuccess: 'Force logout successful',
        forceLogoutFailed: 'Force logout failed',
        batchLogoutSuccess: 'Batch logout successful',
        batchLogoutFailed: 'Batch logout failed',
        selectUsersFirst: 'Please select users to logout',
        forceLogoutWarning: 'You have been forced to logout, please login again',
        sendMessageSuccess: 'Message sent successfully',
        exportSuccess: 'Export successful',
        exportFailed: 'Export failed',
        refreshSuccess: 'Data refreshed successfully',
        refreshFailed: 'Data refresh failed'
      },

      // Column settings
      columnSetting: {
        title: 'Column Settings',
        item: 'Column Setting Item'
      },

      // Fullscreen
      fullscreen: {
        enter: 'Enter Fullscreen',
        exit: 'Exit Fullscreen'
      },

      // Empty state
      empty: 'No online users'
    }
  }
} 
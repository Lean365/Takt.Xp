export default {
  identity: {
    user: {
      title: 'User Management',
      profile: 'Personal Information',
      changePasswordTitle: 'Change Password',
      
      // Password related
      password: {
        old: {
          label: 'Old Password',
          placeholder: 'Please enter old password',
          validation: {
            required: 'Old password cannot be empty',
          }
        },
        new: {
          label: 'New Password',
          placeholder: 'Please enter new password',
          validation: {
            required: 'New password cannot be empty',
          }
        },
        confirm: {
          label: 'Confirm Password',
          placeholder: 'Please enter confirm password',
          validation: {
            required: 'Confirm password cannot be empty',
          }
        },
      },
      tabs: {
        userInfo: 'User Information',
        organization: 'Organization Relations',
        avatar: 'Avatar'
      },
      // Form field definitions
      fields: {
        userId: {
          label: 'User ID'
        },
        userName: {
          label: 'Username',
          placeholder: 'Please enter username',
          validation: {
            required: 'Username cannot be empty',
            format: 'Must start with lowercase letter, only contain lowercase letters, numbers and hyphens, no dots or underscores, length between 4-50 characters'
          }
        },
        nickName: {
          label: 'Nickname',
          placeholder: 'Please enter nickname',
          validation: {
            required: 'Nickname cannot be empty',
            format: '2-50 characters, allow Chinese, Japanese, Korean, English, numbers, English periods and hyphens, no underscores or spaces'
          }
        },
        realName: {
          label: 'Real Name',
          placeholder: 'Please enter real name',
          validation: {
            required: 'Real name cannot be empty',
            format: 'Real name length must be between 2-20 characters, only contain Chinese, English, numbers and underscores'
          }
        },
        fullName: {
          label: 'Full Name',
          placeholder: 'Please enter full name',
          validation: {
            required: 'Full name cannot be empty',
            format: 'Full name length must be between 2-20 characters, only contain Chinese, English, numbers and underscores'
          }
        },
        englishName: {
          label: 'English Name',
          placeholder: 'Please enter English name',
          validation: {
            required: 'English name cannot be empty',
            format: 'English name length must be between 2-100 characters, must start with letter, only contain English letters, spaces, hyphens and English periods'
          }
        },
        password: {
          label: 'Password',
          placeholder: 'Please enter password',
          validation: {
            required: 'Password cannot be empty',
            format: 'Password length must be between 6-20 characters, only contain English letters, numbers and special characters'
          }
        },
        userType: {
          label: 'User Type',
          placeholder: 'Please select user type',
          options: {
            admin: 'Administrator',
            user: 'Regular User'
          }
        },
        email: {
          label: 'Email',
          placeholder: 'Please enter email',
          validation: {
            required: 'Email cannot be empty',
            invalid: 'Email length must be between 6-100 characters and in correct format'
          }
        },
        phoneNumber: {
          label: 'Phone Number',
          placeholder: 'Please enter phone number',
          validation: {
            required: 'Phone number cannot be empty',
            invalid: 'Please enter correct mobile or landline phone number format'
          }
        },
        gender: {
          label: 'Gender',
          placeholder: 'Please select gender',
          options: {
            male: 'Male',
            female: 'Female',
            unknown: 'Unknown'
          }
        },
        avatar: {
          label: 'Avatar',
          upload: 'Upload Avatar',
          currentAvatar: 'Current Avatar',
          avatarUpload: 'Avatar Upload',
          uploadSuccess: 'Avatar uploaded successfully',
          uploadError: 'Avatar upload failed',
          uploading: 'Uploading avatar...',
          onlyImage: 'Only image files can be uploaded!',
          sizeLimit: 'Image size cannot exceed 5MB!'
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled'
          }
        },
        lastPasswordChangeTime: {
          label: 'Last Password Change Time'
        },
        lockEndTime: {
          label: 'Lock End Time'
        },
        lockReason: {
          label: 'Lock Reason'
        },
        isLock: {
          label: 'Is Locked'
        },
        errorLimit: {
          label: 'Error Count Limit'
        },
        loginCount: {
          label: 'Login Count'
        },
        deptName: {
          label: 'Department',
          placeholder: 'Please select department',
          validation: {
            required: 'Department cannot be empty'
          }
        },
        role: {
          label: 'Role',
          placeholder: 'Please select role',
          validation: {
            required: 'Role cannot be empty'
          }
        },
        post: {
          label: 'Position',
          placeholder: 'Please select position',
          validation: {
            required: 'Position cannot be empty'
          }
        },
        remark: {
          label: 'Remarks',
          placeholder: 'Please enter remarks'
        }
      },

      // Action buttons
      actions: {
        add: 'Add User',
        edit: 'Edit User',
        'delete': 'Delete User',
        resetPassword: 'Reset Password',
        export: 'Export Users'
      },

      // Message prompts
      messages: {
        confirmDelete: 'Are you sure you want to delete the selected user?',
        confirmResetPassword: 'Are you sure you want to reset the password for the selected user?',
        confirmToggleStatus: 'Are you sure you want to {action} this user?',
        deleteSuccess: 'User deleted successfully',
        deleteFailed: 'User deletion failed',
        saveSuccess: 'User information saved successfully',
        saveFailed: 'User information save failed',
        resetPasswordSuccess: 'Password reset successfully',
        resetPasswordFailed: 'Password reset failed',
        importSuccess: 'User import successful',
        importFailed: 'User import failed',
        exportSuccess: 'User export successful',
        exportFailed: 'User export failed',
        toggleStatusSuccess: 'Status changed successfully',
        toggleStatusFailed: 'Status change failed',
        cannotDeleteAdmin: 'Administrative users cannot be deleted!',
        cannotEditAdmin: 'Administrative users cannot be edited!',
        cannotUpdateAdminStatus: 'Administrative user status cannot be changed!',
        cannotResetAdminPassword: 'Administrative user password cannot be reset!',
        cannotUnlockAdmin: 'Administrative users cannot be unlocked!',
        cannotAllocateRole: 'Administrative users cannot be assigned roles!',
        cannotAllocateDept: 'Administrative users cannot be assigned departments!',
        cannotAllocatePost: 'Administrative users cannot be assigned positions!',
        statusUpdateSuccess: 'Status updated successfully',
        statusUpdateFailed: 'Status update failed',
        lockStatusUpdateSuccess: 'Lock status updated successfully',
        lockStatusUpdateFailed: 'Lock status update failed'
      },

      // Table display text
      table: {
        notLocked: 'Not Locked',
        none: 'None',
        queryParams: 'Query Parameters',
        importResponseData: 'Import Response Data',
        parsedData: 'Parsed Data',
        exportFailed: 'Export Failed',
        downloadTemplateFailed: 'Download Template Failed',
        rowClicked: 'Row Clicked',
        toggleFullscreenState: 'Toggle Fullscreen State',
        getOptionsFailed: 'Get Options Failed',
        createUser: 'Create User',
        updateUser: 'Update User'
      },

      // Import tips
      importTips: 'Excel worksheet name must be "User"',

      // Tabs
      tab: {
        basic: 'Basic Information',
        profile: 'Personal Profile',
        role: 'Role Permissions',
        dept: 'Department Position',
        other: 'Other Information',
        avatar: 'Avatar Settings',
        loginLog: 'Login Log',
        operateLog: 'Operation Log',
        errorLog: 'Exception Log',
        taskLog: 'Task Log'
      },

      // Import/Export
      import: {
        title: 'Import Users',
        template: 'Download Template',
        success: 'Import Successful',
        failed: 'Import Failed',
        fileName: 'User Data'
      },
      export: {
        title: 'Export Users',
        fileName: 'User Data',
        success: 'Export Successful',
        failed: 'Export Failed'
      },
      template: {
        fileName: 'User Import Template',
        downloadFailed: 'Download Template Failed'
      },

      // Reset password
      resetPwd: 'Reset Password',
      
      // Change password
      changePassword: {
        success: 'Password changed successfully',
        failed: 'Password change failed, please try again',
        changeFailed: 'Change Password Failed'
      },

      // Allocation related
      allocateDept: 'Assign Department',
      allocatePost: 'Assign Position',
      allocateRole: 'Assign Role',
      
      roleAllocate: {
        unallocated: 'Unallocated',
        allocated: 'Allocated',
        loadRoleListFailed: 'Load Role List Failed',
        startLoadUserRoles: 'Start Loading User Roles',
        userRolesApiResponse: 'User Roles API Response',
        setSelectedRoles: 'Set Selected Roles',
        loadUserRolesFailed: 'Load User Roles Failed'
      },
      
      postAllocate: {
        unallocated: 'Unallocated',
        allocated: 'Allocated',
        loadPostListFailed: 'Load Post List Failed',
        loadUserPostsFailed: 'Load User Posts Failed'
      }
    }
  }
}

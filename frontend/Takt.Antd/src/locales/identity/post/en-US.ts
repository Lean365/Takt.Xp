export default {
  identity: {
    post: {
      title: 'Post Management',
      fields: {
        postId: {
          label: 'Post ID'
        },
        postCode: {
          label: 'Post Code',
          placeholder: 'Please enter post code',
          validation: {
            required: 'Post code cannot be empty',
            length: 'Post code length must be between 2-20 characters'
          }
        },
        postName: {
          label: 'Post Name',
          placeholder: 'Please enter post name',
          validation: {
            required: 'Post name cannot be empty',
            length: 'Post name length must be between 2-20 characters'
          }
        },
        postSort: {
          label: 'Display Order',
          placeholder: 'Please enter display order'
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled'
          }
        },
        description: {
          label: 'Description',
          placeholder: 'Please enter description'
        },
        createTime: 'Create Time'
      },
      actions: {
        add: 'Add Post',
        edit: 'Edit Post',
        delete: 'Delete Post',
        export: 'Export Posts'
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the post with code "{code}"?',
        deleteSuccess: 'Post deleted successfully',
        deleteFailed: 'Failed to delete post',
        saveSuccess: 'Post information saved successfully',
        saveFailed: 'Failed to save post information'
      }
    }
  }
} 
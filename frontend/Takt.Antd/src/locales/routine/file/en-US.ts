export default {
  routine: {
    file: {
      // Basic information
      fileName: 'File Name',
      fileType: 'File Type',
      fileSize: 'File Size',
      filePath: 'File Path',
      fileUrl: 'File URL',
      fileMd5: 'File MD5',
      fileOriginalName: 'Original File Name',
      fileExtension: 'File Extension',
      fileStorageType: 'Storage Type',
      fileStorageLocation: 'Storage Location',
      status: 'Status',
      remark: 'Remark',
      // Time information
      createTime: 'Created At',
      updateTime: 'Updated At',
      // Operations
      operation: 'Operation',
      // Placeholders
      placeholder: {
        fileName: 'Please enter file name',
        fileType: 'Please enter file type',
        fileSize: 'Please enter file size',
        status: 'Please select status',
        remark: 'Please enter remark'
      },
      // Validation messages
      validation: {
        fileName: {
          required: 'Please enter file name',
          maxLength: 'File name cannot exceed 100 characters'
        },
        fileType: {
          required: 'Please enter file type',
          maxLength: 'File type cannot exceed 50 characters'
        },
        status: {
          required: 'Please select status'
        },
        file: {
          required: 'Please select a file to upload',
          size: 'Uploaded file size cannot exceed 2MB!'
        }
      },
      // Operation results
      message: {
        addSuccess: 'File added successfully',
        editSuccess: 'File updated successfully',
        deleteSuccess: 'File deleted successfully',
        deleteConfirm: 'Are you sure you want to delete the file "{name}"?',
        exportSuccess: 'File exported successfully',
        importSuccess: 'File imported successfully',
        uploadSuccess: 'File uploaded successfully',
        downloadSuccess: 'File downloaded successfully'
      },
      // Details
      detail: {
        title: 'File Details'
      },
      upload: {
        success: 'File uploaded successfully',
        failed: 'File upload failed',
        fileEmpty: 'Please select a file to upload',
        fileType: 'Unsupported file type',
        fileSize: 'File size exceeds the limit',
        fileExists: 'File already exists',
        fileNameRequired: 'File name cannot be empty'
      }
    }
  }
} 
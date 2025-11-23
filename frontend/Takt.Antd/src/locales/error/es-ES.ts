export default {
  error: {
    404: {
      title: 'Page Not Found',
      description: 'Sorry, the page you visited does not exist'
    },
    403: {
      title: 'Access Denied',
      description: 'Sorry, you do not have permission to access this page'
    },
    401: {
      title: 'Unauthorized',
      description: 'Sorry, you need to log in to access this page'
    },
    500: {
      title: 'Server Error',
      description: 'Sorry, something went wrong on our server'
    },
    503: {
      title: 'Service Unavailable',
      description: 'Sorry, the service is temporarily unavailable. Please try again later'
    },
    actions: {
      back: 'Go Back',
      home: 'Go Home',
      retry: 'Retry',
      login: 'Log In'
    }
  }
} 
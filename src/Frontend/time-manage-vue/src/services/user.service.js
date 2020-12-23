import router from '../router'

export const userService = {
  login,
  logout
}

function login (username, password) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password })
  }

  return fetch(`${process.env.VUE_APP_ROOT_API}/api/authentication/login`, requestOptions)
    .then(handleResponse)
    .then(user => {
      if (user.token) {
        getCurrentUserOverview(user.token)
          .then(overview => {
            var userData = {
              firstName: overview.firstName,
              lastName: overview.lastName,
              email: overview.email,
              username: overview.username,
              token: user.token
            }
            localStorage.setItem('user', JSON.stringify(userData))
            router.push('/')
          })
      }
      return user
    })
}
function logout () {
  localStorage.removeItem('user')
  router.push('/login')
}

function getCurrentUserOverview (token) {
  const requestOptions = {
    method: 'GET',
    headers: { Authorization: 'Bearer ' + token }
  }
  return fetch(`${process.env.VUE_APP_ROOT_API}/api/user/overview`, requestOptions)
    .then(handleResponse)
}

function handleResponse (response) {
  return response.text().then(t => {
    const data = t && JSON.parse(t)
    if (!response.ok) {
      if (response.status === 401) {
        logout()
        location.reload()
      }
      const error = (data & data.message) || response.statusText
      return Promise.reject(error)
    }
    return data
  })
}

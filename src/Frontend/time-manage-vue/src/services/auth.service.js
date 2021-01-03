export const authService = {
    login,
    logout,
    getAuthHeader,
    handleApiResponse
}
function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    }

    return fetch(`${process.env.VUE_APP_ROOT_API}/api/authentication/login`, requestOptions)
        .then(handleApiResponse)
        .then(user => {
            if (user.token) {
                localStorage.setItem('user', JSON.stringify({ token: user.token }))
                getCurrentUserOverview()
                    .then(overview => {
                        var userData = {
                            firstName: overview.firstName,
                            lastName: overview.lastName,
                            email: overview.email,
                            username: overview.username,
                            token: user.token
                        }
                        localStorage.setItem('user', JSON.stringify(userData))
                    })
            }
            return user
        },
        error => {
            return Promise.reject(error)
        })
}
function logout() {
    localStorage.removeItem('user')
}

function getAuthHeader() {
    const user = JSON.parse(localStorage.getItem('user'))

    if (user && user.token) {
        return { Authorization: 'Bearer ' + user.token, 'Content-Type': 'application/json' }
    } else {
        return {}
    }
}

function getCurrentUserOverview() {
    const requestOptions = {
        method: 'GET',
        headers: getAuthHeader()
    }
    return fetch(`${process.env.VUE_APP_ROOT_API}/api/user/overview`, requestOptions)
        .then(handleApiResponse)
}

function handleApiResponse(response) {
    return response.text().then(t => {
        const data = t && JSON.parse(t)
        if (!response.ok) {
            if (response.status === 401) {
                logout()
            }
            const error = data ? data.message : response.statusText
            return Promise.reject(error)
        }
        return data
    })
}
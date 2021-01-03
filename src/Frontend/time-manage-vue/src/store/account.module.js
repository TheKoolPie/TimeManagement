import { authService } from '../services/auth.service'

const user = JSON.parse(localStorage.getItem('user'))
const state = user ?
  { status: { loggedIn: true }, user } :
  { status: { loggedIn: false }, user: null }

const actions = {
  login({ dispatch, commit }, { username, password }) {
    authService.login(username, password)
      .then(
        user => {
          commit('loginSuccess', user)
        },
        error => {
          commit('loginFailure', error)
        }
      )
  },
  logout({ commit }) {
    authService.logout()
    commit('logout')
  }
}
const mutations = {
  loginSuccess(state, user) {
    state.status = { loggedIn: true }
    state.user = user
  },
  loginFailure(state) {
    state.status = { loggedIn: false }
    state.user = null
  },
  logout(state) {
    state.status = { loggedIn: false }
    state.user = null
  }
}

export const account = {
  namespaced: true,
  state,
  actions,
  mutations
}

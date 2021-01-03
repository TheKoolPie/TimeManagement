const state = {
  isSuccessful: false,
  type: null,
  message: null
}

const actions = {
  success({ commit }, message) {
    commit('success', message)
  },
  error({ commit }, message) {
    commit('error', message)
  },
  clear({ commit }) {
    commit('clear')
  }
}

const mutations = {
  success(state, message) {
    state.isSuccessful = true
    state.type = 'success'
    state.message = message
  },
  error(state, message) {
    state.isSuccessful = false
    state.type = 'error'
    state.message = message
  },
  clear(state) {
    state.isSuccessful = false
    state.type = null
    state.message = null
  }
}

export const alert = {
  namespaced: true,
  state,
  actions,
  mutations
}

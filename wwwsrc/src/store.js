import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    userKeeps: [],
    publicKeeps: [],
    activeKeep: {},
    userVaults: [],
    activeVault: {},
    vaultKeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
    },
    setUserKeeps(state, data) {
      state.userKeeps = data
    },
    setPublicKeeps(state, data) {
      state.publicKeeps = data
    },
    setActiveKeep(state, data) {
      state.activeKeep = data
    },
    setVaults(state, data) {
      state.userVaults = data
    },
    setActiveVault(state, data) {
      state.activeVault = data
    },
    setVaultKeeps(state, data) {
      state.vaultKeeps = data
    }

  },

//SECTION ====================== AUTH ======================
//NOTE Already provided by CW

  actions: {
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },

//SECTION ====================== KEEPS =========================
//NOTE per visualization requirements. Referenced Vue-Hackathon.

//NOTE GET/VIEW ALL KEEPS, NO LOGIN NEEDED
    async getPublicKeeps({ commit, dispatch }) {
      try {
        let res = await api.get('keeps/')
        commit("setPublicKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },

//NOTE GET ONE KEEP BY ID
    async getKeepsById({ commit, dispatch }, payload) {
      try {
        let res = await api.get('keeps/' + payload) //payload is the keep id
        commit("setActiveKeep", res.data)
        dispatch('getVaultByUserId')
      } catch (error) {
        console.error(error)
      }
    },

    // setActiveKeep({ commit }, payload) {
    //   commit("setActiveKeep", payload)
    // },

//NOTE GET BY USER ID
    async getKeepsByUserId({ commit, dispatch }) {
      try {
        let res = await api.get('keeps/user')
        commit("setUserKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
//NOTE POST
    async createKeep({ commit, dispatch }, payload) { //payload is user id
      try {
        let res = await api.post('keeps', payload)
        commit("getKeepsByUserId", res.data) //must be logged in to create a keep
      } catch (error) {
        console.error(error)
      }
    },
//NOTE DELETE
    async deleteKeeps({ commit, dispatch }, payload) {
      try {
        await api.delete('keeps/' + payload)
        console.log("Your Keep has been delorted.")
        dispatch("getKeepsByUserId") //must be logged in to delete a Keep
      } catch (error) {
        console.error(error)
      }
    },

    //SECTION ======================= VAULTS =========================

//NOTE GET ONE VAULT BY ID    
    async getVaultById({ commit, dispatch }, payload) { //id
      try {
        let res = await api.get('vault/' + payload)
        commit("setActiveVault", res.data)
      } catch (error) {
        console.error(error)
      }
    },

    // setActiveVault({ commit }, payload) {
    //   commit("setActiveVault", payload)
    // },

//NOTE GET BY USER ID
    async getVaultByUserId({ commit, dispatch }) { 
      try {
        let res = await api.get('vault')
        commit("setUserVaults", res.data)
      } catch (error) {
        console.error(error)
      }
    },
//NOTE POST
    async createVault({ commit, dispatch }, payload) { //payload is user id
      try {
        let res = await api.post('vault', payload)
        commit("getVaultByUserId", res.data) //must be logged in to create a vault
      } catch (error) {
        console.error(error)
      }
    },
//NOTE DELETE
    async deleteVault({ commit, dispatch }, payload) {
      try {
        await api.delete('vault/' + payload)
        console.log("Your Vault has been delorted.")
        dispatch("getVaultByUserId") //must be logged in to delete a Keep
      } catch (error) {
        console.error(error)
      }
    },

//SECTION ==================== VAULT KEEPS ========================
    
//NOTE GET ALL VAULT KEEPS (BY ID)
    
    async getVaultKeepsById({ commit, dispatch }, payload) {
      try {
        let res = await api.get('vaultKeeps/' + payload)
        commit("setVaultKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    
    // TODO dispatch getKeepsByVaultId?
    // async getVaultKeeps({ commit, dispatch }, payload) {
    //   try {
    //     let res = await api.get('vaultkeeps')
    //     commit("setVaultKeeps", res.data)
    //     dispatch("get.....) 
    //   } catch (error) {
    //     console.error(error)
    //   }
    // },
//NOTE POST
    async addKeepToVault({ commit, dispatch }, payload) {
      try {
        await api.post("vaultKeeps" + payload)
      } catch (error) {
        console.error(error)
      }
    },

//NOTE DELETE
    async removeKeepsFromVaults({ commit, dispatch }, payload) {
      try {
        await api.put("vaultKeeps", payload) //must be logged in to delete/remove
        dispatch("getKeepsByUserId")
      } catch (error) {
        console.error(error)
      }
    },

  }
})


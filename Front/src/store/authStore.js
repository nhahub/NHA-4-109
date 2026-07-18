import { create } from "zustand";

export const useAuthStore = create((set) => ({
  accessToken: null,
  refreshToken: null,
  role: null,
  isAuthenticated: false,

  setAuth: ({ accessToken, refreshToken, role }) =>
    set({
      accessToken,
      refreshToken,
      role,
      isAuthenticated: true,
    }),

  clearAuth: () =>
    set({
      accessToken: null,
      refreshToken: null,
      role: null,
      isAuthenticated: false,
    }),
}));

export const getAccessToken = () =>
  useAuthStore.getState().accessToken;

export const clearAuth = () =>
  useAuthStore.getState().clearAuth();
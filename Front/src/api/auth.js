import { apiClient } from "./client";

export const login = (email, password) =>
  apiClient
    .post("/auth/login", { email, password })
    .then((res) => res.data);

export const registerTenant = (payload) =>
  apiClient
    .post("/auth/register/tenant", payload)
    .then((res) => res.data);

export const registerOwner = (payload) =>
  apiClient
    .post("/auth/register/owner", payload)
    .then((res) => res.data);
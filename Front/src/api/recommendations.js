import { apiClient } from "./client";

export const fetchRecommendations = (topN = 10) =>
  apiClient
    .get(`/recommendation?topN=${topN}`)
    .then((res) => res.data);

export const recordInteraction = (
  propertyId,
  interactionType,
  rating
) =>
  apiClient.post("/recommendation/interact", {
    propertyId,
    interactionType,
    rating,
  });
import axios from "axios";

export const axiosInstance = axios.create({
    baseURL: "http://localhost:5148",
    headers: {
       "Content-Type": "application/json"
    }
});
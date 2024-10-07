import baseApi from "./base.api";

export default {
    GetIngameItems: async () => {
        return await baseApi.get("InGameItem/get-all");
    },
    GetIngameItemById: async (id : number|string) => {
        return await baseApi.get("InGameItem/get-by-id?id=" + id);
    },
    CreateIngameItem: async (data : any) => {
        return await baseApi.post("InGameItem/create", data);
    },
    UpdateIngameItem: async (data : any) => {
        return await baseApi.put("InGameItem/update", data); // dang loi BE
    },
    DeleteIngameItem: async (id : number | string) => {
        return await baseApi.delete("InGameItem/Delete?id=" + id);
    },
}     
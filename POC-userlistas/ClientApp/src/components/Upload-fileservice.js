import http from "../../src/components/http-common.js";

class UploadFilesService {
    async upload(file, onUploadProgress) {
        let formData = new FormData();

        formData.append("file", file);

        return await http.post("files", formData, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
            onUploadProgress,
        });
    }

    getFiles() {
        return http.get("/files");
    }
}

export default new UploadFilesService();

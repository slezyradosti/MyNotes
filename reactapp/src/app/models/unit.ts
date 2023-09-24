import BaseModel from "./baseModel";

export interface Unit extends BaseModel {
    name: string;
    notebookId: string;
}
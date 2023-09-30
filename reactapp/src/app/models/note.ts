import BaseModel from "./baseModel";

export default interface Note extends BaseModel {
    name?: string;
    record: string;
    pageId: string;
}
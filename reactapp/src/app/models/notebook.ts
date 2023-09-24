import BaseModel from "./baseModel";

export interface Notebook extends BaseModel {
  name: string;
  userId?: string;
}

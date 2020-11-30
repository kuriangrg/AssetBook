import { AssetTree } from "../../model/assetActionTypes";

export interface Variant{
    variantId :number,
    variantName:string,
    variantDataLoc:string,
    assetId:number,
 }
 export interface AssetDetail{
     asset:AssetTree;
     variants?:Variant[];
 }
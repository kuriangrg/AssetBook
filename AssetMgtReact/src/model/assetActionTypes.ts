export const ASSET_LOAD="ASSET_LOAD"
export const ASSET_SUCCESS="ASSET_SUCCESS"
export const ASSET_FAILURE="ASSET_FAILURE"
export const ASSET_ADD="ASSET_ADD"

export type AssetTree={
    assetId: number;
    assetName: string;
    pathName?: string;
    assetDataLoc?:string;
    parentAssetId:number;
    thumbnail?:string;
    isFolder?:boolean;
    children?: AssetTree[];

}

export type AssetAbility={
    ability:Ability
}

export type FolderRequest={
    folderName:string,
    parentAssetId:number
}

export type Ability={
    name:string,
    url:string
}

export type AssetSprites={
    front_default:string,
    url:string
}

export type AssetStats={
    base_stat:number,
    stat:{
        name:string
    }
}

export interface AssetLoading{
    type: typeof ASSET_LOAD
}

export interface AssetFail{
    type: typeof ASSET_FAILURE
}

export interface AssetSuccess{
    type: typeof ASSET_SUCCESS
    payload:  AssetTree
}

export interface FolderAdd{
    type: typeof ASSET_ADD
    payload:  AssetTree,
  
}

export interface ImageAdd{
    type: typeof ASSET_ADD
    payload:  AssetTree,
   
    parentAssetId:number
    file:File
}
export type AssetDispatchTypes=AssetLoading | AssetSuccess |AssetFail |FolderAdd
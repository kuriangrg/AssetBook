import {Dispatch} from "redux"
import {AssetDispatchTypes,AssetTree,ASSET_ADD,ASSET_CURRENTASSET_ADD,ASSET_FAILURE,ASSET_LOAD, ASSET_SUCCESS, FolderRequest} from "../model/assetActionTypes"
import axios from "axios";
import configData from "../config";

export const GetAsset =(assetId:number)=> async (dispatch:Dispatch<AssetDispatchTypes>) =>{

    try
    {
      const res=await axios.get( `${configData.baseUrl}${configData.api.GetAsset}${assetId}`);
      dispatch({type:ASSET_SUCCESS,payload:res.data});
    }
    catch(e)
    {
        dispatch({type:ASSET_FAILURE})
    }
}

export const AddFolder =(folderRequest:FolderRequest)=> async (dispatch:Dispatch<AssetDispatchTypes>) =>{

    try
    {
        const res=await axios.post( `${configData.baseUrl}${configData.api.CreateFolder}`,{...folderRequest})
       //API Request
        dispatch({type:ASSET_ADD,
            payload:res.data});
    }
    catch(e)
    {
        dispatch({type:ASSET_FAILURE})
    }
}

export const AddImage =(file:File,parentAssetId:number)=> async (dispatch:Dispatch<AssetDispatchTypes>) =>{

  try
  {
      const formData=new FormData();
      formData.append("FormFile",file);
      formData.append("ParentAssetId",parentAssetId.toString());
      const res=await axios.post(`${configData.baseUrl}${configData.api.CreateMedia}`,formData);
      //API Request
       dispatch({type:ASSET_ADD,
           payload:res.data});
      
  }
  catch(e)
  {
    console.log(e);
      dispatch({type:ASSET_FAILURE})
  }
}
export const AddCurrentAsset =(currentAsset:AssetTree)=> async (dispatch:Dispatch<AssetDispatchTypes>) =>{

    try
    {
      
        //API Request
         dispatch({type:ASSET_CURRENTASSET_ADD,
             payload:currentAsset});
        
    }
    catch(e)
    {
      console.log(e);
        dispatch({type:ASSET_FAILURE})
    }
  }



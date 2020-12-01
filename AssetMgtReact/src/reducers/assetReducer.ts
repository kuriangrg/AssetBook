import { AssetDispatchTypes, AssetTree,ASSET_ADD, 
    ASSET_CURRENTASSET_ADD, ASSET_FAILURE, ASSET_LOAD, ASSET_SUCCESS } from "../model/assetActionTypes";

interface DefaultStateI{
    error?:boolean,
    currentAsset:AssetTree,
    asset:AssetTree
}
const defaultState:DefaultStateI={
    currentAsset:{
        assetId: 1,
        assetName: 'Parent',
        pathName:"1",
        parentAssetId:0,
        isFolder:true,
        children: [],
    },
    asset:{
        assetId: 1,
        assetName: 'Parent',
        pathName:"1",
        parentAssetId:0,
        isFolder:true,
        children: [],
      },
      error:false
};


const assetReducer=(state:DefaultStateI=defaultState,action:AssetDispatchTypes) : DefaultStateI=>{

    switch(action.type)
    {
        case ASSET_FAILURE:
            return{error:true,...state}
        case ASSET_LOAD:
                return{ ...state}
        case ASSET_SUCCESS:
            if(action.payload.assetId===1)
            {
            return{asset:action.payload,currentAsset:action.payload}
            }
            else
            {
              var result=state.asset!=null ? searchTree(state.asset,action.payload.assetId):null;
              if(result!=null)
              {
                    if(!result?.children)
                    {
                        result.children=[];
                    }
                    result.children=action.payload.children;
                }
                   
                return {...state};
            }
        case ASSET_ADD:
          var result=state.asset!=null ? searchTree(state.asset,action.payload.parentAssetId):null;
          if(result!=null)
          {
                if(!result?.children)
                {
                    result.children=[];
                }
                result.children.push(action.payload);
            }
               
            return {...state};
        case ASSET_CURRENTASSET_ADD:
                                    
                  return {...state, currentAsset: action.payload};

       
    }
    return {...state,currentAsset:state.asset};
    
}

const searchTree=function(element:AssetTree,assetId:number):AssetTree|null{
  if(element.assetId === assetId){
       return element;
  }else if (element.children != null){
       var result = null;
       for(var i=0; result == null && i < element.children.length; i++){
            result = searchTree(element.children[i], assetId);
       }
       return result;
  }
  return null;
}


export default assetReducer;
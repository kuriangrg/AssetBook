import { AssetDispatchTypes, AssetTree,ASSET_ADD, ASSET_FAILURE, ASSET_LOAD, ASSET_SUCCESS } from "../model/assetActionTypes";

interface DefaultStateI{

    loading:boolean,
    asset:AssetTree
}
const defaultState:DefaultStateI={
    loading:false,
    asset:{
        assetId: 1,
        assetName: 'Parent',
        pathName:"1",
        parentAssetId:0,
        isFolder:true,
        children: [],
      }
};


const assetReducer=(state:DefaultStateI=defaultState,action:AssetDispatchTypes) : DefaultStateI=>{

    switch(action.type)
    {
        case ASSET_FAILURE:
            return{loading:false,asset:state.asset}
        case ASSET_LOAD:
                return{ ...state}
        case ASSET_SUCCESS:
            if(action.payload.parentAssetId==null)
            {
            return{loading:false,asset:action.payload}
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
    }
    return state;
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
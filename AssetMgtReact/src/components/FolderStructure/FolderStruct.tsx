import { makeStyles } from "@material-ui/core";
import * as React from "react";
import TreeView from '@material-ui/lab/TreeView';
import ExpandMoreIcon from "@material-ui/icons/FolderOpen";
import ChevronRightIcon from "@material-ui/icons/Folder";
import TreeItem from "@material-ui/lab/TreeItem";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootStore } from "../../store";
import { AssetTree } from "../../model/assetActionTypes";
import { GetAsset } from "../../actions/assetAction";
import { useHistory } from "react-router-dom";
import { FolderProps } from "./iFolder";


export function FolderStruct(Props:FolderProps) {
  const classes = useStyles();
  const dispatch = useDispatch();
  const assetState=useSelector((state:RootStore)=>state.asset);
  const history = useHistory();
  
  useEffect(() => {
        dispatch(GetAsset(1))
  },[])

  const updateState = (item:AssetTree) => {
       Props.FolderClick(item);
       dispatch(GetAsset(item.assetId));
       history.push('/');
      
  };

  const renderTree = (nodes: AssetTree) =>(
   
    (nodes.isFolder)&&<TreeItem key={nodes.assetId} nodeId={nodes.assetId.toString()} label={nodes.assetName} onClick={()=>updateState(nodes)}>
      {Array.isArray(nodes.children) ? nodes.children.map((node) => renderTree(node)) : null}
    </TreeItem> )

  return (
    <TreeView
      className={classes.treeView}
      defaultCollapseIcon={<ExpandMoreIcon />}
      defaultExpanded={['1']}
      defaultExpandIcon={<ChevronRightIcon /> }
      defaultEndIcon={<ChevronRightIcon />}
    >
      {renderTree(assetState.asset)}
    </TreeView>
  );
}

const useStyles = makeStyles({
	treeView:{
		height: 240,
		flexGrow: 1,
		maxWidth: 400,
		margin:10
	
	},
});

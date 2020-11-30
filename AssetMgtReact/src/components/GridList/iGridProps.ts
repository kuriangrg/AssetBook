import { AssetTree } from "../../model/assetActionTypes";

export interface GridProps {
    currentAsset:AssetTree[];
    tileClick: (asset: AssetTree) => void,
    }
  
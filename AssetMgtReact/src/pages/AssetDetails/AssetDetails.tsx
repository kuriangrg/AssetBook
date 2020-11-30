import { Container } from "@material-ui/core";
import makeStyles from "@material-ui/styles/makeStyles";
import axios from "axios";
import React from "react";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import ImageCard from "../../components/VariantList/ImageCard";
import configData from "../../config";
import { AssetDetail } from "./IAssetDetail";

export function AssetDetails() {
    
    const { assetId } = useParams<Record<string, string | undefined>>();
    const classes = useStyles();
    const [assetDetailState, setAssetDetailState] = React.useState<AssetDetail>();
    
    useEffect(()=>{
        axios.get(`${configData.baseUrl}api/v1/Assets/GetAssetDetails/${assetId}`).then(response=>{
            setAssetDetailState(response.data);
        }
        ).catch(err=>console.log(err));
    },[assetId]);

    return(
        <Container maxWidth="lg" className={classes.root} >
             <h1>Asset and Variants</h1>
            <ImageCard  imageSrc={`${configData.baseUrl}${configData.api.imageServie}${assetDetailState&&assetDetailState.asset.assetDataLoc}`}
             title="Original Asset" description={`Original Asset of ${assetDetailState&&assetDetailState.asset.assetName}`} ></ImageCard>
            {
               assetDetailState&&
               assetDetailState.variants
               &&assetDetailState.variants.map((variant) => 
               <ImageCard key={variant.variantId} imageSrc={`${configData.baseUrl}${configData.api.imageServie}${variant.variantDataLoc}`} 
               title={variant.variantName}
               description={`Asset of ${assetDetailState.asset.assetName} with variant ${variant.variantName}`} ></ImageCard>)
            }
        </Container>
    );
}

const useStyles = makeStyles({
    root: {
      maxWidth: 800,
       
    }
  });
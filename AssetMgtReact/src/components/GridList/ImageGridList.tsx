import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Info';
import configData from '../../config';
import { GridProps } from './iGridProps';



export default function TitlebarGridList(assetData:GridProps) 
 {
  const classes = useStyles();
  return (
    <div className={classes.root}>
    <GridList cellHeight={300}  cols={4}  spacing={40} >
      <GridListTile key="Subheader" cols={4} style={{ height: 'auto' }} >
        <ListSubheader component="div"></ListSubheader>
      </GridListTile>
      {assetData.currentAsset.map((asset) => 
       (!asset.isFolder&&<GridListTile  key={asset.assetId} className={classes.gridTile} onClick={()=>assetData.tileClick(asset)}>
          <img src={`${configData.baseUrl}${configData.api.imageServie}${asset.thumbnail}`}  alt={asset.assetName} />
          <GridListTileBar
            title={asset.assetName}
            actionIcon={
              <IconButton aria-label={`info about ${asset.assetName}`} className={classes.icon}>
                <InfoIcon />
              </IconButton>
            }
          />
        </GridListTile>)
 
      )}
    </GridList>
  </div>
  );
  }

  const useStyles = makeStyles((theme) => ({
    root: {
      backgroundColor: theme.palette.background.paper,
    },
    gridTile:{
      cursor:'pointer'
    },
    titleBar: {
      background:
        'linear-gradient(to top, rgba(0,0,0,0.7) 0%, rgba(0,0,0,0.3) 70%, rgba(0,0,0,0) 100%)',
    },
    icon: {
      color: 'rgba(255, 255, 255, 0.54)',
    },
  }));
  

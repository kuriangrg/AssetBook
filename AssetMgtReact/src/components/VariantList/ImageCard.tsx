import React from 'react';
import { Box } from '@material-ui/core';
import { title } from 'process';
export type MediaCard={
    imageSrc?: string;
    title: string;
    description?: string;
}
export default function ImageCard(mediacard:MediaCard) {
  return (
   <React.Fragment  >
       <h1>{mediacard.title}</h1>
        <Box p={1} m={1}  >
        <img src={mediacard.imageSrc} alt={title} width="600"></img>
        </Box>
    </React.Fragment>
  );
}
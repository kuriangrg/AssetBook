import { makeStyles } from "@material-ui/styles";
import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { AddFolder, AddImage } from "../../actions/assetAction";
import { AssetTree } from "../../model/assetActionTypes";
import { DropzoneArea } from "material-ui-dropzone";
import TitlebarGridList from "../../components/GridList/ImageGridList";
import { useHistory } from 'react-router-dom';
import { Button, TextField } from "@material-ui/core";
import { HomeProps } from "./IHomeProps";
import { RootStore } from "../../store";



export function HomePage(props: HomeProps) {

	const dispatch = useDispatch();
	const [assetName, setassetName] = React.useState("");
	const history = useHistory();
	const assetState = useSelector((state: RootStore) => state.asset);
	const [mediaFile, setMediaFile] = React.useState<File>();
	const classes = useStyles();
	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		setassetName(e.target.value);

	};
	const handleSubmit = () => {
		dispatch(AddFolder({ folderName: assetName, parentAssetId: assetState.currentAsset.assetId }));
		setassetName("");

	}
	


	const imageSubmit = () => {
		if (mediaFile) {
			dispatch(AddImage(mediaFile, props.currentAsset.assetId));
		}
	}
	const assetDetails = (asset: AssetTree) => {

		history.push(`/details/${asset.assetId}`);
	}
	const onHandleImageChange = (files: File[]) => {

		setMediaFile(files[0]);
	}

	return (
		<div className={classes.root}>
			{/* <form autoComplete="off"  > */}
				<TextField label="Folder Name" required
					onChange={handleChange} value={assetName} />
				<Button onClick={handleSubmit} variant="contained" color="primary" disabled={!assetName}>
					Create Folder
				</Button>
			{/* </form> */}
			{/* <form> */}
				<DropzoneArea
					filesLimit={1}
					onChange={onHandleImageChange}
					clearOnUnmount={true}
				/>
				<Button onClick={imageSubmit} variant="contained" color="primary" disabled={!mediaFile}>
					Upload Asset
			</Button>
			{/* </form> */}
			{
				(assetState.currentAsset.children) && <TitlebarGridList currentAsset={assetState.currentAsset.children} tileClick={assetDetails} />
			}
		</div>
	);
}

const useStyles = makeStyles({
	root: {
		height: "100%",
		textAlign: "center",
		paddingTop: 20,
		paddingLeft: 15,
		paddingRight: 15,

	},
	centerContainer: {
		flex: 1,
		height: "90%",
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		flexDirection: "column",
	},

	button: {
		marginTop: 20,
		marginBottom: 30
	},
});
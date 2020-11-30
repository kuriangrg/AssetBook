import { makeStyles } from "@material-ui/styles";
import * as React from "react";
import { useDispatch } from "react-redux";
import { AddFolder, AddImage} from "../../actions/assetAction";
import { AssetTree } from "../../model/assetActionTypes";
import { DropzoneArea } from "material-ui-dropzone";
import TitlebarGridList from "../../components/GridList/ImageGridList";
import { useHistory } from 'react-router-dom';
import { Button, TextField } from "@material-ui/core";
import { HomeProps } from "./IHomeProps";


export function HomePage(props: HomeProps) {

	const dispatch = useDispatch();
	const [assetName, setassetName] = React.useState("");
	const history = useHistory();

	const [mediaFile, setMediaFile] = React.useState<File>();
	const classes = useStyles();
	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => setassetName(e.target.value);
	const handleSubmit = () => {
		dispatch(AddFolder({ folderName: assetName, parentAssetId: props.currentAsset.assetId }));
		setassetName("");
		
	}
	const imageSubmit = () => {
		if (mediaFile) {
			dispatch(AddImage(mediaFile, props.currentAsset.assetId));
		}
	}
	const assetDetails = (asset: AssetTree) => {

		history.push(`/details/${ asset.assetId}`);
	}
	const onHandleImageChange = (files: File[]) => {

		setMediaFile(files[0]);
	}

	return (
		<div className={classes.root}>
			<TextField  label="Folder Name"  onChange={handleChange} value={assetName}/>
			<Button onClick={handleSubmit} variant="contained" color="primary">
				Create Folder
			</Button>
			<DropzoneArea
				filesLimit={1}
				onChange={onHandleImageChange}
				clearOnUnmount={true}
			/>
			<Button onClick={imageSubmit} variant="contained" color="primary">
				Upload Asset
			</Button>
			{
				(props.currentAsset.children)&&<TitlebarGridList currentAsset={props.currentAsset.children} tileClick={assetDetails} />
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
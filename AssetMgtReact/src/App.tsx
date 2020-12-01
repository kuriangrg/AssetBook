// prettier-ignore
import { AppBar, Drawer as DrawerMui, Hidden, IconButton, Toolbar, Typography, useMediaQuery } from "@material-ui/core";
import { Theme } from "@material-ui/core/styles";
import MenuIcon from "@material-ui/icons/Menu";
import { makeStyles } from "@material-ui/styles";
import * as React from "react";
import { Route, Router ,Switch} from "react-router-dom";
import { history } from "./store";
import { FolderStruct } from "./components/FolderStructure/FolderStruct";
import { HomePage } from "./pages";
import { withRoot } from "./withRoot";
import { AssetTree } from "./model/assetActionTypes";
import { AssetDetails } from "./pages/AssetDetails/AssetDetails";
import { useDispatch } from "react-redux";

import { AddCurrentAsset } from "./actions/assetAction";

function App() {
	const classes = useStyles();
	const dispatch = useDispatch();
	const [mobileOpen, setMobileOpen] = React.useState(true);
	
	const isMobile = useMediaQuery((theme: Theme) =>
	theme.breakpoints.down("sm")
	);

	const handleDrawerToggle = () => {
		setMobileOpen(!mobileOpen);
	};

	const handleFolderClick = (asset:AssetTree) => {
		dispatch(AddCurrentAsset(asset));
	};

	return (
		<Router history={history}>
			<div className={classes.root}>
				<div className={classes.appFrame}>
					<AppBar className={classes.appBar}>
						<Toolbar>
							<IconButton
								color="inherit"
								aria-label="open drawer"
								onClick={handleDrawerToggle}
								className={classes.navIconHide}
							>
								<MenuIcon />
							</IconButton>
							<Typography
								variant="h6"
								color="inherit"
								noWrap={isMobile}
							>
								Asset Management
							</Typography>
						</Toolbar>
					</AppBar>
					<Hidden smDown>
						<DrawerMui
							variant="permanent"
							open
							classes={{
								paper: classes.drawerPaper,
							}}
						>
						<div>
							<div className={classes.drawerHeader} />
							<FolderStruct FolderClick={handleFolderClick} ></FolderStruct>
						</div>
						</DrawerMui>
					</Hidden>
					<Hidden mdUp>
						<DrawerMui
							variant="temporary"
							anchor={"left"}
							open={mobileOpen}
							classes={{
								paper: classes.drawerPaper,
							}}
							onClose={handleDrawerToggle}
							ModalProps={{
								keepMounted: true, // Better open performance on mobile.
							}}
						>
								<div>
							<div className={classes.drawerHeader} />
							<FolderStruct FolderClick={handleFolderClick} ></FolderStruct>
						</div>
						</DrawerMui>
					</Hidden>
					<div className={classes.content}>
						<Switch >
							<Route exact path="/">
								<HomePage></HomePage>
							</Route>
							<Route path="/details/:assetId">
								<AssetDetails></AssetDetails>
							</Route>
						</Switch>
						
					</div>
				</div>
			</div>
		</Router>
	);
}


const drawerWidth = 240;
const useStyles = makeStyles((theme: Theme) => ({
	root: {
		width: "100%",
		height: "100%",
		zIndex: 1,
		overflowX: "hidden",
		overflowY:"scroll"
	},
	
	appFrame: {
		position: "relative",
		display: "flex",
		width: "100%",
		height: "100%",
	},
	appBar: {
		zIndex: theme.zIndex.drawer + 1,
		position: "absolute",
	},
	navIconHide: {
		[theme.breakpoints.up("md")]: {
			display: "none",
		},
	},
	drawerHeader: { ...theme.mixins.toolbar },
	drawerPaper: {
		width: 250,
		backgroundColor: theme.palette.background.default,
		[theme.breakpoints.up("md")]: {
			width: drawerWidth,
			position: "relative",
			height: "100vh",
		},
	},
	content: {
		backgroundColor: theme.palette.background.default,
		width: "100%",
		height: "calc(100% - 56px)",
		marginTop: 56,
		[theme.breakpoints.up("sm")]: {
			height: "calc(100% - 64px)",
			marginTop: 64,
		},
	},
}));

export default withRoot(App);

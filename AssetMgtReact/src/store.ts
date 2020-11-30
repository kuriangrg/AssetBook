import {createStore,applyMiddleware} from "redux";
import RootReducer from "./reducers/rootReducer"
import {composeWithDevTools} from "redux-devtools-extension"
import thunk from "redux-thunk";
import { createBrowserHistory } from "history";

const Store=createStore(RootReducer,composeWithDevTools(applyMiddleware(thunk)));
export type RootStore=ReturnType<typeof RootReducer>
export default Store

const history = createBrowserHistory();

export { history };
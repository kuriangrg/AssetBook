import {combineReducers} from 'redux';
import assetReducer from './assetReducer';

const RootReducer=combineReducers({
    asset:assetReducer
});

export default RootReducer
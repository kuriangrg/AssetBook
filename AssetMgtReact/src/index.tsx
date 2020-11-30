import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import App from "./App";

import store from "./store"


const rootEl = document.getElementById("root");


ReactDOM.render(<Provider store={store}>
    <App></App>
</Provider>,rootEl);

// comment in for PWA with service worker in production mode
// registerServiceWorker();

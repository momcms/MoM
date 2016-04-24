/// <reference path="../../../typings/browser.d.ts" />
/// <reference path="../../../node_modules/zone.js/dist/zone.js.d.ts" />
/// <reference path="../../../node_modules/ng2-bootstrap/ng2-bootstrap.d.ts" />
import {bootstrap}    from "angular2/platform/browser"
import {ROUTER_PROVIDERS} from "angular2/router";
import {HTTP_PROVIDERS} from "angular2/http";
import {Ng2BootstrapConfig, Ng2BootstrapTheme} from 'ng2-bootstrap/ng2-bootstrap'; 

import {AppComponent} from "./app.component"
Ng2BootstrapConfig.theme = Ng2BootstrapTheme.BS4;
bootstrap(AppComponent, [ROUTER_PROVIDERS, HTTP_PROVIDERS]);

console.log(Ng2BootstrapConfig.theme);
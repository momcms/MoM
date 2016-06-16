/// <reference path="../../../typings/browser.d.ts" />
/// <reference path="../../../node_modules/zone.js/dist/zone.js.d.ts" />
/// <reference path="../../../node_modules/ng2-bootstrap/ng2-bootstrap.d.ts" />
import {bootstrap}    from "@angular/platform-browser-dynamic";
import {ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {HTTP_PROVIDERS} from "@angular/http";
import {Ng2BootstrapConfig, Ng2BootstrapTheme} from "ng2-bootstrap/ng2-bootstrap";

import {InstallComponent} from "./install.component";

import { enableProdMode } from "@angular/core";
Ng2BootstrapConfig.theme = Ng2BootstrapTheme.BS4;
//enableProdMode();
bootstrap(InstallComponent, [ROUTER_PROVIDERS, HTTP_PROVIDERS]);
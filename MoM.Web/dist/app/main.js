"use strict";
/// <reference path="../../../typings/browser.d.ts" />
/// <reference path="../../../node_modules/zone.js/dist/zone.js.d.ts" />
/// <reference path="../../../node_modules/ng2-bootstrap/ng2-bootstrap.d.ts" />
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var router_deprecated_1 = require("@angular/router-deprecated");
var http_1 = require("@angular/http");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var main_component_1 = require("./main.component");
ng2_bootstrap_1.Ng2BootstrapConfig.theme = ng2_bootstrap_1.Ng2BootstrapTheme.BS4;
//enableProdMode();
platform_browser_dynamic_1.bootstrap(main_component_1.MainComponent, [router_deprecated_1.ROUTER_PROVIDERS, http_1.HTTP_PROVIDERS]);

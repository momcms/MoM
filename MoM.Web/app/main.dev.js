"use strict";
/// <reference path="../../../typings/browser.d.ts" />
/// <reference path="../../../node_modules/zone.js/dist/zone.js.d.ts" />
/// <reference path="../../../node_modules/ng2-bootstrap/ng2-bootstrap.d.ts" />
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var router_deprecated_1 = require("@angular/router-deprecated");
var http_1 = require("@angular/http");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var app_component_1 = require("./app.component");
ng2_bootstrap_1.Ng2BootstrapConfig.theme = ng2_bootstrap_1.Ng2BootstrapTheme.BS4;
platform_browser_dynamic_1.bootstrap(app_component_1.AppComponent, [router_deprecated_1.ROUTER_PROVIDERS, http_1.HTTP_PROVIDERS]);
// console.log(Ng2BootstrapConfig.theme); 
//# sourceMappingURL=main.dev.js.map
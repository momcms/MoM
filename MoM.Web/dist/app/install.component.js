"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
// add all operators to Observable
require("rxjs/Rx");
var core_1 = require("@angular/core");
var router_deprecated_1 = require("@angular/router-deprecated");
var common_1 = require("@angular/common");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var InstallComponent = (function () {
    function InstallComponent(router, location) {
        this.router = router;
        this.location = location;
        this.routes = null;
    }
    InstallComponent.prototype.ngOnInit = function () {
        if (this.routes === null) {
            this.routes = [
                new router_deprecated_1.AsyncRoute({
                    path: "/",
                    name: "Install",
                    useAsDefault: true,
                    data: { includeInMenu: true },
                    loader: function () { return System.import("app/install/install").then(function (c) { return c["InstallComponent"]; }); }
                })
            ];
            this.router.config(this.routes);
        }
    };
    InstallComponent.prototype.getLinkStyle = function (route) {
        return this.location.path().indexOf(route.path) > -1;
    };
    InstallComponent = __decorate([
        core_1.Component({
            selector: "app",
            templateUrl: "/pages/appinstall",
            directives: [router_deprecated_1.RouterOutlet, router_deprecated_1.RouterLink, router_deprecated_1.ROUTER_DIRECTIVES, ng2_bootstrap_1.CollapseDirective]
        }), 
        __metadata('design:paramtypes', [router_deprecated_1.Router, common_1.Location])
    ], InstallComponent);
    return InstallComponent;
}());
exports.InstallComponent = InstallComponent;

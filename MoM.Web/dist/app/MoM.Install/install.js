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
var core_1 = require("@angular/core");
var router_deprecated_1 = require("@angular/router-deprecated");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var InstallComponent = (function () {
    function InstallComponent() {
    }
    //message: string;
    //constructor() { }
    InstallComponent.prototype.ngOnInit = function () {
        //this.message = "Welcome to EasyModules.NET"
    };
    InstallComponent = __decorate([
        core_1.Component({
            selector: "install-page",
            templateUrl: "/pages/install",
            directives: [router_deprecated_1.RouterLink, ng2_bootstrap_1.TAB_DIRECTIVES]
        }), 
        __metadata('design:paramtypes', [])
    ], InstallComponent);
    return InstallComponent;
}());
exports.InstallComponent = InstallComponent;

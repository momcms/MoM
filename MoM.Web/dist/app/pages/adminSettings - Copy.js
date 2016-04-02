System.register(["angular2/core", 'ng2-bootstrap/ng2-bootstrap'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, ng2_bootstrap_1;
    var AdminSettingsComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (ng2_bootstrap_1_1) {
                ng2_bootstrap_1 = ng2_bootstrap_1_1;
            }],
        execute: function() {
            AdminSettingsComponent = (function () {
                //message: string;
                function AdminSettingsComponent() {
                }
                AdminSettingsComponent.prototype.ngOnInit = function () {
                    //this.message = "Welcome to EasyModules.NET"
                };
                AdminSettingsComponent = __decorate([
                    core_1.Component({
                        selector: "mom-admin-settings",
                        directives: [ng2_bootstrap_1.TAB_DIRECTIVES],
                        templateUrl: "/pages/adminsettings"
                    }), 
                    __metadata('design:paramtypes', [])
                ], AdminSettingsComponent);
                return AdminSettingsComponent;
            })();
            exports_1("AdminSettingsComponent", AdminSettingsComponent);
        }
    }
});
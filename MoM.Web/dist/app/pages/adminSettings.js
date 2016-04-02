System.register(["angular2/core", 'ng2-bootstrap/ng2-bootstrap', '../core/services/sitesettingsservice', 'angular2/router'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, ng2_bootstrap_1, sitesettingsservice_1, router_1;
    var AdminSettingsComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (ng2_bootstrap_1_1) {
                ng2_bootstrap_1 = ng2_bootstrap_1_1;
            },
            function (sitesettingsservice_1_1) {
                sitesettingsservice_1 = sitesettingsservice_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            AdminSettingsComponent = (function () {
                function AdminSettingsComponent(service, router, routeParams) {
                    this.service = service;
                    this.router = router;
                    this.routeParams = routeParams;
                    this.isLoading = false;
                }
                AdminSettingsComponent.prototype.ngOnInit = function () {
                    this.getSiteSettings();
                    //this.message = "Welcome to EasyModules.NET"
                };
                AdminSettingsComponent.prototype.getSiteSettings = function () {
                    var _this = this;
                    this.isLoading = true;
                    this.service.getSiteSettings(function (json) {
                        if (json) {
                            _this.siteSettings = json;
                            _this.isLoading = false;
                            console.log(_this.siteSettings);
                        }
                    });
                };
                AdminSettingsComponent.prototype.onSaveSiteSettings = function () {
                    var _this = this;
                    this.isLoading = true;
                    this.service.saveSiteSettings(this.siteSettings, function (json) {
                        if (json) {
                            _this.siteSettings = json;
                            _this.isLoading = false;
                            console.log(_this.siteSettings);
                        }
                    });
                };
                AdminSettingsComponent.prototype.onCancel = function () {
                    //this.router.navigate(['../AdminSettings']);
                };
                AdminSettingsComponent = __decorate([
                    core_1.Component({
                        selector: "mom-admin-settings",
                        directives: [ng2_bootstrap_1.TAB_DIRECTIVES],
                        providers: [sitesettingsservice_1.SiteSettingsService],
                        templateUrl: "/pages/adminsettings"
                    }), 
                    __metadata('design:paramtypes', [sitesettingsservice_1.SiteSettingsService, router_1.Router, router_1.RouteParams])
                ], AdminSettingsComponent);
                return AdminSettingsComponent;
            })();
            exports_1("AdminSettingsComponent", AdminSettingsComponent);
        }
    }
});

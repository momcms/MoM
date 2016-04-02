System.register(["angular2/core", 'ng2-bootstrap/ng2-bootstrap', '../core/services/moduleservice'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, ng2_bootstrap_1, moduleservice_1;
    var AdminModulesComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (ng2_bootstrap_1_1) {
                ng2_bootstrap_1 = ng2_bootstrap_1_1;
            },
            function (moduleservice_1_1) {
                moduleservice_1 = moduleservice_1_1;
            }],
        execute: function() {
            AdminModulesComponent = (function () {
                function AdminModulesComponent(service) {
                    this.service = service;
                    this.isLoading = false;
                }
                AdminModulesComponent.prototype.ngOnInit = function () {
                    this.getInstalledModules();
                };
                AdminModulesComponent.prototype.getInstalledModules = function () {
                    var _this = this;
                    this.isLoading = true;
                    this.service.getInstalledModules(function (json) {
                        if (json) {
                            _this.modules = json;
                            _this.isLoading = false;
                            console.log(_this.modules);
                        }
                    });
                };
                AdminModulesComponent = __decorate([
                    core_1.Component({
                        selector: "mom-admin-modules",
                        directives: [ng2_bootstrap_1.TAB_DIRECTIVES],
                        providers: [moduleservice_1.ModuleService],
                        templateUrl: "/pages/adminmodules"
                    }), 
                    __metadata('design:paramtypes', [moduleservice_1.ModuleService])
                ], AdminModulesComponent);
                return AdminModulesComponent;
            })();
            exports_1("AdminModulesComponent", AdminModulesComponent);
        }
    }
});

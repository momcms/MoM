System.register(["angular2/core", 'ng2-bootstrap/ng2-bootstrap', "../core/services/adminidentityservice"], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, ng2_bootstrap_1, adminidentityservice_1;
    var AdminSecurityComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (ng2_bootstrap_1_1) {
                ng2_bootstrap_1 = ng2_bootstrap_1_1;
            },
            function (adminidentityservice_1_1) {
                adminidentityservice_1 = adminidentityservice_1_1;
            }],
        execute: function() {
            AdminSecurityComponent = (function () {
                //message: string;
                function AdminSecurityComponent(service) {
                    this.service = service;
                    this.isLoading = false;
                }
                AdminSecurityComponent.prototype.ngOnInit = function () {
                    this.initUsers();
                };
                AdminSecurityComponent.prototype.initUsers = function () {
                    this.pagerUsers = { pageNo: 0, pageSize: 10, sortColumn: "UserName", sortByAscending: false };
                    this.getUsers();
                };
                AdminSecurityComponent.prototype.getUsers = function () {
                    var _this = this;
                    this.service.getUsers(this.pagerUsers, function (json) {
                        if (json) {
                            _this.users = json;
                            _this.isLoading = false;
                            console.log(_this.users);
                        }
                    });
                };
                AdminSecurityComponent = __decorate([
                    core_1.Component({
                        selector: "mom-admin-security",
                        directives: [ng2_bootstrap_1.TAB_DIRECTIVES],
                        providers: [adminidentityservice_1.AdminIdentityService],
                        templateUrl: "/pages/adminsecurity"
                    }), 
                    __metadata('design:paramtypes', [adminidentityservice_1.AdminIdentityService])
                ], AdminSecurityComponent);
                return AdminSecurityComponent;
            })();
            exports_1("AdminSecurityComponent", AdminSecurityComponent);
        }
    }
});

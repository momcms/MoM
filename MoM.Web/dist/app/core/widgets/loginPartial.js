System.register(["angular2/core", "angular2/src/common/directives/core_directives", "../services/accountservice"], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, core_directives_1, accountservice_1;
    var LoginPartialComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (core_directives_1_1) {
                core_directives_1 = core_directives_1_1;
            },
            function (accountservice_1_1) {
                accountservice_1 = accountservice_1_1;
            }],
        execute: function() {
            LoginPartialComponent = (function () {
                function LoginPartialComponent(service) {
                    this.service = service;
                    this.isLoading = false;
                    this.pageSize = 10;
                }
                LoginPartialComponent.prototype.ngOnInit = function () {
                    //this.get();
                };
                LoginPartialComponent.prototype.logIn = function () {
                    this.isLoading = true;
                };
                LoginPartialComponent.prototype.logOff = function () {
                    var _this = this;
                    this.isLoading = true;
                    this.service.logOff(function (json) {
                        if (json) {
                            console.log(json);
                            _this.isLoading = false;
                        }
                    });
                };
                LoginPartialComponent = __decorate([
                    core_1.Component({
                        selector: "account-login-partial",
                        templateUrl: "/account/widgets/loginpartial",
                        providers: [accountservice_1.AccountService],
                        directives: core_directives_1.CORE_DIRECTIVES
                    }), 
                    __metadata('design:paramtypes', [accountservice_1.AccountService])
                ], LoginPartialComponent);
                return LoginPartialComponent;
            })();
            exports_1("LoginPartialComponent", LoginPartialComponent);
        }
    }
});

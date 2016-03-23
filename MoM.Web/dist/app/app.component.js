System.register(['rxjs/Rx', "angular2/core", "angular2/router"], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1;
    var AppComponent;
    return {
        setters:[
            function (_1) {},
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            AppComponent = (function () {
                function AppComponent(router, location) {
                    this.router = router;
                    this.location = location;
                    this.routes = null;
                    this.menu = null;
                }
                AppComponent.prototype.ngOnInit = function () {
                    if (this.routes === null) {
                        this.routes = [
                            new router_1.AsyncRoute({
                                path: "/",
                                name: "Home",
                                useAsDefault: true,
                                data: { includeInMenu: true },
                                loader: function () { return System.import("app/pages/home").then(function (c) { return c["HomeComponent"]; }); }
                            }),
                            new router_1.AsyncRoute({
                                path: "/services",
                                name: "Services",
                                useAsDefault: false,
                                data: { includeInMenu: true },
                                loader: function () { return System.import("app/pages/services").then(function (c) { return c["ServicesComponent"]; }); }
                            }),
                            new router_1.AsyncRoute({
                                path: "/blog",
                                name: "Blog",
                                useAsDefault: false,
                                data: { includeInMenu: true },
                                loader: function () { return System.import("app/modules/MoM.Blog/pages/blog").then(function (c) { return c["BlogComponent"]; }); }
                            }),
                            new router_1.AsyncRoute({
                                path: "blog/:year/:month/:urlSlug",
                                name: "Post",
                                useAsDefault: false,
                                data: { includeInMenu: false },
                                loader: function () { return System.import("app/modules/MoM.Blog/pages/post").then(function (c) { return c["PostComponent"]; }); }
                            }),
                            new router_1.AsyncRoute({
                                path: "/admin/...",
                                name: "Admin",
                                useAsDefault: false,
                                data: { includeInMenu: true },
                                loader: function () { return System.import("app/pages/admin").then(function (c) { return c["AdminComponent"]; }); }
                            })
                        ];
                        this.router.config(this.routes);
                        this.menu = this.routes.filter(function (route) {
                            return route.data.includeInMenu === true;
                        });
                    }
                };
                AppComponent.prototype.getLinkStyle = function (route) {
                    return this.location.path().indexOf(route.path) > -1;
                };
                AppComponent = __decorate([
                    core_1.Component({
                        selector: "app",
                        templateUrl: "/pages/app",
                        directives: [router_1.ROUTER_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [router_1.Router, router_1.Location])
                ], AppComponent);
                return AppComponent;
            })();
            exports_1("AppComponent", AppComponent);
        }
    }
});

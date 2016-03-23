System.register(["angular2/core", 'angular2/router'], function(exports_1) {
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
    var AdminComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            AdminComponent = (function () {
                function AdminComponent() {
                }
                AdminComponent.prototype.ngOnInit = function () {
                };
                AdminComponent = __decorate([
                    core_1.Component({
                        selector: "mom-admin",
                        templateUrl: "/pages/admin",
                        directives: [router_1.RouterOutlet]
                    }),
                    router_1.RouteConfig([
                        new router_1.AsyncRoute({
                            path: "/",
                            name: "AdminContent",
                            useAsDefault: true,
                            data: { includeInMenu: true, icon: "fa fa-sitemap fa-2x", title: "Content" },
                            loader: function () { return System.import("app/pages/admincontent").then(function (c) { return c["AdminContentComponent"]; }); }
                        }),
                        new router_1.AsyncRoute({
                            path: "/blog",
                            name: "AdminBlog",
                            useAsDefault: false,
                            data: { includeInMenu: false, icon: "fa fa-book", title: "Blog" },
                            loader: function () { return System.import("app/modules/MoM.Blog/pages/admin").then(function (c) { return c["AdminComponent"]; }); }
                        }),
                        new router_1.AsyncRoute({
                            path: "/blog/post",
                            name: "AdminBlogPostCreate",
                            useAsDefault: false,
                            data: { includeInMenu: false, icon: "" },
                            loader: function () { return System.import("app/modules/MoM.Blog/pages/adminpost").then(function (c) { return c["AdminPostComponent"]; }); }
                        }),
                        new router_1.AsyncRoute({
                            path: "/blog/post/:postId",
                            name: "AdminBlogPostEdit",
                            useAsDefault: false,
                            data: { includeInMenu: false, icon: "" },
                            loader: function () { return System.import("app/modules/MoM.Blog/pages/adminpost").then(function (c) { return c["AdminPostComponent"]; }); }
                        })
                    ]), 
                    __metadata('design:paramtypes', [])
                ], AdminComponent);
                return AdminComponent;
            })();
            exports_1("AdminComponent", AdminComponent);
        }
    }
});

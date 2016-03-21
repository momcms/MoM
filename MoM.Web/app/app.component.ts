import {Component, OnInit} from "angular2/core";
import {AsyncRoute, Router, RouteDefinition, RouteConfig, Location, ROUTER_DIRECTIVES} from "angular2/router";

declare var System: any;

@Component({
    selector: "app",
    templateUrl: "/components/template",
    directives: [ROUTER_DIRECTIVES]
})

export class AppComponent implements OnInit {
    public routes: RouteDefinition[] = null;
    public menu: RouteDefinition[] = null;
    constructor(
        private router: Router,
        private location: Location
    ) { }

    ngOnInit() {
        if (this.routes === null) {
            this.routes = [
                new AsyncRoute({
                    path: "/",
                    name: "Home",
                    useAsDefault: true,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/components/home").then(c => c["HomeComponent"])
                }),
                new AsyncRoute({
                    path: "/services",
                    name: "Services",
                    useAsDefault: false,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/components/services").then(c => c["ServicesComponent"])
                }),
                new AsyncRoute({
                    path: "/admin",
                    name: "Admin",
                    useAsDefault: false,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/components/admin").then(c => c["AdminComponent"])
                }),
                new AsyncRoute({
                    path: "/blog",
                    name: "Blog",
                    useAsDefault: false,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/modules/MoM.Blog/pages/blog").then(c => c["BlogComponent"])
                }),
                new AsyncRoute({
                    path: "blog/:year/:month/:urlSlug",
                    name: "Post",
                    useAsDefault: false,
                    data: { includeInMenu: false },
                    loader: () => System.import("app/modules/MoM.Blog/pages/post").then(c => c["PostComponent"])
                }),
                new AsyncRoute({
                    path: "/admin/blog",
                    name: "AdminBlog",
                    useAsDefault: false,
                    data: { includeInMenu: false, includeInAdminMenu: true, icon: "" },
                    loader: () => System.import("app/modules/MoM.Blog/pages/admin").then(c => c["AdminComponent"])
                }),
            ];

            this.router.config(this.routes);
            this.menu = this.routes.filter(function (route) {
                return route.data.includeInMenu === true;
            });
        }
    }

    getLinkStyle(route: RouteDefinition) {
        return this.location.path().indexOf(route.path) > -1;
    }
}
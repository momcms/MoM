// Add all operators to Observable
import 'rxjs/Rx';
import {Component, OnInit} from "angular2/core";
import {AsyncRoute, Router, RouterLink, RouteDefinition, RouteConfig, ROUTER_DIRECTIVES, RouterOutlet} from "angular2/router";
import { Location } from "angular2/platform/common"
import { CollapseDirective } from 'ng2-bootstrap/ng2-bootstrap';

declare var System: any;

@Component({
    selector: "app",
    templateUrl: "/pages/app",
    directives: [RouterOutlet, RouterLink, ROUTER_DIRECTIVES, CollapseDirective]
})

export class AppComponent implements OnInit {
    public routes: RouteDefinition[] = null;
    public menu: RouteDefinition[] = null;
    public isCollapsed: boolean = true;
    public isAdminArea: boolean = false;
    constructor(
        private router: Router,
        private location: Location
    ) { }
   

    ngOnInit() {
        if (this.routes === null) {
            this.routes = [
                //new AsyncRoute({
                //    path: "/",
                //    name: "Public",
                //    useAsDefault: true,
                //    data: { includeInMenu: false },
                //    loader: () => System.import("app/pages/public").then(c => c["PublicComponent"])
                //}),
                new AsyncRoute({
                    path: "/",
                    name: "Home",
                    useAsDefault: true,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/modules/MoM.CMS/pages/home").then(c => c["HomeComponent"])
                }),
                new AsyncRoute({
                    path: "/services",
                    name: "Services",
                    useAsDefault: false,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/modules/MoM.CMS/pages/services").then(c => c["ServicesComponent"])
                }),
                new AsyncRoute({
                    path: "/products",
                    name: "Products",
                    useAsDefault: false,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/modules/MoM.CMS/pages/products").then(c => c["ProductsComponent"])
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
                    path: "/admin/...",
                    name: "Admin",
                    useAsDefault: false,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/modules/MoM.CMS/pages/admin").then(c => c["AdminComponent"])
                })
            ];

            this.router.config(this.routes);
            this.menu = this.routes.filter(function (route) {
                return route.data.includeInMenu === true;
            });
        }

    }

    //hide footer if the area is admin
    ngDoCheck() {        
        this.isAdminArea = this.location.path().startsWith("/admin");
    }
    getLinkStyle(route: RouteDefinition) {
        return this.location.path().indexOf(route.path) > -1;
    }
}
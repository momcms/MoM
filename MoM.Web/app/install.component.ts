// add all operators to Observable
//import "rxjs/Rx";
import {Component, OnInit} from "@angular/core";
import {AsyncRoute, Router, RouterLink, RouteDefinition, ROUTER_DIRECTIVES, RouterOutlet} from "@angular/router-deprecated";
import { Location } from "@angular/common";
import { CollapseDirective } from "ng2-bootstrap/ng2-bootstrap";

declare var System: any;

@Component({
    selector: "app",
    templateUrl: "/pages/appinstall",
    directives: [RouterOutlet, RouterLink, ROUTER_DIRECTIVES, CollapseDirective]
})

export class InstallComponent implements OnInit {
    public routes: RouteDefinition[] = null;
    constructor(
        private router: Router,
        private location: Location
    ) { }

    ngOnInit() {
        if (this.routes === null) {
            this.routes = [
                new AsyncRoute({
                    path: "/",
                    name: "Install",
                    useAsDefault: true,
                    data: { includeInMenu: true },
                    loader: () => System.import("app/install/install").then(c => c["InstallComponent"])
                })
            ];

            this.router.config(this.routes);
        }

    }
    getLinkStyle(route: RouteDefinition) {
        return this.location.path().indexOf(route.path) > -1;
    }
}
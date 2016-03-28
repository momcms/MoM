//import {Component, OnInit} from "angular2/core";
//import {AsyncRoute, RouteConfig, RouterLink, RouterOutlet, RouteData, Router, ROUTER_DIRECTIVES} from 'angular2/router';
//declare var System: any;
//@Component({
//    selector: "mom-public",
//    templateUrl: "/pages/public",
//    directives: [RouterOutlet, RouterLink]
//})
//@RouteConfig([
//    new AsyncRoute({
//        path: "/",
//        name: "Home",
//        useAsDefault: true,
//        data: { includeInMenu: true },
//        loader: () => System.import("app/pages/home").then(c => c["HomeComponent"])
//    }),
//    new AsyncRoute({
//        path: "/services",
//        name: "Services",
//        useAsDefault: false,
//        data: { includeInMenu: true },
//        loader: () => System.import("app/pages/services").then(c => c["ServicesComponent"])
//    }),
//    new AsyncRoute({
//        path: "/blog",
//        name: "Blog",
//        useAsDefault: false,
//        data: { includeInMenu: true },
//        loader: () => System.import("app/modules/MoM.Blog/pages/blog").then(c => c["BlogComponent"])
//    }),
//    new AsyncRoute({
//        path: "blog/:year/:month/:urlSlug",
//        name: "Post",
//        useAsDefault: false,
//        data: { includeInMenu: false },
//        loader: () => System.import("app/modules/MoM.Blog/pages/post").then(c => c["PostComponent"])
//    })
//])
//export class PublicComponent implements OnInit {
//    constructor(
//        private router: Router
//    ) { }
//    ngOnInit() {
//        //this.router.navigate(['./Home']);
//    }
//} 

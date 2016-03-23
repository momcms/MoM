import {Component, OnInit} from "angular2/core";
import {AsyncRoute, RouteConfig, RouterOutlet, RouteData} from 'angular2/router';

declare var System: any;

@Component({
    selector: "mom-admin",
    templateUrl: "/pages/admin",
    directives: [RouterOutlet]
})
@RouteConfig([
    new AsyncRoute({
        path: "/",
        name: "AdminContent",
        useAsDefault: true,
        data: { includeInMenu: true, icon: "fa fa-sitemap fa-2x", title: "Content" },
        loader: () => System.import("app/pages/admincontent").then(c => c["AdminContentComponent"])
    }),
    new AsyncRoute({
        path: "/blog",
        name: "AdminBlog",
        useAsDefault: false,
        data: { includeInMenu: false, icon: "fa fa-book", title: "Blog" },
        loader: () => System.import("app/modules/MoM.Blog/pages/admin").then(c => c["AdminComponent"])
    }),
    new AsyncRoute({
        path: "/blog/post",
        name: "AdminBlogPostCreate",
        useAsDefault: false,
        data: { includeInMenu: false, icon: "" },
        loader: () => System.import("app/modules/MoM.Blog/pages/adminpost").then(c => c["AdminPostComponent"])
    }),
    new AsyncRoute({
        path: "/blog/post/:postId",
        name: "AdminBlogPostEdit",
        useAsDefault: false,
        data: { includeInMenu: false, icon: "" },
        loader: () => System.import("app/modules/MoM.Blog/pages/adminpost").then(c => c["AdminPostComponent"])
    })
])
export class AdminComponent implements OnInit {
    ngOnInit() {

    }
}
import {Component, OnInit} from "@angular/core";
import {RouterLink} from "@angular/router-deprecated";

import { TAB_DIRECTIVES  } from "ng2-bootstrap/ng2-bootstrap";


@Component({
    selector: "install-page",
    templateUrl: "/pages/install",
    directives: [RouterLink, TAB_DIRECTIVES]
})
export class InstallComponent implements OnInit {
    siteSettings: SiteSettings;
    isLoading: boolean = false;

    //constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }

    onSaveDatabaseSettings() {
        this.isLoading = true;
        this.service.saveSiteSettings(this.siteSettings, json => {
            if (json) {
                this.siteSettings = json;
                this.isLoading = false;
                console.log(this.siteSettings);
            }
        });
    }
}
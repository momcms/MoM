import {Component, OnInit} from "@angular/core";
import {RouterLink} from "@angular/router-deprecated";

import {SiteSettingDto} from "../dtos/SiteSettingDto";
import {SiteSettingConnectionStringDto} from "../dtos/SiteSettingConnectionStringDto";
import {SiteSettingInstallationStatusDto} from "../dtos/SiteSettingInstallationStatusDto";
import { SetupService } from "../api/SetupService";

import { TAB_DIRECTIVES, PROGRESSBAR_DIRECTIVES } from "ng2-bootstrap/ng2-bootstrap";


@Component({
    selector: "install-page",
    templateUrl: "/pages/install",
    providers: [SetupService],
    directives: [RouterLink, TAB_DIRECTIVES, PROGRESSBAR_DIRECTIVES ]
})
export class InstallComponent implements OnInit {
    siteSetting: SiteSettingDto;
    connectionstring: SiteSettingConnectionStringDto;
    status: SiteSettingInstallationStatusDto;
    isLoading: boolean = false;

    constructor(
        private service: SetupService
    ) {

    }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }

    onSaveDatabaseSettings() {
        this.isLoading = true;
        this.service.saveConnectionstring(this.connectionstring).subscribe(
            status => this.status = status
        );
        console.log(this.status);
        //this.service.saveSiteSettings(this.siteSetting, json => {
        //    if (json) {
        //        this.siteSettings = json;
        //        this.isLoading = false;
        //        console.log(this.siteSetting);
        //    }
        //});
    }
}
import {Component, OnInit} from "@angular/core";
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from "@angular/common";
import {RouterLink} from "@angular/router-deprecated";

import {SiteSettingDto} from "../dtos/SiteSettingDto";
import {SiteSettingInputDto} from "../dtos/SiteSettingInputDto";
import {SiteSettingConnectionStringDto} from "../dtos/SiteSettingConnectionStringDto";
import {SiteSettingInstallationStatusDto} from "../dtos/SiteSettingInstallationStatusDto";
import { SetupService } from "../api/SetupService";

import { BUTTON_DIRECTIVES, TAB_DIRECTIVES, PROGRESSBAR_DIRECTIVES } from "ng2-bootstrap/ng2-bootstrap";


@Component({
    selector: "install-page",
    templateUrl: "/pages/install",
    providers: [SetupService],
    directives: [RouterLink, CORE_DIRECTIVES, FORM_DIRECTIVES, BUTTON_DIRECTIVES, TAB_DIRECTIVES, PROGRESSBAR_DIRECTIVES ]
})
export class InstallComponent implements OnInit {
    installStepsCompleted: number = 0;
    stepOneComplete: boolean = false;
    siteSetting: SiteSettingDto;
    connectionstring: SiteSettingConnectionStringDto;
    connectionAuthenticationModel: string = "sql";
    status: SiteSettingInstallationStatusDto;
    isLoading: boolean = false;
    hasError: boolean = false;
    errorMessage: string;
    constructor(
        private service: SetupService
    ) {

    }

    ngOnInit() {
        this.getConnectionstring();
    }

    getConnectionstring() {
        this.isLoading = true;
        this.service.getConnectionString().subscribe(
            connectionstring => {
                this.connectionstring = connectionstring;
                this.connectionAuthenticationModel = this.connectionstring.useWindowsAuthentication === true ? "windows" : "sql";
                this.isLoading = false;
            },
            error => {
                this.hasError = true;
                this.errorMessage = <any>error;
                this.isLoading = false;
            }
        );
    }

    onSaveDatabaseSettings() {
        this.isLoading = true;
        this.connectionstring.useWindowsAuthentication = this.connectionAuthenticationModel === "windows" ? true : false;
        this.service.saveConnectionstring(this.connectionstring).subscribe(
            status => {
                this.status = status;
                this.installStepsCompleted = Math.max.apply(Math, this.status.completedSteps);
                this.stepOneComplete = true;
                this.isLoading = false;
            },
            error => {
                this.hasError = true;
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    onAdminAccountSave() {
        var test = 0;
    }

    onSaveSettings(step:number) {
        var setting: SiteSettingInputDto;
        setting.siteSetting = this.siteSetting;
        setting.step = step;
        this.service.saveSiteSetting(setting).subscribe(
            status => {
                this.status = status;
                this.installStepsCompleted = Math.max.apply(Math, this.status.completedSteps);
                this.stepOneComplete = true;
                this.isLoading = false;
            },
            error => {
                this.hasError = true;
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }
}
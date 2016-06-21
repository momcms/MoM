import {Component, OnInit} from "@angular/core";
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from "@angular/common";
import {RouterLink} from "@angular/router-deprecated";

import {SiteSettingDto} from "../dtos/SiteSettingDto";
import {SiteSettingInputDto} from "../dtos/SiteSettingInputDto";
import {SiteSettingConnectionStringDto} from "../dtos/SiteSettingConnectionStringDto";
import {SiteSettingInstallationStatusDto} from "../dtos/SiteSettingInstallationStatusDto";
import {UserCreateDto} from "../dtos/UserCreateDto";
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
    isAllreadyInstalled: boolean = false;
    stepOneComplete: boolean = false;
    stepTwoComplete: boolean = false;
    stepThreeComplete: boolean = false;
    stepFourComplete: boolean = false;
    stepFiveComplete: boolean = false;
    siteSetting: SiteSettingDto;
    connectionstring: SiteSettingConnectionStringDto;
    connectionAuthenticationModel: string = "sql";
    status: SiteSettingInstallationStatusDto;
    isLoading: boolean = false;
    hasError: boolean = false;
    errorMessage: string;
    admin: UserCreateDto;
    constructor(
        private service: SetupService
    ) {
        this.admin = { username: "", email:"" , password:"" };
    }

    ngOnInit() {
        this.init();
    }

    init() {
        this.isLoading = true;
        this.service.init().subscribe(
            status => {
                this.siteSetting = status.siteSetting;
                this.status = status;
                this.checkCompletedSteps();
                this.getConnectionstring();
            },
            error => {
                this.hasError = true;
                this.errorMessage = <any>error;
                this.isLoading = false;
            }
        );
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
                this.checkCompletedSteps();
                this.isLoading = false;
            },
            error => {
                this.hasError = true;
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    onAdminAccountSave() {
        var self = this;
    }

    checkCompletedSteps() {
        var self = this;
        self.installStepsCompleted = self.status.installationStatus;
        for (var i = 0; i < self.installStepsCompleted; i++) {
            self.setCompletedStep(i);
        };
    }

    setCompletedStep(status: number) {
        switch (status) {
            case 1:
                this.stepOneComplete = true;
                return;
            case 2:
                this.stepTwoComplete = true;
                return;
            case 3:
                this.stepThreeComplete = true;
                return;
            case 4:
                this.stepFourComplete = true;
                return;
            case 5:
                this.stepFiveComplete = true;
                return;
            case 6:
                this.isAllreadyInstalled = true;
            default:
                return;
        }
    }
}
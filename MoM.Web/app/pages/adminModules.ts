import {Component, OnInit} from "angular2/core";
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {ExtensionInfo} from "../core/interfaces/iExtensionInfo";
import {AdminModuleService} from '../core/services/adminmoduleservice';

@Component({
    selector: "mom-admin-modules",
    directives: [TAB_DIRECTIVES],
    providers: [AdminModuleService],
    templateUrl: "/pages/adminmodules"
})
export class AdminModulesComponent implements OnInit {
    modules: ExtensionInfo;
    isLoading: boolean = false;

    constructor(
        private service: AdminModuleService
    ) { }

    ngOnInit() {
        this.getInstalledModules();
    }

    getInstalledModules() {
        this.isLoading = true;
        this.service.getInstalledModules(json => {
            if (json) {
                this.modules = json;
                this.isLoading = false;
                console.log(this.modules);
            }
        });
    }
}
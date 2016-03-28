import {Component, OnInit} from "angular2/core";
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';

@Component({
    selector: "mom-admin-settings",
    directives: [TAB_DIRECTIVES],
    templateUrl: "/pages/adminsettings"
})
export class AdminSettingsComponent implements OnInit {
    //message: string;

    constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }
}
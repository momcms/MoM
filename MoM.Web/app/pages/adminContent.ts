import {Component, OnInit} from "angular2/core";
import {Alert} from 'ng2-bootstrap/ng2-bootstrap';

@Component({
    selector: "mom-admin-content",
    directives: [Alert],
    templateUrl: "/pages/admincontent"
})
export class AdminContentComponent implements OnInit {
    //message: string;

    constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }
}
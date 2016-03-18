import {Component, OnInit} from "angular2/core";

@Component({
    selector: "mvc",
    templateUrl: "/components/admin"
})
export class AdminComponent implements OnInit {
    //message: string;

    constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }
}
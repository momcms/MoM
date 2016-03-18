import {Component, OnInit} from "angular2/core";

@Component({
    selector: "mvc",
    templateUrl: "/components/home"
})
export class HomeComponent implements OnInit {
    //message: string;

    constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }
}
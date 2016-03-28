import {Component, OnInit} from "angular2/core";
import {RouterLink} from 'angular2/router';

@Component({
    selector: "mvc",
    templateUrl: "/pages/home",
    directives: [RouterLink]
})
export class HomeComponent implements OnInit {
    //message: string;

    constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }
}
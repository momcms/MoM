import {Component, OnInit} from "angular2/core";
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Pager} from "../core/interfaces/iPager";
import {AdminIdentityService} from "../core/services/adminidentityservice";

@Component({
    selector: "mom-admin-security",
    directives: [TAB_DIRECTIVES],
    providers: [AdminIdentityService],
    templateUrl: "/pages/adminsecurity"
})
export class AdminSecurityComponent implements OnInit {
    isLoading: boolean = false;
    users: any;
    pagerUsers: Pager;
    //message: string;

    constructor(
        private service: AdminIdentityService
    ) { }

    ngOnInit() {
        this.initUsers();
    }

    initUsers() {
        this.pagerUsers = { pageNo: 0, pageSize: 10, sortColumn: "UserName", sortByAscending: false };
        this.getUsers();
    }

    getUsers() {
        this.service.getUsers(this.pagerUsers, json => {
            if (json) {
                this.users = json;
                this.isLoading = false;
                console.log(this.users);
            }
        });
    }
}
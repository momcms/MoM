import {Http, Headers, HTTP_PROVIDERS} from "angular2/http";
import {Injectable} from "angular2/core";
import {Pager} from "../interfaces/iPager";
import 'rxjs/Rx';

@Injectable()
export class AdminIdentityService {
    constructor(private http: Http) { }

    getUsers(pager: Pager, onNext: (json: any) => void) {
        this.http.get("api/identity/users/?pageNo=" + pager.pageNo + "&pageSize=" + pager.pageSize + "&sortColumn=" + pager.sortColumn + "&sortByAscending=" + pager.sortByAscending).map(response => response.json()).subscribe(onNext);
    }
}
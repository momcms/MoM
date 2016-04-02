import {Http, Headers, HTTP_PROVIDERS} from "angular2/http";
import {Injectable} from "angular2/core";

import 'rxjs/Rx';

@Injectable()
export class AccountService {
    constructor(private http: Http) { }

    logOff(onNext: (json: any) => void) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        this.http.post("account/logoff", "", { headers }).map(response => response.json()).subscribe(onNext);
    }
}
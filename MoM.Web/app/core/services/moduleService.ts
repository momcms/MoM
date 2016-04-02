import {Http, Headers, HTTP_PROVIDERS} from "angular2/http";
import {Injectable} from "angular2/core";
import {ExtensionInfo} from "../interfaces/iExtensionInfo";
import 'rxjs/Rx';

@Injectable()
export class ModuleService {
    constructor(private http: Http) { }

    getInstalledModules(onNext: (json: ExtensionInfo) => void) {
        this.http.get("api/modules/getinstalledmodules").map(response => response.json()).subscribe(onNext);
    }
}
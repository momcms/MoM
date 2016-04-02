import {Http, Headers, HTTP_PROVIDERS} from "angular2/http";
import {Injectable} from "angular2/core";
import {SiteSettings, Theme, Authentication, Facebook, Google, Microsoft, Twitter, Logo, Email} from "../interfaces/iSiteSettings";
import 'rxjs/Rx';

@Injectable()
export class SiteSettingsService {
    constructor(private http: Http) { }

    getSiteSettings(onNext: (json: SiteSettings) => void) {
        this.http.get("api/sitesettings/getsitesettings").map(response => response.json()).subscribe(onNext);
    }

    saveSiteSettings(siteSettings: SiteSettings, onNext: (json: SiteSettings) => void) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        this.http.post("api/sitesettings/savesitesettings", JSON.stringify(siteSettings), { headers }).map(response => response.json()).subscribe(onNext);
    }
}
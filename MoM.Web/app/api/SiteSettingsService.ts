
// $Classes/Enums/Interfaces(filter)[template][separator]
// filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
// template: The template to repeat for each matched item
// separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

// More info: http://frhagn.github.io/Typewriter/

import {Injectable} from "@angular/core";
import {Http, Response, Headers, RequestOptions, RequestOptionsArgs} from "@angular/http";
import {Observable} from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/share";

// import {parseModel} from '../models/ModelHelper';



import {SiteSettingDto} from "../dtos/SiteSettingDto";

@Injectable()
export class SiteSettingsService {
constructor(private _http: Http) { }


    public get = () : Observable<SiteSettingDto> => {
        return this._http.request("getsitesettings", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<SiteSettingDto>res.json()));
    }
    public saveSiteSettings = (siteSettings: SiteSettingDto) : Observable<SiteSettingDto> => {
        return this._http.request("savesitesettings", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "post",
            body: JSON.stringify(siteSettings)
        })).map(res => (<SiteSettingDto>res.json()));
    }

    public getModulePath = () : Observable<string> => {
        return this._http.request("getmodulepath", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<string>res.json()));
    }
}
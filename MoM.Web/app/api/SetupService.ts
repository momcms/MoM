
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



import {SiteSettingInstallationStatusDto} from "../dtos/SiteSettingInstallationStatusDto";
import {SiteSettingConnectionStringDto} from "../dtos/SiteSettingConnectionStringDto";
import {UserCreateDto} from "../dtos/UserCreateDto";
import {SiteSettingDto} from "../dtos/SiteSettingDto";

@Injectable()
export class SetupService {
constructor(private _http: Http) { }


    public init = () : Observable<SiteSettingInstallationStatusDto> => {
        return this._http.request("api/setup/init", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<SiteSettingInstallationStatusDto>res.json()));
    }
    public getConnectionString = () : Observable<SiteSettingConnectionStringDto> => {
        return this._http.request("api/setup/getconnectionstring", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<SiteSettingConnectionStringDto>res.json()));
    }
    public saveConnectionstring = (connectionstring: SiteSettingConnectionStringDto) : Observable<SiteSettingInstallationStatusDto> => {
        return this._http.request("api/setup/saveconnectionstring", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "post",
            body: JSON.stringify(connectionstring)
        })).map(res => (<SiteSettingInstallationStatusDto>res.json()));
    }
    public createAdmin = (user: UserCreateDto) : Observable<SiteSettingInstallationStatusDto> => {
        return this._http.request("api/setup/createadmin", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "post",
            body: JSON.stringify(user)
        })).map(res => (<SiteSettingInstallationStatusDto>res.json()));
    }
    public setupSocial = (siteSetting: SiteSettingDto) : Observable<SiteSettingInstallationStatusDto> => {
        return this._http.request("api/setup/setupsocial", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "post",
            body: JSON.stringify(siteSetting)
        })).map(res => (<SiteSettingInstallationStatusDto>res.json()));
    }
    public installModules = () : Observable<SiteSettingInstallationStatusDto> => {
        return this._http.request("api/setup/installmodules", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "post",
            body: JSON.stringify(null)
        })).map(res => (<SiteSettingInstallationStatusDto>res.json()));
    }

}
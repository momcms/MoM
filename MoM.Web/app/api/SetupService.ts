
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
import {ConnectionString} from "../dtos/ConnectionString";
import {InstallationResult} from "../dtos/InstallationResult";

@Injectable()
export class SetupService {
constructor(private _http: Http) { }


    public get = () : Observable<SiteSettingDto> => {
        return this._http.request(`getsitesettings`, new RequestOptions({
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<SiteSettingDto>res.json()));
    }
    public saveConnectionstring = (connectionstring: ConnectionString) : Observable<InstallationResult> => {
        return this._http.request(`saveconnectionstring`, new RequestOptions({
            method: "post",
            body: JSON.stringify(connectionstring)
        })).map(res => (<InstallationResult>res.json()));
    }

    public checkDatabaseConnection = (connectionstring: ConnectionString) : Observable<boolean> => {
        return this._http.request(`checkdatabaseconnection`, new RequestOptions({
            method: "post",
            body: JSON.stringify(connectionstring)
        })).map(res => (<boolean>res.json()));
    }
}


@Injectable()
export class ConnectionString {
constructor(private _http: Http) { }



}


@Injectable()
export class InstallationResult {
constructor(private _http: Http) { }



}
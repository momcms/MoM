
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



import {UserDto} from "../dtos/UserDto";
import {RoleDto} from "../dtos/RoleDto";

@Injectable()
export class IdentityService {
constructor(private _http: Http) { }

    public createRole = (roleName: string) : Observable<Response> => {
        return this._http.request(`createrole`, new RequestOptions({
            method: "post",
            body: JSON.stringify(roleName)
        }));
    }

    public getUsers = (pageNo: number, pageSize: number, sortColumn: string, sortByAscending: boolean) : Observable<UserDto[]> => {
        return this._http.request(`users?pageNo=${pageNo}&pageSize=${pageSize}&sortColumn=${sortColumn}&sortByAscending=${sortByAscending}`, new RequestOptions({
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<UserDto[]>res.json()));
    }
    public getRoles = (pageNo: number, pageSize: number, sortColumn: string, sortByAscending: boolean) : Observable<RoleDto[]> => {
        return this._http.request(`roles?pageNo=${pageNo}&pageSize=${pageSize}&sortColumn=${sortColumn}&sortByAscending=${sortByAscending}`, new RequestOptions({
            method: "get",
            body: JSON.stringify(null)
        })).map(res => (<RoleDto[]>res.json()));
    }

}
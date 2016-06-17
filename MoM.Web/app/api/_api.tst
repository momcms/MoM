${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using Typewriter.Extensions.WebApi;
    
    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        //settings.IncludeProject("Project.Name");
        settings.OutputExtension = ".ts"; //could also be .tsx
        
        settings.OutputFilenameFactory = file => System.IO.Path.ChangeExtension(RenameControllerToService(file.Name), settings.OutputExtension);
    }

    // Custom extension methods can be used in the template by adding a $ prefix
    string RenameControllerToService(string name) => name.Replace("Controller", "Service");
    string ServiceName(Class c) => RenameControllerToService(c.Name);
    string ServiceUrl(Class c) => c.Attributes.FirstOrDefault(x => x.Name.Equals("Route")).Value;
    Type[] CalculatedModelTypes(Class c)
    {
        var allTypes = c.Methods
            .SelectMany(m => m.Parameters.Select(p => p.Type).Concat(new [] { m.Type }))
            .Select(t => CalculatedType(t))
            .Where(t => t != null && (t.IsDefined || (t.IsEnumerable && !t.IsPrimitive)))
            .ToLookup(t => t.ClassName(), t => t);
        return allTypes.ToDictionary(l => l.Key, l => l.First()).Select(kvp => kvp.Value).ToArray();
    }
    Type CalculatedType(Type t)
    {
        var type = t;
        while (!type.IsEnumerable && type.IsGeneric) {
            type = type.Unwrap();
        }
        return type.Name == "IHttpActionResult" ? null : type;
    }
    string CalculatedTypeName(Type t)
    {
        var type = CalculatedType(t);
        return type != null ? type.Name : "void";
    }
    string UrlTrimmed(Method m) => m.Url().TrimEnd('/');
}
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

$Classes(c => c.Namespace == "MoM.Web.Controllers.Api")[
$CalculatedModelTypes[
import {$ClassName} from "../dtos/$ClassName";]

@Injectable()
export class $ServiceName {
constructor(private _http: Http) { }
$Methods(m => CalculatedTypeName(m.Type) == "void")[
    public $name = ($Parameters[$name: $Type][, ]) : Observable<Response> => {
        return this._http.request("$Route", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "$HttpMethod",
            body: JSON.stringify($RequestData)
        }));
    }]
$Methods(m => CalculatedTypeName(m.Type) != "void" && !CalculatedType(m.Type).IsPrimitive)[
    public $name = ($Parameters[$name: $Type][, ]) : Observable<$Type[$CalculatedTypeName]> => {
        return this._http.request("$Route", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "$HttpMethod",
            body: JSON.stringify($RequestData)
        })).map(res => (<$Type[$CalculatedTypeName]>res.json()));
    }]
$Methods(m => CalculatedTypeName(m.Type) != "void" && CalculatedType(m.Type).IsPrimitive)[
    public $name = ($Parameters[$name: $Type][, ]) : Observable<$Type[$CalculatedTypeName]> => {
        return this._http.request("$Route", new RequestOptions({
            headers: {
                "Content-Type": "application/json"
            },
            method: "$HttpMethod",
            body: JSON.stringify($RequestData)
        })).map(res => (<$Type[$CalculatedTypeName]>res.json()));
    }]
}]
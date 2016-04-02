System.register(["angular2/http", "angular2/core", 'rxjs/Rx'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var http_1, core_1;
    var SiteSettingsService;
    return {
        setters:[
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (_1) {}],
        execute: function() {
            SiteSettingsService = (function () {
                function SiteSettingsService(http) {
                    this.http = http;
                }
                SiteSettingsService.prototype.getSiteSettings = function (onNext) {
                    this.http.get("api/sitesettings/getsitesettings").map(function (response) { return response.json(); }).subscribe(onNext);
                };
                SiteSettingsService.prototype.saveSiteSettings = function (siteSettings, onNext) {
                    var headers = new http_1.Headers();
                    headers.append('Content-Type', 'application/json');
                    this.http.post("api/sitesettings/savesitesettings", JSON.stringify(siteSettings), { headers: headers }).map(function (response) { return response.json(); }).subscribe(onNext);
                };
                SiteSettingsService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [http_1.Http])
                ], SiteSettingsService);
                return SiteSettingsService;
            })();
            exports_1("SiteSettingsService", SiteSettingsService);
        }
    }
});

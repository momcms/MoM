System.register(["angular2/platform/browser", "angular2/router", "angular2/http", 'ng2-bootstrap/ng2-bootstrap', "./app.component"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var browser_1, router_1, http_1, ng2_bootstrap_1, app_component_1;
    return {
        setters:[
            function (browser_1_1) {
                browser_1 = browser_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (ng2_bootstrap_1_1) {
                ng2_bootstrap_1 = ng2_bootstrap_1_1;
            },
            function (app_component_1_1) {
                app_component_1 = app_component_1_1;
            }],
        execute: function() {
            ng2_bootstrap_1.Ng2BootstrapConfig.theme = ng2_bootstrap_1.Ng2BootstrapTheme.BS4;
            browser_1.bootstrap(app_component_1.AppComponent, [router_1.ROUTER_PROVIDERS, http_1.HTTP_PROVIDERS]);
            console.log(ng2_bootstrap_1.Ng2BootstrapConfig.theme);
        }
    }
});
//# sourceMappingURL=main.dev.js.map
/**
 * System configuration for MoM Angular 2
 * Adjust as necessary for your application needs.
 */
(function(global) {
  // map tells the System loader where to look for things
  var map = {
    'app':                          'app', // 'dist',
    '@angular':                     'lib/@angular',
    //'angular2-in-memory-web-api':   'lib/angular2-in-memory-web-api',
    'rxjs':                         'lib/rxjs',
    'moment':                       'lib/moment.min.js',
    //'ng2-bootstrap':                'lib/extensions/ng2-bootstrap',
    'ng2-prism':                    'lib/extensions/ng2-prism',
    'prismjs':                      'lib/extensions/ng2-prism/node_modules/prismjs',
    'dragula':                      'lib/extensions/dragula/dragula',
    'ng2-dragula':                  'lib/extensions/ng2-dragula'
  };
  // packages tells the System loader how to load when no filename and/or no extension
  var packages = {
    'app':                        { main: 'main.dev.js',  defaultExtension: 'js' },
    'rxjs':                       { defaultExtension: 'js' },
    //'angular2-in-memory-web-api': { defaultExtension: 'js' },
  };
  var ngPackageNames = [
    'common',
    'compiler',
    'core',
    'http',
    'platform-browser',
    'platform-browser-dynamic',
    'router',
    'router-deprecated',
    'upgrade',
  ];
  // Add package entries for angular packages
  ngPackageNames.forEach(function(pkgName) {
    packages['@angular/'+pkgName] = { main: pkgName + '.umd.js', defaultExtension: 'js' };
  });
  var config = {
    map: map,
    packages: packages
  }
  System.config(config);
})(this);
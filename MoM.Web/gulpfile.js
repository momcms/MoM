/// <binding ProjectOpened='watch-web' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

"use strict";

var gulp = require("gulp"),
    runSequence = require("run-sequence"),
    tslint = require("gulp-tslint"),
    typescript = require('gulp-typescript'),
    sass = require('gulp-ruby-sass'),
    notify = require("gulp-notify"),
    bower = require('gulp-bower'),
    rimraf = require('rimraf'),
    uglify = require('gulp-uglify'),
    cssmin = require('gulp-cssmin'),
    rename = require('gulp-rename');

var paths = {
    npm: '../../node_modules/',
    scripDist: "./dist",
    scriptDestination: "./wwwroot/app/",
    lib: './wwwroot/lib/'
}

var libs = [
    paths.npm + 'angular2/bundles/angular2.dev.js',
    paths.npm + 'angular2/bundles/http.dev.js',
    paths.npm + 'angular2/bundles/angular2-polyfills.js',
    paths.npm + 'angular2/bundles/router.dev.js',
    paths.npm + 'es6-shim/es6-shim.js',
    paths.npm + 'systemjs/dist/system.js',
    paths.npm + 'systemjs/dist/system-polyfills.js',
    paths.npm + 'moment/moment.js',
    paths.npm + 'jquery/dist/jquery.js',
    paths.npm + 'jquery-validation/dist/jquery.validate.js',
    paths.npm + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
    paths.npm + 'bootstrap/dist/js/bootstrap.js',

    paths.npm + 'angular2/bundles/angular2.min.js',
    paths.npm + 'angular2/bundles/http.min.js',
    paths.npm + 'angular2/bundles/angular2-polyfills.min.js',
    paths.npm + 'angular2/bundles/router.min.js',
    paths.npm + 'es6-shim/es6-shim.min.js',
    paths.npm + 'systemjs/dist/system.js',
    paths.npm + 'systemjs/dist/system-polyfills.js',
    paths.npm + 'moment/min/moment.min.js',
    paths.npm + 'jquery/dist/jquery.min.js',
    paths.npm + 'jquery-validation/dist/jquery.validate.min.js',
    paths.npm + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js',
    paths.npm + 'bootstrap/dist/js/bootstrap.min.js',

    paths.npm + 'systemjs/dist/system.js.map',
    paths.npm + 'systemjs/dist/system-polyfills.js.map',
];

var tsProject = typescript.createProject('app/tsconfig.json');

var moduleName = "MoM.CMS";

gulp.task('copy-scripts', ['typescript-transpile'], function () {
    gulp.src(paths.scripDist + "/app/**/*.js")
    .pipe(gulp.dest(paths.scriptDestination))
});

gulp.task('clean-app', function (cb) {
    rimraf('./wwwroot/app', cb);
});

gulp.task('clean-dist', function (cb) {
    rimraf('./dist', cb);
});

//Extensions
gulp.task('ng2-prism', ['clean-libs'], function () {
    return gulp.src([paths.npm + 'ng2-prism/**/*.js', paths.npm + 'ng2-prism/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/ng2-prism/'));
});
gulp.task('prismjs', ['clean-libs'], function () {
    return gulp.src([paths.npm + 'ng2-prism/node_modules/prismjs/**/*.js', paths.npm + 'ng2-prism/node_modules/prismjs/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/prismjs/'));
});

gulp.task('ng2-dragula', ['clean-libs'], function () {
    return gulp.src([paths.npm + 'ng2-dragula/**/*.js', paths.npm + 'ng2-dragula/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/ng2-dragula/'));
});
gulp.task('dragula', ['clean-libs'], function () {
    return gulp.src([paths.npm + 'dragula/dist/**/*.js', paths.npm + 'dragula/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/dragula/'));
});


gulp.task('ng2-bootstrap', ['clean-libs'], function () {
    return gulp.src([paths.npm + 'ng2-bootstrap/**/*.js', paths.npm + 'ng2-bootstrap/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/ng2-bootstrap/'));
});

gulp.task('rxjs', ['clean-libs', 'ng2-prism', 'prismjs', 'ng2-bootstrap', 'ng2-dragula', 'dragula'], function () {
    return gulp.src([paths.npm + 'rxjs/**/*.js', paths.npm + 'rxjs/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'rxjs/'));
});

gulp.task('rxjs-min', ['clean-libs'], function () {
    return gulp.src(paths.npm + 'rxjs/**/*.js')
        .pipe(uglify())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(gulp.dest(paths.lib + 'rxjs/'));
});

gulp.task('copy-libs', ['rxjs'], function () {
    return gulp.src(libs)
        .pipe(gulp.dest(paths.lib));
});

gulp.task('clean-libs', function (cb) {
    rimraf('./wwwroot/lib', cb);
});

gulp.task('lint-typescript', function () {
    gulp.src(['app/**/*.ts'])
        .pipe(tslint())
        .pipe(tslint.report('verbose'));
});

gulp.task('typescript-transpile', ['lint-typescript'], function () {
    var tsResult = tsProject.src()
        .pipe(typescript(tsProject));
    return tsResult.js.pipe(gulp.dest(paths.scripDist + "/app/"));
});

gulp.task('watch-web', function () {
    gulp.watch('app/**/*.ts', ['copy-scripts']);
});
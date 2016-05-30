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
    paths.npm + 'moment/moment.js',
    paths.npm + 'jquery/dist/jquery.js',
    paths.npm + 'jquery-validation/dist/jquery.validate.js',
    paths.npm + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
    paths.npm + 'bootstrap/dist/js/bootstrap.js',

    paths.npm + 'moment/min/moment.min.js',
    paths.npm + 'jquery/dist/jquery.min.js',
    paths.npm + 'jquery-validation/dist/jquery.validate.min.js',
    paths.npm + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js',
    paths.npm + 'bootstrap/dist/js/bootstrap.min.js',

];

var libsToCopy = [
    "moment",
    "@angular",
    "rxjs",

]

var libsExtToCopy = [
    "ng2-bootstrap",
    "ng2-prism",
    "prismjs",
    "ng2-dragula",
    "dragula"
]

var tsProject = typescript.createProject('app/tsconfig.json');

var moduleName = "MoM.CMS";

gulp.task('libs-clean', function (cb) {
    rimraf('./wwwroot/lib', cb);
});

gulp.task('libs-copy', ['libs-clean', 'rxjs', 'angular2', 'core-js', 'zone.js', 'reflect-metadata', 'systemjs', 'ng2-prism', 'prismjs', 'ng2-bootstrap', 'ng2-dragula', 'dragula'], function () {
    return gulp.src(libs)
        .pipe(gulp.dest(paths.lib));
});

gulp.task('angular2', ['libs-clean'], function () {
    return gulp.src([paths.npm + '@angular/**/*.js', paths.npm + '@angular/**/*.map'])
        .pipe(gulp.dest(paths.lib + '@angular/'));
});

gulp.task('rxjs', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'rxjs/**/*.js', paths.npm + 'rxjs/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'rxjs/'));
});

gulp.task('core-js', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'core-js/**/*.js', paths.npm + 'core-js/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'core-js/'));
});

gulp.task('zone.js', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'zone.js/**/*.js', paths.npm + 'zone.js/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'zone.js/'));
});

gulp.task('reflect-metadata', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'reflect-metadata/**/*.js', paths.npm + 'reflect-metadata/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'reflect-metadata/'));
});

gulp.task('systemjs', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'systemjs/**/*.js', paths.npm + 'systemjs/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'systemjs/'));
});

// extensions
gulp.task('ng2-bootstrap', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'ng2-bootstrap/**/*.js', paths.npm + 'ng2-bootstrap/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/ng2-bootstrap/'));
});

gulp.task('ng2-prism', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'ng2-prism/**/*.js', paths.npm + 'ng2-prism/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/ng2-prism/'));
});
gulp.task('prismjs', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'ng2-prism/node_modules/prismjs/**/*.js', paths.npm + 'ng2-prism/node_modules/prismjs/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/prismjs/'));
});

gulp.task('ng2-dragula', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'ng2-dragula/**/*.js', paths.npm + 'ng2-dragula/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/ng2-dragula/'));
});
gulp.task('dragula', ['libs-clean'], function () {
    return gulp.src([paths.npm + 'dragula/dist/**/*.js', paths.npm + 'dragula/**/*.map'])
        .pipe(gulp.dest(paths.lib + 'extensions/dragula/'));
});


gulp.task('watch-web', function () {
    gulp.watch('app/**/*.ts', ['app-copy']);
});

gulp.task('app-clean-wwwroot', function (cb) {
    rimraf('./wwwroot/app', cb);
});

gulp.task('app-clean-dist', function (cb) {
    rimraf('./dist', cb);
});

gulp.task('app-copy', ['app-typescript-transpile', 'app-copy-systemjs.config'], function () {
    gulp.src(paths.scripDist + "/app/**/*.js")
    .pipe(gulp.dest(paths.scriptDestination))
});

gulp.task('app-copy-systemjs.config', function () {
    gulp.src('app/systemjs.config.js')
    .pipe(gulp.dest(paths.scriptDestination))
})

gulp.task('app-lint-typescript', ['app-clean-dist', 'app-clean-wwwroot'], function () {
    gulp.src(['app/**/*.ts'])
        .pipe(tslint({ configuration: "../../tslint.json" }))
        .pipe(tslint.report('verbose'));
});

gulp.task('app-typescript-transpile', ['app-lint-typescript'], function () {
    var tsResult = tsProject.src()
        .pipe(typescript(tsProject));
    return tsResult.js.pipe(gulp.dest(paths.scripDist + "/app/"));
});


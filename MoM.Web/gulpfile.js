/// <binding AfterBuild='copy-module' ProjectOpened='watch-core' />
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

var tsProject = typescript.createProject('app/tsconfig.json');

var moduleName = "MoM.CMS";

var paths = {
    scripDist: "./dist",
    scriptDestination: "./wwwroot/app/"
}

gulp.task('copy-scripts', ['typescript-transpile'], function () {
    gulp.src(paths.scripDist + "/app/**/*.js")
    .pipe(gulp.dest(paths.scriptDestination))
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
gulp.task('watch-core', function () {
    gulp.watch('app/**/*.ts', ['copy-scripts']);
});
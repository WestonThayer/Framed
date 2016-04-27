"use strict";
 
var gulp = require("gulp");
var sass = require("gulp-sass");
var imagemin = require("gulp-imagemin");
var pngquant = require("imagemin-pngquant");
 
gulp.task("sass", function () {
    return gulp.src("./scss/**/*.scss")
        .pipe(sass().on("error", sass.logError))
        .pipe(gulp.dest("./css"));
});
 
gulp.task("sass-watch", function () {
  gulp.watch("./scss/**/*.scss", ["sass"]);
});

gulp.task("minify-images", function() {
    return gulp.src("assets/*.png")
        .pipe(imagemin({ use: [pngquant()] }))
        .pipe(gulp.dest("distassets/"));
});
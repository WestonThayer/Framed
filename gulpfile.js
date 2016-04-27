"use strict";
 
var gulp = require("gulp");
var sass = require("gulp-sass");
var imagemin = require("gulp-imagemin");
var pngquant = require("imagemin-pngquant");
var autoprefixer = require("gulp-autoprefixer");
 
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

gulp.task("autoprefix", function() {
    gulp.src("css/styles.css")
        .pipe(autoprefixer({ browsers: ["last 2 versions"], cascade: false }))
        .pipe(gulp.dest("css/"));
});
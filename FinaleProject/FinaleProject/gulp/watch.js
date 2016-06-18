(function () {
    'use-strict';
    var gulp = require('gulp'),
        watch = require('gulp-watch'),
        _ = require('lodash'),
        paths = gulp.paths;

    gulp.task('watch', function () {
        watch(paths.styles.src, _.debounce(function () {
            gulp.start('styles');
        }, 2000));

        watch(paths.templates.src, _.debounce(function () {
            gulp.start('templateCache');
        }, 2000));
    });
}());
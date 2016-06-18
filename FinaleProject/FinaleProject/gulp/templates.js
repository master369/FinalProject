(function () {
    'use-strict';
    var gulp = require('gulp'),
        del = require('del'),
        templateCache = require('gulp-angular-templatecache'),
        runSequence = require('run-sequence'),
        paths = gulp.paths;

    gulp.task('templateCache', function () {
        runSequence(['cleanTemplates', 'buildTemplates']);
    });

    gulp.task('cleanTemplates', function () {
        del([paths.templates.dest + '/templates.js']);
    });

    gulp.task('buildTemplates', function () {
        gulp.src(paths.templates.src)
            .pipe(templateCache({
                standalone: true
            }))
            .pipe(gulp.dest(paths.templates.dest));
    });
}());
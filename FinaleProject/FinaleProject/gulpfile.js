(function () {
    'use-strict';

    var gulp = require('gulp'),
        runSequence = require('run-sequence'),
        pathPrefix = './';

    gulp.paths = {
        templates: {
            src: pathPrefix + 'templates/**/*.html',
            dest: pathPrefix + 'scripts/app/'
        },
        styles: {
            pathPrefix: pathPrefix,
            src: pathPrefix + 'styles/**/*.scss',
            dest: pathPrefix + 'styles/'
        }
    };

    require('require-dir')('./gulp');

    gulp.task('serve', function () {
        runSequence(['templateCache', 'styles', 'watch']);
    });

}());
(function () {
    'use-strict';
    var gulp = require('gulp'),
        del = require('del'),
        runSequence = require('run-sequence'),
        globbing = require('gulp-css-globbing'),
        autoprefixer = require('gulp-autoprefixer'),
        sass = require('gulp-sass'),
        cssmin = require('gulp-minify-css'),
        concat = require('gulp-concat'),
        $ = require('gulp-load-plugins')(),
        paths = gulp.paths;

    /*function transformStyles(src, appCss, vendorCss, dest, done) {
        var sassOptions = {
            style: 'expanded'
        };

        var injectFiles = gulp.src(src, {read: false});

        var injectOptions = {
            transform: function (filePath) {
                return '@import \'' + filePath + '\';';
            },
            starttag: '// injector',
            endtag: '// endinjector',
            addRootSlash: false
        };

        var indexFilter = $.filter('index.scss', {restore: true});

        gulp.src([appCss, vendorCss])
            .pipe(indexFilter)
            .pipe($.inject(injectFiles, injectOptions))
            .pipe(indexFilter.restore)
            .pipe($.sass(sassOptions).on('error', done))
            .pipe($.autoprefixer())
            .pipe(gulp.dest(dest)).on('end', done);
    }

    gulp.task('styles', function () {
        runSequence(['cleanStyles', 'buildStyles']);
    });

    gulp.task('cleanStyles', function () {
        del([paths.styles.dest + '/allStyles.css']);
    });

    gulp.task('buildStyles', function (done) {
        var pathPrefix = paths.styles.pathPrefix;

        transformStyles([
                pathPrefix + '/styles/!**!/!*.scss',
                '!' + pathPrefix + '/styles/index.scss',
                '!' + pathPrefix + '/styles/!**!/_variables.scss',
                '!' + pathPrefix + '/styles/!**!/_typography-variables.scss'
            ],
            pathPrefix + '/styles/index.scss',
            pathPrefix + '/styles/vendor.scss',
            paths.styles.dest,
            done);
    });*/

    gulp.task('styles', function () {
        runSequence(['cleanStyles', 'buildStyles']);
    });

    gulp.task('cleanStyles', function () {
        del([paths.styles.dest + '/allStyles.css']);
    });

    gulp.task('buildStyles', function () {
        gulp.src(paths.styles.src)
            .pipe(sass({
                sourceMap: true,
                errLogToConsole: true
            }).on('error', sass.logError))
            .pipe(autoprefixer("last 3 version", "safari 5", "ie 9"))
            .pipe(concat('allStyles.css'))
            .pipe(cssmin())
            .pipe(gulp.dest(paths.styles.dest));
    });

}());
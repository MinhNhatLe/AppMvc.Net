var gulp = require('gulp'),
    cssmin = require("gulp-cssmin")
rename = require("gulp-rename");
const sass = require('gulp-sass')(require('sass'));

// mặc định là tên default 
// có thể đặt tên khác
gulp.task('default', function () {
    return gulp.src('assets/scss/site.scss')
        .pipe(sass().on('error', sass.logError))
        // .pipe(cssmin())
        .pipe(rename({
            // suffix: ".min"
        }))
        .pipe(gulp.dest('wwwroot/css/'));
});


// này giúp mình saveAs thì nó sẽ lưu lại luôn và chạy gulp
gulp.task("watch", function () {
    gulp.watch('assets/scss/*.scss', gulp.series('default'));
});    
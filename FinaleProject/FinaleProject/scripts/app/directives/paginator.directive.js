
App.directive('paginator', function ($state) {
        return {
            restrict: 'EA',
            templateUrl: 'paginator.html',
            replace: true,
            scope: {
                itemsCount: '=',
                totalItems: '=',
                currentPage: '='
            },
            link: function (scope) {
                var PAGES_BY_SIDES = 2;
                scope.pages = [];
                scope.SEPARATOR = '...';
                scope.PREV = 'prev';
                scope.NEXT = 'next';

                scope.$watch('currentPage', build);
                scope.$watch('itemsCount', build);
                scope.$watch('totalItems', build);

                scope.changePage = function (page) {
                    if (page === scope.SEPARATOR) {
                        return;
                    } else if (page === scope.PREV && scope.currentPage !== 1) {
                        scope.currentPage--;
                    } else if (page === scope.NEXT && scope.currentPage !== scope.totalPages) {
                        scope.currentPage++;
                    } else if (angular.isNumber(page)) {
                        scope.currentPage = page;
                    }
                    $state.go('.', { page: scope.currentPage }, { notify: false });
                };

                function build() {
                    var totalPages = Math.ceil(scope.totalItems / scope.itemsCount),
                        currentPage = scope.currentPage,
                        i;
                    scope.pages.splice(0, scope.pages.length);

                    if (currentPage > totalPages) {
                        scope.currentPage = totalPages || 1;
                        return;
                    }

                    if (totalPages <= 10) {
                        makePart(1, totalPages);
                    } else {
                        if (currentPage < PAGES_BY_SIDES * 3) {
                            makePart(1, PAGES_BY_SIDES * 3 + 1);
                            makeSeparator();
                            scope.pages.push(totalPages);
                        } else if (currentPage < totalPages - PAGES_BY_SIDES * 2) {
                            scope.pages.push(1);
                            makeSeparator();
                            makePart(currentPage - PAGES_BY_SIDES, currentPage + PAGES_BY_SIDES);
                            makeSeparator();
                            scope.pages.push(totalPages);
                        } else {
                            scope.pages.push(1);
                            makeSeparator();
                            makePart(totalPages - PAGES_BY_SIDES * 3, totalPages);
                        }
                    }

                    scope.currentPage = currentPage;
                    scope.totalPages = totalPages;

                    function makeSeparator() {
                        scope.pages.push(scope.SEPARATOR);
                    }

                    function makePart(start, end) {
                        for (i = start; i <= end; i++) {
                            scope.pages.push(i);
                        }
                    }
                }
            }
        };
    });



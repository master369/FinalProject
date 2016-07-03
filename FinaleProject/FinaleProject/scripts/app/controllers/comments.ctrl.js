App.controller('CommentsCtrl', function ($scope, $http, $stateParams, MainUser, SelectedPhoto) {
    var vm = this,
        myName = MainUser.name,
        photoId = SelectedPhoto.id;

    vm.commentsList = [];

    init();

    function init() {
        $http.post('Views/Comment/Comments.cshtml', {
            typeOfChange: 'getAll',
            photoId: photoId,
            username: myName
        }).then(prepareComments);
    }

    vm.addComment = function () {
        if (!myName) {
            return;
        }
        $http.post('Views/Comment/Comments.cshtml', {
            typeOfChange: 'post',
            text: vm.commentText,
            photoId: photoId,
            username: myName
        }).then(prepareComments);
    };

    vm.deleteComment = function (commentId) {
        $http.post('Views/Comment/Comments.cshtml', {
            typeOfChange: 'delete',
            commentId: commentId,
            username: myName
        }).then(function (res) {
            var commentIndex = _.findIndex(vm.commentsList, function (comment) {
                return comment.Id === commentId;
            });
            vm.commentsList.splice(commentIndex, 1);
        });
    };

    function prepareComments(res) {
        var data = res.data;
        _.forEach(data.CommentsList, function (comment) {
            comment.Date = new Date(prepareDate(comment.Date));
        });
        vm.commentsList = data.CommentsList;

        vm.commentText = '';
        $scope.commentForm.$setPristine();
        $scope.commentForm.$setUntouched();
    }

    function prepareDate(serverDate) {
        var firstBracketPos = _.indexOf(serverDate, '(') + 1,
            lastBracketPos = _.indexOf(serverDate, ')');
        return parseInt( serverDate.substring(firstBracketPos, lastBracketPos), 10);
    }
});
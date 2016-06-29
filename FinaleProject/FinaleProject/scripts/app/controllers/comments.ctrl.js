App.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('altTheme')
      .primaryPalette('purple');
})
.controller('CommentsCtrl', function ($scope) {
    var imagePath = '/Views/Koala.jpg';
    $scope.messages = [
      {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:02',
          notes: "Cool image"
      },
      {
          face: imagePath,
          what: 'Сool',
          who: 'User1',
          when: '3:18',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User2',
          when: '3:28',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool3',
          who: 'User3',
          when: '4:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User4',
          when: '3:55',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      }, {
          face: imagePath,
          what: 'Сool',
          who: 'User',
          when: '3:08',
          notes: "Cool image"
      },
    ];
});
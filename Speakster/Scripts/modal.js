$(".profilePictureButton").click(function () {
    $("#profilePictureModal").modal().appendTo("body");
});
//teacherInfoButton precisa ser uma classe pois são vários buttons em uma View
$(".teacherInfoButton").click(function () {
    $("#teacherInfoModal").modal().appendTo("body");
});

$(document).ready(function () {
    $(".toggle-password").click(function () {
        var target = $($(this).data("target"));
        var targetType = target.attr('type');

        if (targetType === 'password') {
            target.attr('type', 'text');
            $(this).find('i').removeClass('bi-eye-slash').addClass('bi-eye');
        } else {
            target.attr('type', 'password');
            $(this).find('i').removeClass('bi-eye').addClass('bi-eye-slash');
        }
    });
});
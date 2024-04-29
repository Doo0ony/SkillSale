$(document).ready(function () {
    $("#favoriteButton").click(function () {
        var button = $(this);
        var valueToSend = button.data("value");

        $.ajax({
            url: '/Vacancies/AddToFavoriteList',
            type: 'POST',
            data: { value: valueToSend },
            success: function (result) {
                var icon = button.find("i");
                if (icon.hasClass("bi-heart")) {
                    icon.removeClass("bi-heart");
                    icon.addClass("bi-heart-fill text-success");
                } else {
                    icon.removeClass("bi-heart-fill text-success");
                    icon.addClass("bi-heart");
                }
            },
            error: function () {
            }
        });
    });
});


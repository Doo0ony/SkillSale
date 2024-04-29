$(document).ready(function () {
    $("#CandidateVacancy").click(function () {
        var button = $(this);
        var valueToSend = button.data("value");
        $.ajax({
            url: '/Vacancies/AddOrRemoveCandidateFromVacancy',
            type: 'POST',
            data: { value: valueToSend },
            success: function (result) {
                if (button.hasClass("btn-primary")) {
                    button.removeClass("btn-primary");
                    button.addClass("btn-success");
                    button.text("\u041E\u0442\u043C\u0435\u043D\u0430");
                } else {
                    button.removeClass("btn-success");
                    button.addClass("btn-primary");
                    button.text("\u041F\u043E\u0434\u0430\u0442\u044C \u0437\u0430\u044F\u0432\u043A\u0443");
                }
            },
            error: function () {
            }
        });
    });
});


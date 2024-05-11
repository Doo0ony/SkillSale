$(document).ready(function () {

    $(document).ready(function () {
        $(".dropdown-menu .dropdown-item").click(function () {
            var selectedStatus = $(this).text();
            $("#dropdownMenuButton").text(selectedStatus);
        });
    });

    $(".status-button-vacancy").click(function () {
        var selectedStatus = $(this).data("value");
        var itemId = $(this).data("id");
        var moderationStatusInfo = $(this).closest(".nested-block").find(".moderation-status-info");

        moderationStatusInfo.text($(this).text());
        $.ajax({
            type: "POST",
            url: "/ControlPanel/UpdateModerationStatusVacancyes/",
            data: { id: itemId, selectedStatus: selectedStatus },
            success: function (response) {
                console.log("AJAX request successful");
            },
            error: function (xhr, status, error) {
                console.error("AJAX request error:", error);
            }
        });
    });
});
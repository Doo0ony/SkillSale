$(document).ready(function () {

    $(document).ready(function () {
        $(".dropdown-menu .dropdown-item").click(function () {
            var selectedStatus = $(this).text();
            $("#dropdownMenuButton").text(selectedStatus);
        });
    });

    $(".changeRole-button").click(function () {
        var selectedRole = $(this).data("value");
        var userId = $(this).data("id");
        var roleInfo = $(this).closest(".nested-block").find(".moderation-status-info");

        roleInfo.text($(this).text());
        $.ajax({
            type: "POST",
            url: "/UsersRole/SetRole/",
            data: { id: userId, role: selectedRole },
            success: function (response) {
                console.log("AJAX request successful");
            },
            error: function (xhr, status, error) {
                console.error("AJAX request error:", error);
            }
        });
    });
});
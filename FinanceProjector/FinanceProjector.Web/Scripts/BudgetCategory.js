$(document).ready(function () {
    $('#AddCategoryButton').on('click', function () {
        $('#AddCategory').show();
    });

    $('#CancelCategory').on('click', function () {
        $('#AddCategory').hide();
    });

    $('#SaveCategory').on('click', function () {
        $.ajax({
            type: "POST",
            url: "/BudgetCategory/AddCategory",
            data: { userName: $('#username').val(), category: $('#Name').val(), parent: $('#Parent').val() }
        }).done(function (result) {
            $("#CategoryList").html(result);
            $('#Name').val('').focus();
        }).error(function (result, status, error) {
            var message = error;
            alert(message);
        });
    });
});
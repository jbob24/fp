$(document).ready(function () {
    $('#CancelMatch').on('click', function () {
        //$('#AddCategory').hide();
        // clear fields
    });

    $('#SaveMatch').on('click', function () {
        var match = {};

        match.CategoryName = $('#categoryName').val();
        match.TransactionType = $('#TransactionType').val();
        match.Amount = $('#Amount').val();
        match.Name = $('#Name').val();
        match.Comments = $('#Comments').val();
        match.PayeeID = $('#PayeeID').val();

        $.ajax({
            type: "POST",
            url: "/BudgetCategory/AddTransactionMatch",
            data: match
        }).done(function (result) {
            $("#MatchList").html(result);
        }).error(function (result, status, error) {
            var message = error;
            alert(message);
        });
    });
});
$(document).ready(function () {
    $('#CancelMatch').on('click', function () {
        resetAddMatchForm();
    });

    $('.AddMatchButton').on('click', function () {
        $('#CategoryItem').val($(this).data('categoryitem'));
        $('#AddMatch').show();
    });

    $('#SaveMatch').on('click', function () {
        var match = {};
        var categoryItem = $('#CategoryItem').val();

        match.CategoryName = $('#categoryName').val();
        match.TransactionType = $('#TransactionType').val();
        match.Amount = $('#Amount').val();
        match.Name = $('#Name').val();
        match.Comments = $('#Comments').val();
        match.PayeeID = $('#PayeeID').val();
        match.CategoryItem = categoryItem;

        $.ajax({
            type: "POST",
            url: "/BudgetCategory/AddTransactionMatch",
            data: match
        }).done(function (result) {
            $('#' + categoryItem + '.MatchList').html(result);
            resetAddMatchForm();
            //$("#MatchList").html(result);
        }).error(function (result, status, error) {
            var message = error;
            alert(message);
        });
    });

    function resetAddMatchForm() {
        $('#CategoryItem').val('');
        $('#AddMatch').hide();
        // clear fields
    }
});
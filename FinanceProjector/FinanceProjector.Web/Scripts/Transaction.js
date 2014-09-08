$(document).ready(function() {
    $('#importBtn').on('click', function() {
        var file = document.getElementById('qfxfile').files[0];

        if (file) {
            // create reader
            var reader = new FileReader();
            reader.readAsText(file);
            reader.onload = function (e) {

                $.ajax({
                    type: "POST",
                    url: "/Transaction/ImportFile",
                    data: { username: $('#username').val(), file: btoa(e.target.result) }
                }).done(function (result) {
                    $("#transactionlist").html(result);
                    $('#qfxfile').val('');
                    alert('Data imported successfully');
                }).error(function (result, status, error) {
                    var message = error;
                    alert(message);
                });


                // browser completed reading file - display it
                //alert(e.target.result);
            };
        }

        //alert(fileStr);
    });
});
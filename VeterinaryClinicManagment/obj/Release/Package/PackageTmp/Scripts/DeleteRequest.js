
$('.delete-link').on('click', function () {
    if (confirm('Confirmer la suppression ?')) {
        $.delete($(this).data().url, function (data) {
            $('#' + data.id).remove();
        })
    }
});  
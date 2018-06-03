
$('.delete-link').on('click', function () {
    if (confirm('Confirmer la suppression ?')) {
        $.delete($(this).data().url, function (data) {
            $('#' + data.id).remove();
        })
    }
});  

$('.delete-link-animal').on('click', function () {
    if (confirm('Les consultations et chirgurgies seront également supprimées. Confirmer ?')) {
        $.delete($(this).data().url, function (data) {
            $('#' + data.id).remove();
        })
    }
});  
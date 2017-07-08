$(document).ready(function () {
    var list = $('#list')

    $.get('/api/ls').done(function (directoryList) {
        $.each(directoryList.files, function (index, file) {
            var elem = $('<LI>').text(file)
            list.append(elem)
        })
    })
})

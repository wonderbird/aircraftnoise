$(document).ready(function () {
    $.get('/api/directorylist').done(function (directoryList) {
        var list = $('#list')
        $.each(directoryList.files, function (index, file) {
            var elem = $('<LI>').text(file)
            list.append(elem)
        })
    })

    $.get('/api/version').done(function (versionInfo) {
        var version = $('#version')
        version.text(versionInfo.version)
    })
})

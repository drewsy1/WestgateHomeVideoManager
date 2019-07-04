var detailViewID;
var detailViewData;
var progressBar = $('<div/>').attr("class","modal-content").append(
    $('<div/>').attr("class","modal-body").append(
        $('<div/>').attr("class","progress").append(
            $('<div/>').attr({
                class: "progress-bar progress-bar-striped progress-bar-animated",
                role: "progressbar",
                "aria-valuenow": "100",
                "aria-valuemin": "0",
                "aria-valuemax": "100",
                style:"width: 100%"
            })
        )
    )
)

function SetDetailViewID(id) {
    detailViewID = id;
}

function CreateModal(id, isLarge) {
    var modal = $('<div/>').attr({
        class: "modal fade",
        id: id,
        tabindex: "-1",
        role: "dialog",
        "aria-labelledby": id+"Label",
        "aria-hidden": "true",
        "data-backdrop": "static"
    });

    var modalDialog = $('<div/>').attr({
        class: ((isLarge) ? "modal-dialog modal-lg modal-dialog-centered":"modal-dialog modal-dialog-centered"),
        role: "document"
    });

    var modalContent = $('<div/>').attr("class", "modal-content");
    var modalBody = $('<div/>').attr("class", "modal-body");

    modal.append(modalDialog.append(modalContent.append(modalBody.append(progressBar))));

    return modal;
}

function ajaxPost(url, data) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        headers: { 'RequestVerificationToken': antiforgeryToken },
        success: function(data){
            window.location.replace(data);
        }
    });
}

$('#pageCardBody').on('shown.bs.modal',
    function (event) {
        //console.log("Showing modal");
        $.get(detailViewID,
            function (data) {
                $(event.target).find('.modal-content').replaceWith(data);

                if ($('#deleteModalBtn').length) {
                    $('#deleteModalBtn').remove();
                }
            });
    });

$('#pageCardBody').on('hidden.bs.modal',
    function (event) {
        //console.log("Hiding modal");
        $(event.target).find('.modal-content').replaceWith(progressBar.clone());
    });
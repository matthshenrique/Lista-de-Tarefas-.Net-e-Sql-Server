// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deleteTarefa(i) {
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            id: i
        },
        success: function () {
            window.location.reload();
        }
    });
}

function updateTarefa(i) {

    $.ajax({
        url: 'Home/PopulateForm',
        type: 'GET',
        data: {
            id: i
        },
        dataType: 'json',
        success: function (response) {
            $("#Tarefa_Descricao").val(response.tarefa.descricao);
            $("#Tarefa_Id").val(response.tarefa.id);
            $("#form-button").val("Alterar Tarefa");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}
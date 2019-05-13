// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(function () {
    const dateFormat = "M/D/YYYY h:mm:ss A";

    $("#dateTimePicker_SourceDateBurned").datetimepicker({
        format: dateFormat
    });
    $("#dateTimePicker_SourceDateRipped").datetimepicker({
        format: dateFormat
    });
});
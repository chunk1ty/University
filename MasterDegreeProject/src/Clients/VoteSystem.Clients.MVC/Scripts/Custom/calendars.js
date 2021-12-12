(function ($) {
    "use strict";

    function calendarSettings(selector) {
        $(selector).daterangepicker({
            singleDatePicker: true,
            timePicker: true,
            linkedCalendars: false,
            autoUpdateInput: false,
            autoApply: true,
            calender_style: "picker_4",
            cancelClass: "btn btn-dark"
        });
    }

    calendarSettings('#start-date');
    calendarSettings('#end-date');

    $('#start-date').on('apply.daterangepicker', function (ev, picker) {
        $('#start-date').val(picker.startDate.format('MM/DD/YYYY h:mm A'));
    });

    $('#end-date').on('apply.daterangepicker', function (ev, picker) {
        $('#end-date').val(picker.startDate.format('MM/DD/YYYY h:mm A'));
    });
}($));
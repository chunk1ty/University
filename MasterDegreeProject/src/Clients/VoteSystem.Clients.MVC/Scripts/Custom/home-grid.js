$(function ($) {
    "use strict";

    $(document).ready(function () {
        $('#example').dataTable({
            columnDefs: [{
                targets: [0],
                searchable: false,
                sortable: false
            }, {
                targets: [1]
            }, {
                targets: [2],
                searchable: false,
                sortable: false
            }, {
                targets: [3],
                searchable: false
            }],
            pagingType: "full_numbers",
            language: {
                search: "Търсене по име на система: ",
                info: "Страница _PAGE_ от _PAGES_ страници.",
                lengthMenu: "Покажи _MENU_ системи.",
                emptyTable: "Няма налични системи.",
                paginate: {
                    first: "Първа",
                    last: "Последна",
                    next: "Следваща",
                    previous: "Предишна"
                },
            }
        });
    });
}($));
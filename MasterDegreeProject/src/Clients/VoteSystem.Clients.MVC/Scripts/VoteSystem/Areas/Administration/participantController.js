$(function ($) {
    "use strict";
   
    //grid settings
    var table = $('#user-grid').DataTable({
        columnDefs: [{
            targets: [0],
            searchable: false,
            orderable: false,
            className: 'dt-body-center'
        }, {
            targets: [1],
            searchable: true,
            sortable: true
        }, {
            targets: [2],
            searchable: true,
            sortable: true
        }, {
            targets: [3],
            searchable: true,
            sortable: true
        }, {
            targets: [4],
            searchable: true,
            sortable: true
        }],
        pagingType: "full_numbers",
        aaSorting: [],
        language: {
            search: "Търсене по колони: ",
            info: "Страница _PAGE_ от _PAGES_ страници.",
            lengthMenu: "Покажи _MENU_ потребителя.",
            emptyTable: "Няма налични потребители.",
            paginate: {
                first: "Първа",
                last: "Последна",
                next: "Следваща",
                previous: "Предишна"
            }
        }
    });
    
    $('#select-all-checkbox').on('click', function () {      
        var rows = table
            .rows({ 'search': 'applied' })
            .nodes();

        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });
   
    // Handle click on checkbox to set state of "Select all" control
    $('#user-grid tbody').on('change', 'input[type="checkbox"]', function () {          
        if (!this.checked) {
            var el = $('#select-all-checkbox').get(0);   
            if (el && el.checked && ('indeterminate' in el)) {
                el.indeterminate = true;
            }
        }
    });
}($));
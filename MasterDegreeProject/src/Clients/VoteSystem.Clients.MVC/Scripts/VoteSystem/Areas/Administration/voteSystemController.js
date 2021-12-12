$(function ($) {
    "use strict";

    var selectedVoteSystemId;

    //grid settings
    $('#admin-grid').dataTable({
        columnDefs: [{
            targets: [0],
            searchable: false,
        }, {
            targets: [1],
        }, {
            targets: [2],
            searchable: false,
            sortable: false
        }, {
            targets: [3],
            searchable: false,
            sortable: false
        }, {
            targets: [4],
            searchable: false,
            sortable: false
        }],
        pagingType: "full_numbers",
        language: {
            search: "Търсене по име на система: ",
            info: "Страница _PAGE_ от _PAGES_ страници.",
            lengthMenu: "Покажи _MENU_ системи.",
            paginate: {
                first: "Първа",
                last: "Последна",
                next: "Следваща",
                previous: "Предишна"
            }
        }
    });

    $('tbody').on('click', '.delete-btn', function () {
        selectedVoteSystemId = $(this)
                .closest('tr')
                .children()
                .first();

        $('#modal-content').text(
            $(this)
                .closest('tr')
                .children()
                .first()
                .next()
                .next()
                .text());
    });

    $('#delete-vote-system-btn').on('click', function () {
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            type: 'POST',
            url: '/admin/votesystem/delete',
            data: {
                __RequestVerificationToken: token,
                 voteSystemId: selectedVoteSystemId.val()
            },
            success: function () {
                $('#cancel-btn').trigger('click');

                $(selectedVoteSystemId).parent().remove();
            },
            error: function (ex) {
                alert('Can not delete vote system!' + ex);
            }
        });
    });
}($));
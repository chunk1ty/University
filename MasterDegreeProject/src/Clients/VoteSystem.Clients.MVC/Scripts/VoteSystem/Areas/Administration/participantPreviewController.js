$(function ($) {
    "use strict";

    var isSend = false,
        voteSystemId = $('#vote-system-id').val(),
        sentEmailUrl = '/admin/participant/sentemails';

    //grid settings
    $('#user-preview-grid').DataTable({
        columnDefs: [{
            targets: [0],
            searchable: true,
            sortable: true
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

    $('#sent-email-btn').on('click', function () {
        if (isSend) {
            alert('Имейлите бяха изпратерни!');
        } else {
            $('body').prepend('<div id="site-spinner" />');

            var opts = {
                lines: 17,
                length: 0,
                width: 5,
                radius: 25,
                corners: 1,
                rotate: 0,
                color: '#1ABB9C',
                speed: 1,
                trail: 80,
                shadow: true,
                hwaccel: true,
                className: 'spinner',
                zIndex: 2e9,
                top: '45%',
                left: '50%'
            };

            var target = document.getElementById('site-spinner');
            var spinner = new Spinner(opts).spin(target);
            $('#votesystem-content').hide();

            $.ajax({
                type: 'GET',
                url: sentEmailUrl,
                data: { voteSystemId: voteSystemId },
                success: function (notification) {
                    $('.right_col').prepend(notification);
                    $('#sent-email-btn').addClass('disabled');
                    $('#votesystem-content').show();
                    $('#site-spinner').remove();
                    isSend = true;
                },
                error: function (ex) {
                    alert('Can not sent emails' + ex);
                    $('#votesystem-content').show();
                    $('#site-spinner').remove();
                }
            });
        }
    });
}($));
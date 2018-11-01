const uri = 'api/survey';
let surveys = null;
function getCount(data) {
    const el = $('#counter');
    let name = 'to-do';
    if (data) {
        if (data > 1) {
            name = 'to-dos';
        }
        el.text(data + ' ' + name);
    } else {
        el.html('No ' + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            $('#surveys').empty();
            getCount(data.length);
            $.each(data, function (key, item) {
                const checked = item.choice ? 'checked' : '';

                $('<tr><td>' + item.question + '</td>' +
                    '<td><input disabled="true" type="checkbox" ' + checked + '></td>' +
                    '<td><button onclick="editSurvey(' + item.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteSurvey(' + item.id + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#surveys'));
            });

            surveys = data;
        }
    });
}

function addSurvey() {
    const item = {
        'question': $('#add-survey').val(),
        'option': false
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#add-survey').val('');
        }
    });
}

function deleteSurvey(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

function editSurvey(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $('#edit-question').val(item.name);
            $('#edit-id').val(item.id);
            $('#edit-question')[0].checked = item.isComplete;
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const item = {
        'question': $('#edit-question').val(),
        'choice': $('#edit-question').is(':checked'),
        'id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}
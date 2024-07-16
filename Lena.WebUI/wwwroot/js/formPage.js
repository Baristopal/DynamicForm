$(document).ready(function () {
    $('#getModal').on('click', function () {
        debugger;
        $.ajax({
            url: '/EditForm',
            type: 'GET',
            success: function (result) {
                $('#modalContainer').html(result);
                $('#exampleModalCenter').modal('show');
            }

        });
    });



    $('#search').on('keyup', function () {
        debugger;
        var search = $('#search').val();
        var filter = {
            Name: search,
            Forms: forms,
        }
        $.ajax({
            url: '/SearchForms',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(filter),
            success: function (result) {
                debugger;
                var tbody = $('#formTableBody');
                tbody.empty();

                $.each(result, function (index, form) {
                    var row = '<tr>' +
                        '<th>' + form.id + '</th>' +
                        '<td>' + form.name + '</td>' +
                        '<td>' + form.description + '</td>' +
                        '<td><button type="button" class="btn btn-primary openDetail">Görüntüle</button></td>' +
                        '</tr>';
                    tbody.append(row);
                });
            },
        });

    });


    $('#formTableBody').on('click', '.openDetail', function () {
        var formId = $(this).closest('tr').find('th').text();
        $.ajax({
            url: `/forms/${formId}`,
            type: 'GET',
            data: { id: formId },
            success: function (result) {
                if (result.length > 0) {
                    window.open(`/forms/${formId}`, '_blank');
                } else {
                    $('#notLoadForm').toast('show');
                    console.log("hata");
                }
            }
        });
    });

    $('input[type="checkbox"]').prop('disabled', true);

    var selectedFields = [];

    $(document).on('click', '.custom-button', function () {

        var fieldName = $(this).attr('name');

        if (selectedFields.find(e => e.name === fieldName)) {
            selectedFields = $.grep(selectedFields, function (e) {
                return e.name !== fieldName;
            });
            // selectedFields = selectedFields.filter(e => e !== fieldName);
            console.log(selectedFields);
            $(this).removeClass('btn-info');
            $(this).addClass('btn-light');

            $("#field-" + fieldName + "-Required").prop('disabled', true);
            $("#field-" + fieldName + "-Required").prop('checked', false);
            return;
        }
        var dataType;
        if (fieldName === "Age") {
            dataType = "NUMBER";
        }
        else {
            dataType = "STRING";
        }

        var model = {
            name: fieldName,
            required: $(this).find('input[type="checkbox"]').is(':checked'),
            dataType: dataType
        }
        selectedFields.push(model);
        $(this).addClass('btn-info');
        $(this).removeClass('btn-light');

        $("#field-" + fieldName + "-Required").prop('disabled', false);

        console.log(selectedFields);

    });



    $(document).on('click', 'input[type="checkbox"]', function () {
        var fieldName = $(this).attr('name').split('-')[1];
        if (selectedFields.find(e => e.name === fieldName)) {

            var model = {
                name: fieldName,
                required: $(this).is(':checked')
            }
            selectedFields = selectedFields.filter(e => e.name !== fieldName);
            selectedFields.push(model);
            console.log(selectedFields);
        }

    });


    $(document).on('click', '.close', function () {
        $("#exampleModalCenter").modal("hide");
    });




    $(document).on('click', '#Save', function (event) {
        debugger;
        event.preventDefault();
        var data = {
            Name: $('#Name').val(),
            Description: $('#Description').val(),
            Fields: selectedFields
        }

        if ($('#Name').val() === "" || $('#Description').val() === "" || $(selectedFields).length === 0) {
            if ($('#Name').val() === "")
                $('#NameValidaton').text('Bu alan boş bırakılamaz');

            if ($('#Description').val() === "")
                $('#DescriptionValidation').text('Bu alan boş bırakılamaz');

            if ($(selectedFields).length === 0)
                $('#FieldsValidation').text('Bu alan boş bırakılamaz');
            return false;
        }
        else {

            $('#NameValidaton').text('');
            $('#DescriptionValidation').text('');
            $('#FieldsValidation').text('');
        }



        // debugger;
        $.ajax({
            type: 'POST',
            url: '/AddForm',
            data: JSON.stringify(data),

            contentType: 'application/json',
            success: function (response) {
                if (response.success === true) {
                    debugger;
                    $("#exampleModalCenter").modal("hide");
                    $('#successToast').toast('show');
                    location.reload();
                }
                else {
                    debugger;
                    $('#dangerToast').toast('show');
                }
            },
        });

    });


});
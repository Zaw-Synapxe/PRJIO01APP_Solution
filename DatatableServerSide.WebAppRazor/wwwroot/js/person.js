$(document).ready(function () {

    $("#personDatatable").DataTable({
        "processing": true,
        "serverSide": true, // enabling server side
        "filter": true, //set true for enabling searching
        "ajax": {
            "url": "/api/person",// ajax url to load content
            "type": "POST", // type of method to call
            "datatype": "json" // return datatype
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "First Name", "autoWidth": true }, // columns name and related settings
            { "data": "lastName", "name": "Last Name", "autoWidth": true },
        ]
    });

});
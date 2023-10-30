
let tablaDeConsultas;

$(document).ready(function () {

    crear_procesar_tablaDeConsultas();

});

function crear_procesar_tablaDeConsultas() {
    tablaDeConsultas = $('#tbconsultas').DataTable({
        ajax: {
            "url": "https://jsonplaceholder.typicode.com/posts",
            // "url": @Url.Action("vista","controlador")
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                alert("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                    return "<button type='button' onclick='facturarConsulta(" + data + ")' class='btn btn-success btn-sm' data-toggle='modal' data-target='.bd-example-modal-lg'><i class='fas fa-pen'></i><span class='material-symbols-outlined'>Done</span></button >" +
                        "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarFactura(" + data +
                        ")'><i class='fas fa-pen'><span class='material-symbols-outlined'>close</span></i></button>"
                },
                "ordenable": false,
                "searchable": false,
                "width": "150px",
            },
        ],
        columnDefs: [
            { className: "text-center", targets: "_all" },
            { width: "60px", targets: [0, 3] },
            { orderable: false, targets: [3] },
        ],
        autoHeight: true,
        responsive: true,
        processing: true,
        language: {
            // zeroRecords: "No hay registros disponibles.",
            infoEmpty: "Sin Registros",
            search: "Buscar",
            processing: "<div class='spinner-grow text-primary' role='status'><span class='sr-only'> Loading...</span ></div>",
            paginate: {
                next: "next",
                previous: "previous"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
            "lengthMenu": "Mostrando _MENU_ filas",
        }
    });
}

function facturarConsulta( id ) {
    console.log("facturando consulta " + id);
}

function cancelarFactura() {
    confirm("Está Seguro de Cancelar esta Cita?")
}
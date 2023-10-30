

let tablaDeFacturas;

$(document).ready(function () {

    crear_procesar_tablaDeFacturas();

});

function crear_procesar_tablaDeFacturas() {
    tablaDeFacturas = $('#tbfacturas').DataTable({
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
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='detallesFactura(" + data + ")'><i class='fas fa-pen'></i><span class='material-symbols-outlined'>preview</span></button>"
                },
                "ordenable": false,
                "searchable": false,
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

function detallesFactura(idFactura) {
    alert("Factura #" + idFactura + "detalles")
}

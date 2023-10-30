
// se declara como general para usar el metodo reload de ajax.
let tablaDeCompras;

$(document).ready(function () {

    crear_procesar_tabla_Compras(1);

});

function crear_procesar_tabla_Compras() {
    tablaDeCompras = $('#tbcompras').DataTable({
        ajax: {
            "url": "/Peticiones/Compras?id=" + 1, // esta direccion llama al controlador de c#
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
                "render": (data, type, row, meta) => {
                    return (`<button class='btn btn-primary btn-sm' type='button' onclick='verDetallesDeCompra(${data})'><i class='fas fa-pen'><span class='material-symbols-outlined'>preview</span></i></button>`);
                },
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

function verDetallesDeCompra(id) {
    alert("Detalles de compra id: " + id);
}
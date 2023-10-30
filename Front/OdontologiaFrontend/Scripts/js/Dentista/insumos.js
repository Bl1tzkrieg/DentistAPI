// variables globales 
let tablaDeInsumos;

$(document).ready(function () {

    crear_procesar_tablaDeInsumos();
    
});

function crear_procesar_tablaDeInsumos() {

    tablaDeInsumos = $('#tbinsumos').DataTable({
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
                    return "<button class='btn btn-info btn-sm' type='button' onclick='openModal(" + data + ")'><i class='fas fa-pen'></i><span class='material-symbols-outlined'>settings</span></button>" +
                        "<button class='btn btn-danger btn-sm' type='button' onclick='eliminarPedido(" + data +
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
        "dom": 'Bfrtip',
        "buttons": [
            {
                text: '+Registrar Nuevo Pedido',
                className: "btn btn-info",
                action: function (e, dt, node, config) {
                    openModal(0);
                }
            },
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
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });

    $(".btnCloseModal").click(function () {
        $("#FormModal").modal('hide')
    });

    // problemas con los estilos de botones solucion temporal
    $(".dt-button").attr("id", "boton");
    $(".dt-button").removeClass();
    $("#boton").addClass("btn btn-primary");

}

function openModal(id) {

    console.log(id);

    if (id != 0)
        confirm("Quieres Actualizar este Pedido");
    else {
        if (confirm("Quieres registrar un nuevo Pedido"))
            $("#FormModal").modal("show");
    }
     
}

function eliminarPedido() {
    confirm("Estas seguro de eliminar este pedido");
}
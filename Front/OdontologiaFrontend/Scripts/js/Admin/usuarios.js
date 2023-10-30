
let tablaDeUsuarios; // modificacion pendiente

$(document).ready(function () {

    crear_procesar_tabla_Usuarios();

    $("#agregarUsuario").click(function () {
        agregarNuevoUsuario();
    })

});

function crear_procesar_tabla_Usuarios(){

    tablaDeUsuarios = $('#tbusuarios').DataTable({
        ajax: {
            "url": "https://jsonplaceholder.typicode.com/posts",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                alert("Solicitud no puedo ser Procesada, Error al intentar recibir la data desde la Api");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='modificarUsuario(" + data + ")'><i class='fas fa-pen'><span class='material-symbols-outlined'>settings</span></i></button>" +
                        "<button class='btn btn-danger btn-sm' type='button' onclick='eliminarUsuario(" + data +
                        ")'><i class='fas fa-pen'><span class='material-symbols-outlined'>close</span></i></button>"
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

function modificarUsuario(idUsuario) {
    alert("Modificando usuario: " + idUsuario);
}

function eliminarUsuario(idUsuario) {
    alert("Eliminando usuario: " + idUsuario);
}

function agregarNuevoUsuario() {
    alert("Agregando nuevo usuario");
}
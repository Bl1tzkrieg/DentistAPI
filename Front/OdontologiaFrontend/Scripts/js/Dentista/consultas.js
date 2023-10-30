
let tablaDeConsultas;

$(document).ready(function () {

    crear_procesar_tablaDeConsultas();

    $("#btnConsultar").click(function () {
        // $("#prueba").removeAttr("disabled"); este se usa para eliminar
        $("#btnConsultar").attr("disabled", "");
        $("#btnConsultar").html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Loading...");
        consultarPersona();
    });

    $(".alert-warning").css("display", "none");

});

function crear_procesar_tablaDeConsultas() {

    tablaDeConsultas = $('#tbconsultas').DataTable({
        "ajax": {
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
                    return "<button class='btn btn-success btn-sm' type='button' onclick='aprobarConsulta(" + data + ")'><i class='fas fa-pen'></i><span class='material-symbols-outlined'>Done</span></button>" +
                        "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarConsulta(" + data +
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
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });

}


// este metodo registra citas o las confirma
function aprobarConsulta(id) {
    console.log(id)

    console.log("mostrar modal para confirmar")

}

function cancelarConsulta(data) {

    console.log(data);

    confirm("Estas seguro de Cancelar Esta CIta?");
}

function consultarPersona() {

    // procesando informacion del input

    if ($("#input_id").val() == "") {
        $(".alert-warning").css("display", "block");
        $("#btnConsultar").html("Consultar");
        $("#btnConsultar").removeAttr("disabled");
        setTimeout(function () {
            $(".alert-warning").css("display", "none");
        }, 5000);
    } else {

        $("#btnConsultar").html("Consultar");

        // si el input contiene la informacion

        console.log($("#input_id").val())

        $(".modalConsulta").modal('hide');
        $(".modalRegistro").modal('show');
    }

}

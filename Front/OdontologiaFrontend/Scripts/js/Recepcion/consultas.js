
let tablaDeConsultas;

$(document).ready(function () {

    crear_procesar_tablaDeConsultas(1);

    $("#btnConsultar").click(function () {
        // $("#prueba").removeAttr("disabled"); este se usa para eliminar
        $("#btnConsultar").attr("disabled", "");
        $("#btnConsultar").html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Loading...");
        consultarPersona();
    });

    $(".alert-warning").css("display", "none");
   
});

function crear_procesar_tablaDeConsultas(status) {
    tablaDeConsultas = $('#tbconsultas').DataTable({
        ajax: {
            url: "https://jsonplaceholder.typicode.com/posts",
            type: "GET",
            datatype: "json",
            dataSrc: "",
            error: function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                alert("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { data: "userId" },
            { data: "title" },
            { data: "body" },
            {
                data: "id",
                render: (data) => {
                    return "<button class='btn btn-success btn-sm' type='button' onclick='confirmarConsulta(" + data + ")'><i class='fas fa-pen'></i><span class='material-symbols-outlined'>done</span></button>" +
                        "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarConsulta(" + data +
                        ")'><i class='fas fa-pen'><span class='material-symbols-outlined'>close</span></i></button>"
                },
                searchable: false,
            },
        ],
        columnDefs: [
            { className: "text-center", targets: "_all" },
            { width: "60px", targets: [0, 3] },
            { orderable: false, targets:[3] },
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

function confirmarConsulta(id) {
    alert("confirmando Consulta " + id);
}

function cancelarConsulta(id) {
    alert("Cancelando Consulta " + id);
}

function reiniciarFormulario() {
    document.getElementById("formulario").reset();
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

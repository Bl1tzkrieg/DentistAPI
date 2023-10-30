
// se declara como general para usar el metodo reload de ajax.
let tablaDeConsultas;

$(document).ready(function () {

    crear_procesar_tabla_Consultas(1);

});

// Creando la tabla con la data
function crear_procesar_tabla_Consultas(statusConsulta) {
    tablaDeConsultas = $('#tbconsultas').DataTable({
        ajax: {
            "url": "https://jsonplaceholder.typicode.com/posts",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            /*
            "success": function (data) {
                console.log(data)

                // Example: Filter the data
                data = data.filter(function (item) {
                    return item.id > 14;
                });

                console.log(data)

                // Display the modified data in the DataTable
                tablaDeConsultas.clear().rows.add(data).draw();
            },
            */
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
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='detallesConsulta(" + data + ")'><i class='fas fa-pen'></i><span class='material-symbols-outlined'>preview</span></button>"
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

function detallesConsulta(id) {
    alert("Ver detalles de la consulta: " + id);
}

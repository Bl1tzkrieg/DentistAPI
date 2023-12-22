

// button for details consulta
buttonPreviewConsultaLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='detallesConsulta(";
buttonPreviewConsultaRigth = ")'><i class='fa-regular fa-rectangle-list fa-lg'></i></button>"

// button for details compra
buttonPreviewCompraLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='detallesCompra(";
buttonPreviewCompraRigth = ")'><i class='fa-regular fa-rectangle-list fa-lg'></i></button>"

// button for details venta
buttonPreviewVentaLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='detallesVenta(";
buttonPreviewVentaRigth = ")'><i class='fa-regular fa-rectangle-list fa-lg'></i></button>"

// boton detalles de insumo
botonPreviewInsumosLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='detallesDeInsumo(";
botonPreviewInsumosRigth = ")'><i class='fa-regular fa-rectangle-list fa-lg'></i></button>"

// ***************** -------- *****************

// button for cancel query
buttonCancelQueryLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarConsulta(";
buttonCancelQueryRigth = ")'><i class='fa-solid fa-xmark fa-lg'></i></button>"

// boton cancelar insumo
buttonCancelInsumoLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarInsumo(";
buttonCancelInsumoRigth = ")'><i class='fa-solid fa-xmark fa-lg'></i></button>"

// boton cancelar Compra
buttonCancelCompraLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarCompra(";
buttonCancelCompraRigth = ")'><i class='fa-solid fa-xmark fa-lg'></i></button>"

// boton cancelar Venta
buttonCancelVentaLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='cancelarVenta(";
buttonCancelVentaRigth = ")'><i class='fa-solid fa-xmark fa-lg'></i></button>"

// ***************** -------- *****************

buttonFacturarConsultaLeft = "<button class='btn btn-success btn-sm' type='button' onclick='facturarConsulta(";
buttonFacturarConsultaRigth = ")'><i class='fa-solid fa-square-check fa-lg'></i></button>";

// button for modify user
buttonModifyUserLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='modificarUsuario(";
buttonModifyUserRigth = ")'><i class='fa-solid fa-gear fa-lg'></i></button>"

// button for delete usuario
buttonDeleteLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='eliminarUsuario(";
buttonDeleteRigth = ")'><i class='fa-regular fa-trash-can fa-lg'></i></button>"


// genera la tabla de Consultas
function crearTablaConsultas( elemento, tipo ) {

    // implementar una eleccion de tipo de consulta

    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenConsultas?id=" + tipo,
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                console.log("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                    if (tipo == 1)
                        return buttonPreviewConsultaLeft + data + buttonPreviewConsultaRigth;
                    if (tipo == 2)
                        return buttonPreviewConsultaLeft + data + buttonPreviewConsultaRigth + buttonCancelQueryLeft + data + buttonCancelQueryRigth;
                    if (tipo == 3)
                        return buttonFacturarConsultaLeft + data + buttonFacturarConsultaRigth + buttonCancelVentaLeft + data + buttonCancelVentaRigth;
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
        "dom": tipo == 2 ? 'Brfltip' : 'rfltip',
        "buttons": [
            {
                text: '+Registrar Nuevo Consulta',
                className: "btn btn-success text-white",
                action: function (e, dt, node, config) {
                    console.log("Registrar Nueva Consulta")
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
                next: "<i class='fa-solid fa-arrow-right fa-2xs'></i>",
                previous: "<i class='fa-solid fa-arrow-left fa-2xs'></i>"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });

}

// genera la tabla de Compras
function crearTablaCompras( elemento ) {

    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenCompras",
            // "url": @Url.Action("vista","controlador")
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                console.log("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                        return buttonPreviewCompraLeft + data + buttonPreviewCompraRigth;
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
        "dom": 'rfltip',
        autoHeight: true,
        responsive: true,
        processing: true,
        language: {
            // zeroRecords: "No hay registros disponibles.",
            infoEmpty: "Sin Registros",
            search: "Buscar",
            processing: "<div class='spinner-grow text-primary' role='status'><span class='sr-only'> Loading...</span ></div>",
            paginate: {
                next: "<i class='fa-solid fa-arrow-right fa-2xs'></i>",
                previous: "<i class='fa-solid fa-arrow-left fa-2xs'></i>"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });

}

// genera la tabla de Ventas
function crearTablaVentas( elemento ) {

    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenVentas",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                console.log("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                        return buttonPreviewVentaLeft + data + buttonPreviewVentaRigth
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
        "dom": 'rfltip',
        autoHeight: true,
        responsive: true,
        processing: true,
        language: {
            // zeroRecords: "No hay registros disponibles.",
            infoEmpty: "Sin Registros",
            search: "Buscar",
            processing: "<div class='spinner-grow text-primary' role='status'><span class='sr-only'> Loading...</span ></div>",
            paginate: {
                next: "<i class='fa-solid fa-arrow-right fa-2xs'></i>",
                previous: "<i class='fa-solid fa-arrow-left fa-2xs'></i>"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });
}

// genera la tabla de usuarios
function crearTablaUsuarios( elemento ) {
    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenUsuarios",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                console.log("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                    return buttonModifyUserLeft + data + buttonModifyUserRigth + buttonDeleteLeft + data + buttonDeleteRigth;
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
        "dom": 'Brfltip',
        "buttons": [
            {
                text: '+Registrar Nuevo Usuario',
                className: "btn btn-info text-white",
                action: function (e, dt, node, config) {
                    alert("Registrar Nuevo usuario")
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
                next: "<i class='fa-solid fa-arrow-right fa-2xs'></i>",
                previous: "<i class='fa-solid fa-arrow-left fa-2xs'></i>"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });
}

// genera la tabla de insumos
function crearTablaInsumos( elemento ) {

    // implementar una eleccion de tipo de consulta

    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenInsumos",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "",
            "error": function (xhr, error, thrown) {
                // Este es el manejador de errores o excepciones 
                // For example, you can hide the alert using jQuery
                console.log("Error al intentar intentar requerir las consultas!");
            }
        },
        columns: [
            { "data": "userId" },
            { "data": "title" },
            { "data": "body" },
            {
                "data": "id",
                "render": (data) => {
                    return botonPreviewInsumosLeft + data + botonPreviewInsumosRigth + buttonCancelInsumoLeft + data + buttonCancelInsumoRigth;
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
        "dom": 'Brfltip',
        "buttons": [
            {
                text: '+Registrar Nuevo Insumo',
                className: "btn btn-primary text-white",
                action: function (e, dt, node, config) {
                    console.log("Registrar Nuevo Insumo")
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
                next: "<i class='fa-solid fa-arrow-right fa-2xs'></i>",
                previous: "<i class='fa-solid fa-arrow-left fa-2xs'></i>"
            },
            info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
            lengthMenu: "Mostrando _MENU_ filas",
        }
    });

}


/*   Funciones de consultas   */
function detallesConsulta( consultaID ) {
    console.log( "Detalles de Consulta -> " + consultaID )
}

function cancelarConsulta(idConsulta) {
    console.log("Cancelando Consulta -> " + idConsulta);
}

function facturarConsulta(consultaId) {
    console.log("Facturando Consulta -> " + consultaId)
}

// fin funciones de consultas

/*    Funciones de Compras    */
function detallesCompra(idCompra) {
    console.log("Detalles de Compra -> " + idCompra )
}

function cancelarCompra(idCompra) {
    console.log("Cancelando Compra -> " + idCompra )
}

// fin funciones de compras

/*      Funciones de Ventas     */
function detallesVenta(idVenta) {
    console.log("Detalles de la Venta -> " + idVenta)
}

function cancelarVenta(idVenta) {
    console.log("Cancelando Venta -> " + idVenta)
}
// fin funciones ventas

/*   funciones de insumos    */
function detallesDeInsumo(idInsumo) {
    console.log("Detalles del Insumo -> " + idInsumo)
}

function cancelarInsumo(idInsumo) {
    console.log("Cancelando Insumo -> " + idInsumo)
}
// fin funciones de Insumos

// funciones relacionadas con usuarios
function modificarUsuario(idUsuario) {
    console.log("Modificando Usuario -> " + idUsuario);
}

function eliminarUsuario(idUsuario) {
    console.log("Eliminar Usuario -> " + idUsuario)
}
// fin funciones de Usuarios











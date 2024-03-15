

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
botonModifyInsumosLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='updateDeInsumo(";
botonModifyInsumosRigth = ")'><i class='fa-solid fa-gear fa-lg'></i></button>"

//button detalles proveedor
buttonProveedorLeft = "<button class='btn btn-info btn-sm' type='button' onclick='detallesDeProveedor(";
buttonProveedorRigth = ")'><i class='fa-regular fa-rectangle-list fa-lg'></i></button>";

// ***************** -------- *****************

// boton cancelar consulta
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

// button for modify query
buttonModifyQueryLeft = "<button class='btn btn-primary btn-sm' type='button' onclick='modificarConsulta(";
buttonModifyQueryRigth = ")'><i class='fa-solid fa-gear fa-lg'></i></button>"

// button for delete usuario
buttonDeleteLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='eliminarUsuario(";
buttonDeleteRigth = ")'><i class='fa-regular fa-trash-can fa-lg'></i></button>";

buttonDeleteProveedorLeft = "<button class='btn btn-danger btn-sm' type='button' onclick='eliminarProveedor(";
buttonDeleteProveedorRigth = ")'><i class='fa-regular fa-trash-can fa-lg'></i></button>";

// genera la tabla de Consultas
function crearTablaConsultas( elemento, tipo ) {

    // implementar una eleccion de tipo de consulta
    let tipoConsulta = tipo === 2 || tipo === 3 ? 2 : 1;

    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenConsultas?id=" + tipoConsulta,
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
            { "data": "consulta_id" },
            { "data": "cedula_cliente" },
            { "data": "nombre_cliente" },
            { "data": "apellido_cliente" },
            { "data": "nombre_usuario" },
            { "data": "consulta_estado" },
            {
                "data": "consulta_id",
                "render": (data) => {
                    if (tipo == 1)
                        return buttonPreviewConsultaLeft + data + buttonPreviewConsultaRigth;
                    if (tipo == 2)
                        return buttonModifyQueryLeft + data + buttonModifyQueryRigth + buttonCancelQueryLeft + data + buttonCancelQueryRigth;
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
            { width: "60px", targets: [0, 5] },
            { orderable: false, targets: [3] },
        ],
        "dom": tipo == 2 ? 'Brfltip' : 'rfltip',
        "buttons": [
            {
                text: '+Registrar Nueva Consulta',
                className: "btn btn-success text-white",
                action: function (e, dt, node, config) {
                    $('#modalBuscarCliente').modal('show');
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
            { "data": "compra_id" },
            { "data": "compra_usuario" },
            { "data": "compra_fecha" },
            { "data": "compra_pedido_id" },
            { "data": "compra_numero_factura" },
            { "data": "compra_total" },
            {
                "data": "compra_pedido_id",
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
            { width: "60px", targets: [0, 5] },
            { orderable: false, targets: [6] },
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
            { "data": "venta_id" },
            { "data": "venta_cedula_cliente" },
            { "data": "venta_username" },
            { "data": "venta_fecha" },
            { "data": "venta_total" },
            {
                "data": "venta_id",
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
            { width: "60px", targets: [0, 5] },
            { orderable: false, targets: [5] },
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
            { "data": "Usu_Id" },
            { "data": "Usu_UserName" },
            {
                "data": "Usu_Rol_Id",
                "render": function (data, type, row) {
                    switch (data) {
                        case 1: return 'Admin'
                        case 2: return 'Dentista'
                        case 3: return 'Facturador'
                        case 4: return 'Recepcion'
                    }
                }
            },
            {
                "data": "Usu_Status",
                "render": function (data, type, row) {
                    return data === true ? 'Activo' : 'Inactivo'
                }
            },
            {
                "data": "Usu_Id",
                "render": (data) => {
                    return buttonModifyUserLeft + data + buttonModifyUserRigth;
                },
                "ordenable": false,
                "searchable": false,
                "width": "150px",
            },
        ],
        columnDefs: [
            { className: "text-center", targets: "_all" },
            { width: "60px", targets: [0, 4] },
            { orderable: false, targets: [3,4] },
        ],
        "dom": 'Brfltip',
        "buttons": [
            {
                text: '+Registrar Nuevo Usuario',
                className: "btn btn-info text-white",
                action: function (e, dt, node, config) {
                    $('#modalBuscarEmpleado').modal('show');
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
            { "data": "pedido_id" },
            { "data": "pedido_cantidad" },
            { "data": "pedido_descripcion" },
            { "data": "pedido_username" },
            { "data": "pedido_fecha" },
            {
                "data": "pedido_id",
                "render": (data) => {
                    return botonModifyInsumosLeft + data + botonModifyInsumosRigth + buttonCancelInsumoLeft + data + buttonCancelInsumoRigth;
                },
                "ordenable": false,
                "searchable": false,
                "width": "150px",
            },
        ],
        columnDefs: [
            { className: "text-center", targets: "_all" },
            { width: "60px", targets: [0, 4] },
            { orderable: false, targets: [5] },
        ],
        "dom": 'Brfltip',
        "buttons": [
            {
                text: '+Registrar Nuevo Insumo',
                className: "btn btn-primary text-white",
                action: function (e, dt, node, config) {
                    $('#usuario_id').val(localStorage.getItem('user_id'));
                    $('#modalRegistroInsumo').modal('show');
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

// genera la tabla de proveedores
function crearTablaProveedores( elemento ) {

    return $(elemento).DataTable({
        ajax: {
            "url": "/Peticiones/ResumenProveedores",
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
                    return buttonProveedorLeft + data + buttonProveedorRigth + buttonDeleteProveedorLeft + data + buttonDeleteProveedorRigth;
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
                text: '+Registrar Nuevo Proveedor',
                className: "btn btn-primary text-white",
                action: function (e, dt, node, config) {
                    console.log("Registrar Nuevo Proveedor")
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
async function detallesConsulta(consultaID) {

    const response = await fetch(
        "/Peticiones/ObtenerConsulta?id=" + consultaID,
        { ...requestOptions, method: "GET" }
    );

    if (response.ok) {
        const { cedula_cliente, nombre_cliente,
            apellido_cliente, descripcion_consulta,
            fecha_generacion, fecha_asignacion,
            nombre_usuario, consulta_estado } = await response.json();

        $('#nombre').val(nombre_cliente);
        $('#apellido').val(apellido_cliente);
        $('#cedula').val(cedula_cliente);
        $('#registrado').val(nombre_usuario);
        $('#estado').val(consulta_estado);
        $('#fecasig').val(fecha_asignacion);
        $('#fecemi').val(fecha_generacion);
        $('#floatingTextarea2').val(descripcion_consulta);

        $('#modalRegistro').modal('show');
    }
    else {
        console.log("error -> " + consultaID + " " + response.status)
    }
    

}

function cancelarConsulta(idConsulta) {
    console.log("Cancelando Consulta -> " + idConsulta);
}

function facturarConsulta(consultaId) {
    console.log("Facturando Consulta -> " + consultaId)
}

async function modificarConsulta(consultaId) {

    const response = await fetch(
        "/Peticiones/ObtenerConsulta?id=" + consultaId,
        { ...requestOptions, method: "GET" }
    );

    if (response.ok) {
        const { cedula_cliente, nombre_cliente,
            apellido_cliente, descripcion_consulta,
            fecha_generacion, fecha_asignacion,
            nombre_usuario, consulta_estado, consulta_id } = await response.json();

        $('#consulta_id').val(consulta_id);
        $('#nombre').val(nombre_cliente);
        $('#apellido').val(apellido_cliente);
        $('#cedula').val(cedula_cliente);
        $('#registrado').val(nombre_usuario);
        $('#estado').val(consulta_estado);
        $('#fecasig').val(fecha_asignacion);
        $('#fecemi').val(fecha_generacion);
        $('#textAreaUpdateQuery').val(descripcion_consulta);

        $('#modalUpdate').modal('show');
    }
    else {
        console.log("error -> " + consultaID + " " + response.status)
    }

    // console.log("Modificar Consulta -> " + consultaId)
}

// fin funciones de consultas


/*    Funciones de Compras    */
async function detallesCompra(idCompra) {

    const response = await fetch("/Peticiones/ObtenerCompra?id=" + idCompra,
        { ...requestOptions, method: "GET" });

    if (response.ok) {
        const { compra_usuario, compra_fecha, compra_numero_factura, pedido_cantidad,
        pedido_fecha_registro, pedido_status, pedido_descripcion, compra_total } = await response.json();

        $('#nombre_usuario').val(compra_usuario);
        $('#compra_fecha').val(compra_fecha);
        $('#numero_factura').val(compra_numero_factura);
        $('#fecha_pedido').val(pedido_fecha_registro);
        $('#estado').val(pedido_status);
        $('#cantidad').val(pedido_cantidad);
        $('#compra_total').val(compra_total);
        $('#floatingTextarea2').val(pedido_descripcion);

        $('#modalCompra').modal('show');
    }
    else {
        console.log('Error al intentar consulta la compra -> ' + idCompra);
    }
}

function cancelarCompra(idCompra) {
    console.log("Cancelando Compra -> " + idCompra )
}

// fin funciones de compras

/*      Funciones de Ventas     */
async function detallesVenta(idVenta) {

    const response = await fetch("/Peticiones/ObtenerVenta?id=" + idVenta,
        { ...requestOptions, method: "GET" });

    if (response.ok) {
        const { venta_nombre_cliente, venta_apellido_cliente, venta_cedula_cliente,
            venta_username, venta_numero_factura, venta_fecha, venta_total, venta_tipo_pago } = await response.json();

        $('#nombre_cliente').val(venta_nombre_cliente);
        $('#apellido_cliente').val(venta_apellido_cliente);
        $('#cedula_cliente').val(venta_cedula_cliente);
        $('#nombre_usuario').val(venta_username);
        $('#numero_factura').val(venta_numero_factura);
        $('#fecha').val(venta_fecha);
        $('#venta_total').val(venta_total);
        $('#tipo_pago').val(venta_tipo_pago);

        $('#modalVenta').modal('show');
    }
    else {
        console.log('Error al consulta detalles compra -> ' + reponse.status);
    }

    console.log("Detalles de la Venta -> " + idVenta)
}

function cancelarVenta(idVenta) {
    console.log("Cancelando Venta -> " + idVenta)
}
// fin funciones ventas

/*   funciones de insumos    */
async function updateDeInsumo(idInsumo) {

    const response = await fetch("/Peticiones/ObtenerInsumo?id=" + idInsumo,
        { ...requestOptions, method: "GET" });

    if (response.ok) {
        const { pedido_id, pedido_cantidad, pedido_descripcion,
            pedido_username, pedido_fecha, pedido_status } = await response.json();

        $('#pedido_id_update').val(pedido_id)
        $('#username_update').val(pedido_username);
        $('#fecha').val(pedido_fecha);
        $('#cantidad_update').val(pedido_cantidad);
        $('#estado').val(pedido_status);
        $('#descripcion_pedido_update').val(pedido_descripcion);

        $('#modalUpdateInsumo').modal('show');
    }
    else {
        console.log('Error al intentar consulta la compra -> ' + idCompra);
    }


    console.log("Detalles del Insumo -> " + idInsumo)
}

function cancelarInsumo(idInsumo) {
    console.log("Cancelando Insumo -> " + idInsumo)
}
// fin funciones de Insumos

// funciones relacionadas con usuarios
async function modificarUsuario(idUsuario) {

    // solicitar la informacion al backend llenar el modal con los datos
    const response = await fetch("/Peticiones/ResumenUsuarios?id=" + idUsuario,
        { ...requestOptions, method: "GET" });

    const usuarios = await response.json();
    if (usuarios.length > 0) {
        const { Usu_UserName, Usu_Rol_Id, Usu_Status, Usu_Emp_Id } = usuarios[0];
        $('#user_update').val(Usu_UserName);
        $('#rol_update').val(Usu_Rol_Id);
        $('#status').val(Usu_Status ? "1" : "0");
        $('#idEmpleado_update').val(Usu_Emp_Id);
        $('#modalUpdate').modal('show'); // mostrar el modal luego de ser rellenado
    } else {
        console.log('No se encontró ningún usuario');
    }
}

function eliminarUsuario(idUsuario) {
    console.log("Eliminar Usuario -> " + idUsuario)
}
// fin funciones de Usuarios

// funciones de proveedores
function detallesDeProveedor( idProveedor ) {
    console.log("Detalles de Proveedor : " + idProveedor);
}

function eliminarProveedor( idProveedor ) {
    console.log("Eliminando Proveedor : " + idProveedor)
}
// fin funciones de proveedores











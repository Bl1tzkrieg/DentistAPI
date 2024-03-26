let tabla_de_insumos;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(async function () {

    $('#alerta_error').hide();

    tabla_de_insumos = crearTablaInsumos("#tbinsumos", 2);

    $("#formularioRegistroInsumo").submit(async function (event) {
        event.preventDefault();
        // spinner y cosas aqui

        let pedido_usuario_id = parseInt($('#usuario_id').val());
        let pedido_cantidad = parseInt($('#cantidad').val());
        let pedido_descripcion = $('#descripcion_pedido').val();

        const data = await JSON.stringify({
            pedido_usuario_id,
            pedido_cantidad,
            pedido_descripcion
        });

        const response = await fetch("https://localhost:7091/api/Orders/registrar_pedido",
            { ...requestOptions, method: "POST", body: data });

        if (response.status === 201) {
            alert("Pedido registrado con exito!");
            location.reload();
        }
        else if( response.status === 400 ) {
            alert("Eventualidad inesperada al intentar registrar el pedido.");
            location.reload();
        }
        else {
            alert("Error -> " + response.status);
            location.reload();
        }

    });

    $('#formularioFacturarInsumo').submit(async function (event) {
        event.preventDefault();

        let usuario_id = parseInt(localStorage.getItem('user_id'));
        let pedido_id = $('#pedido_id').val();
        let numero_factura = $('#numero_factura').val();
        let monto_total = parseFloat($('#monto').val());

        const data = await JSON.stringify({
            usuario_id,
            pedido_id,
            numero_factura,
            monto_total
        })

        const response = await fetch("https://localhost:7091/api/Orders/facturar_pedido",
            { ...requestOptions, method: "POST", body: data });

        if (response.status === 201) {
            alert("El pedido ha sido facturado con exito!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar facturar el pedido");
            location.reload();
        }
        else {
            alert("Error " + response.status);
            location.reload();
        }

    });

    $('#insumos_aprobados').text(await obtenerCantidadPedidosPorEstatus(1));

    $('#insumos_cancelados').text(await obtenerCantidadPedidosPorEstatus(2));

}); // fin

async function obtenerCantidadPedidosPorEstatus(tipo) {
    const response = await fetch("https://localhost:7091/api/Orders/obtener_cantidad_pedidos_por_estatus?v1=" + tipo,
        { ...requestOptions, method: "GET" });

    if (response.status === 200) {
        const cantidad = await response.json();
        return cantidad;
    }

    return 0;
}



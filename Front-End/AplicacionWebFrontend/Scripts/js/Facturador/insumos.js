let tabla_de_insumos;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

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

        const response = await fetch("/Peticiones/RegistrarInsumo",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            alert(resp)
        }
        else {
            console.log('Error al registrar el insumo -> ' + response.status)
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

        const response = await fetch("/Peticiones/FacturarInsumo",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            alert(resp);
        }
        else {
            alert(resp.status);
        }

    });


}); // fin
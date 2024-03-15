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

    $('#formularioUpdateInsumo').submit(async function (event) {
        event.preventDefault();

        let pedido_id = $('#pedido_id_update').val();
        let pedido_cantidad = $('#cantidad_update').val();
        let pedido_descripcion = $('#descripcion_pedido_update').val();

        const data = await JSON.stringify({
            pedido_id,
            pedido_cantidad,
            pedido_descripcion
        });

        const response = await fetch("/Peticiones/ActualizarInsumo",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            alert(resp);
        }
        else {
            alert('Ha ocurrido un error ' + response.status);
        }

    });

}); // fin
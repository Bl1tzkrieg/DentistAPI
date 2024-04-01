let tabla_de_insumos;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    $('#alerta_error').hide();

    tabla_de_insumos = crearTablaInsumos("#tbinsumos", 1);

    $("#formularioRegistroInsumo").submit(async function (event) {
        event.preventDefault();
        // spinner y cosas aqui

        let pedido_usuario_id = parseInt($('#usuario_id').val());
        let pedido_cantidad = parseInt($('#cantidad').val());
        let pedido_descripcion = $('#descripcion_pedido').val();

        const data = JSON.stringify({
            pedido_usuario_id,
            pedido_cantidad,
            pedido_descripcion
        });

        const response = await fetch("https://localhost:7091/api/Orders/registrar_pedido",
            { ...requestOptions, method: "POST", body: data });

        if (response.status === 201) {
            alert("Pedido registrado con exito!")
            location.reload();
        }
        else if (response.status === 400) {
            alert("Ha ocurrido una eventualidad con la peticion.");
            location.reload();
        }
        else {
            alert("Error 500");
            location.reload();
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

        const response = await fetch("https://localhost:7091/api/Orders/actualizar_pedido",
            { ...requestOptions, method: "PATCH", body: data });

        if (response.status === 200) {
            alert("Pedido actualizado correctamente!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Ha ocurrido una eventualidad con la peticion.");
            location.reload();
        }
        else {
            alert("Error 500");
            location.reload();
        }

    });

}); // fin
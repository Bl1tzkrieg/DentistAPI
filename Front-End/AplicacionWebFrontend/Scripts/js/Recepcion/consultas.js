
let tabla_de_consultas;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    $('#alerta_error').hide();

    tabla_de_consultas = crearTablaConsultas("#tbconsultas", 2);

    $('#formularioBuscarCliente').submit(function (e) {
        e.preventDefault();
        let id = $('#tipo_documento').val() + $('#identificacion').val();
        clearModal1();
        buscarCliente(id);
    });

    $('#formularioRegistroConsulta').submit(async function (e) {
        e.preventDefault();

        let id_cliente = parseInt($("#cliente_id").val());
        let id_usuario = parseInt(localStorage.getItem('user_id'));
        let descripcion = $("#floatingTextarea2").val();
        let fecha_asignacion = $("#fechaConsulta").val();

        const data = await JSON.stringify({
            id_cliente,
            id_usuario,
            descripcion,
            fecha_asignacion
        });

        const response = await fetch("https://localhost:7091/api/Querys/registrar_consulta",
            { ...requestOptions, method: "POST", body: data }
        );

        if (response.status === 201) {
            alert("Consulta registrada correctamente!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar registrar la consulta!");
            location.reload();
        }
        else {
            // Manejar error HTTP
            alert('Error -> ' + response.status)
            location.reload();
        }

    })

    $('#formularioUpdate').submit(async function (event) {

        // preparar la peticion para el cambio
        event.preventDefault();

        // spinner y demas cosas aqui

        let fecha_consulta = $('#fecmod').val();
        let descripcion = $('#textAreaUpdateQuery').val();
        let id_consulta = $('#consulta_id').val()

        const data = await JSON.stringify({
            id_consulta,
            fecha_consulta,
            descripcion
        });

        const response = await fetch("https://localhost:7091/api/Querys/actualizar_consulta",
            { ...requestOptions, method: "PATCH", body: data });

        if (response.status === 200) {
            alert("Consulta actualizada correctamente!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar actualizar la consulta!");
            location.reload();
        }
        else {
            alert("Error al intentar actualizar el usuario -> " + response.status);
            location.reload();
        }

    })

    $('#formularioRegistroCliente').submit(async function (e) {
        e.preventDefault();

        let identificacion = $('#cliente_documento').val();
        let nombre_1 = $('#cliente_nombre1').val();
        let nombre_2 = $('#cliente_nombre2').val();
        let apellido_1 = $('#cliente_apellido1').val();
        let apellido_2 = $('#cliente_apellido2').val();
        let telefono = $('#codigo_linea').val() + $('#telefono').val();

        const data = JSON.stringify({
            identificacion,
            nombre_1,
            nombre_2,
            apellido_1,
            apellido_2,
            telefono
        });

        const response = await fetch("https://localhost:7091/api/Customers/registrar_cliente",
            { ...requestOptions, method: "POST", body: data });

        if (response.status === 201) {
            alert("Cliente registrado con exito!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar registrar el cliente.");
            location.reload();
        }
        else {
            alert("Error " + response.status);
            location.reload();
        }

    })

}); // fin

async function buscarCliente(id) {

    const res = await fetch(
        "https://localhost:7091/api/Customers/obtener_cliente?v1=" + id,
        { ...requestOptions, method: "GET" }
    );

    $('#modalBuscarCliente').modal('hide');
    clearModal1();

    if (res.status === 200) {
        // Procesar data
        const { cliente_id, cliente_cedula } = await res.json();
        $('#cliente_id').val(cliente_id);
        $('#cedula_cliente').val(cliente_cedula);
        $('#modalRegistroConsulta').modal('show');
    } else if (res.status === 404) {
        $('#cliente_documento').val(id);
        $('#modalRegistroCliente').modal('show');
    }
    else {
        // Manejar error HTTP
        alert('Error ' + res.status)
        location.reload();
    }

}

function clearModal1() {
    $('#identificacion').val("");
}
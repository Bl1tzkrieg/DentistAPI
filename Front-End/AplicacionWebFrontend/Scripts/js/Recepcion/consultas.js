
let tabla_de_consultas;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    tabla_de_consultas = crearTablaConsultas("#tbconsultas", 2);

    $('#buscarCli').click(function () {
        let id = $('#identificacion').val()
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

        const response = await fetch("/Peticiones/RegistrarConsulta",
            { ...requestOptions, method: "POST", body: data }
        );

        if (response.ok) {
            // Procesar data
            const resp = await response.json();
            location.reload();
            alert(resp);
        } else {
            // Manejar error HTTP
            alert('Error -> ' + response.status)
        }

}); // fin

    $('#formularioUpdate').submit(async function (event) {

        // preparar la peticion para el cambio
        event.preventDefault();

        // spinner y demas cosas aqui

        let nuevaFecha = $('#fecmod').val();
        let nuevaDescripcion = $('#textAreaUpdateQuery').val();
        let id = $('#consulta_id').val()

        const data = await JSON.stringify({
            consulta_id: id,
            nueva_fecha: nuevaFecha,
            descripcion: nuevaDescripcion
        });

        const response = await fetch("/Peticiones/ActualizarConsulta",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            console.log(resp);
        }
        else {
            console.log("Error al intentar actualizar el usuario -> " + response.status);
        }


    })

}); // fin

async function buscarCliente(id) {

    const res = await fetch(
        "/Peticiones/ObtenerCliente?cedula=" + id,
        { ...requestOptions, method: "GET" }
    );

    if (res.ok) {
        // Procesar data
        $('#modalBuscarCliente').modal('hide');
        clearModal1();
        const { cliente_id, cliente_cedula } = await res.json();
        $('#cliente_id').val(cliente_id);
        $('#cedula_cliente').val(cliente_cedula);
        $('#modalRegistroConsulta').modal('show');
        console.log(cliente_id)
    } else {
        // Manejar error HTTP
        alert('Error -> ' + res.status)
    }

}

function clearModal1() {
    $('#identificacion').val("");
}
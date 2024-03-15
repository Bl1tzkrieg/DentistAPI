let tabla_de_usuarios;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    tabla_de_usuarios = crearTablaUsuarios("#tbusuarios");

    $('#buscarEmp').click(function () {
        let id = $('#identificacion').val()
        buscarEmpleado(id);
    });

    $("#formularioRegistro").submit(async function (event) {
        event.preventDefault();
        // spinner y cosas aqui

        let username = $('#username').val();
        let password = $('#password').val();
        let rol = parseInt($('#rol_registro').val());
        let empleado_id = parseInt($('#idEmpleado').val());

        const data = await JSON.stringify({
            empleado_id,
            username,
            password,
            rol
        });

        const response = await fetch("/Peticiones/RegistrarUsuario",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            console.log(resp.status);
        }
        else {
            console.log('Error al registrar el usuario -> ' + response.status)
        }

    });

    $('#formularioUpdate').submit(async function (event) {
        event.preventDefault();

        // spinner y demas cosas aqui
        let username = $('#user_update').val();
        let password = $('#pass_update').val();
        let rol = parseInt($('#rol_update').val());
        let status = $('#status').val();

        const data = await JSON.stringify({
            username,
            password,
            rol,
            username_status : parseInt(status)
        });

        const response = await fetch("/Peticiones/ActualizarUsuario",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            console.log(resp.status);
        }
        else {
            console.log("Error al intentar actualizar el usuario -> " + response.status);
        }
    });

}); // fin

async function buscarEmpleado(id) {

    const res = await fetch(
        "/Peticiones/BuscarEmpleado?v1=" + id,
        { ...requestOptions, method: "GET" }
    );

    if (res.ok) {
        // Procesar data
        $('#modalBuscarEmpleado').modal('hide');
        clearModal1();
        const { Emp_Id } = await res.json();
        $('#idEmpleado').val(Emp_Id); 
        $('#modalRegistro').modal('show');
    } else {
        // Manejar error HTTP
        alert('Error -> ' + res.status)
    }

}

function clearModal1() {
    $('#identificacion').val("");
}
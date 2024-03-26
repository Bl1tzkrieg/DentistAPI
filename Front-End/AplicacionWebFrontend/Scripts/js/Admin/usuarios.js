let tabla_de_usuarios;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    $('#alerta_error').hide();

    tabla_de_usuarios = crearTablaUsuarios("#tbusuarios");

    $('#formularioBuscarEmpleado').submit(function (e) {
        e.preventDefault();
        let documento =  ( $('#tipo_documento').val() + $('#identificacion').val() )
        buscarEmpleado( documento );
    });

    $("#formularioRegistro").submit(async function (event) {
        event.preventDefault();
        // spinner y cosas aqui

        let username = $('#username').val();
        let password = $('#password').val();
        let rol = parseInt($('#rol_registro').val());
        let empleado_id = parseInt($('#idEmpleado').val());
        let documento = $('#cedula_empleado').val();

        const data = await JSON.stringify({
            empleado_id,
            username,
            password,
            rol,
            documento
        });

        const response = await fetch("https://localhost:7091/api/Users/registrar_usuario",
            { ...requestOptions, method: "POST", body: data });

        if (response.status === 201) {
            alert("Usuario registrado correctamente!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar registrar el usuario.");
            location.reload();
        }
        else {
            alert('Error -> ' + response.status);
            location.reload();
        }

    });

    $('#formularioUpdate').submit(async function (event) {
        event.preventDefault();

        // spinner y demas cosas aqui
        

        let password = "";

        if ($('#pass_update').val() !== "") {

            if ($('#pass_update').val() === $('#repeatPass_update').val()) {
                password = $('#pass_update').val();
            }
            else {
                alert("Las contraseñas no coinciden vuelva a intentarlo.");
                location.reload();
            }

        }

        let username = $('#user_update').val();
        let rol = parseInt($('#rol_update').val());
        let username_status = $('#status').val() === "1" ? true : false;

        const data = await JSON.stringify({
            username,
            password,
            rol,
            username_status
        });

        const response = await fetch("https://localhost:7091/api/Users/actualizar_usuario",
            { ...requestOptions, method: "PATCH", body: data });

        if (response.status === 200) {
            alert("Usuario actualizado correctamente!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar actualizar el usuario.");
            location.reload();
        }
        else {
            alert('Error -> ' + response.status);
            location.reload();
        }
    });

    $('#modalRegistroEmpleado').submit(async function (e) {
        e.preventDefault();

        let identificacion = $('#cedula_identidad').val();
        let nombre_1 = $('#primer_nombre').val();
        let nombre_2 = $('#segundo_nombre').val();
        let apellido_1 = $('#primer_apellido').val();
        let apellido_2 = $('#segundo_apellido').val();
        let correo = $('#correo').val();

        const data = await JSON.stringify({
            identificacion,
            nombre_1,
            nombre_2,
            apellido_1,
            apellido_2,
            correo
        })

        const response = await fetch("https://localhost:7091/api/Employees/registrar_empleado",
            { ...requestOptions, method: "POST", body: data });

        clearModalEmpleado();

        if (response.status === 201) {
            alert("Empleado registrado con exito!");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar registrar el empleado!")
        }
        else {
            alert('Error ' + response.status);
            location.reload();
        }
    });

}); // fin

async function buscarEmpleado( documento ) {

    const res = await fetch(
        "https://localhost:7091/api/Employees/obtener_empleado?v1=" + documento,
        { ...requestOptions, method: "GET" }
    );

    if (res.status === 200 || res.status === 404) {

        const { status, Emp_Id } = await res.json();

        if (status) {
            // Procesar data
            $('#modalBuscarEmpleado').modal('hide');
            clearModal1();
            $('#idEmpleado').val(Emp_Id);
            $('#cedula_empleado').val(documento);
            $('#modalRegistro').modal('show');
        }
        else {
            // mostrar modal de registro de empleado
            $('#modalBuscarEmpleado').modal('hide');
            $('#cedula_identidad').val(documento);
            clearModal1();
            $('#modalRegistroEmpleado').modal('show');
        }
        
    } else {
        // Manejar error HTTP
        alert('Error -> ' + res.status)
    }

}

function clearModal1() {
    $('#identificacion').val("");
};

function clearModalEmpleado() {
    $('#cedula_identidad').val("");
    $('#primer_nombre').val("");
    $('#segundo_nombre').val("");
    $('#primer_apellido').val("");
    $('#segundo_apellido').val("");
    $('#correo').val("");
}
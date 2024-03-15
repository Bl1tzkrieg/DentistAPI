
// configuracion de peticion por defecto
const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json",
    },
};


$(document).ready(function () {

    $(".alert").hide();

    $("#login").submit(async function (event) {

        event.preventDefault();

        $("#btnSubmit").prop("disabled", true);
        $("#btnSubmit").html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span><span class='sr-only'></span>Loading..");

        let user = $("#user").val();
        let pass = $("#password").val();

        const data = await JSON.stringify({
            USUARIO: user,
            PASSWORD: pass
        });

        // peticion login
        const response = await fetch("/Acceso/Login", { ...requestOptions, method: "POST", body: data });

        const credenciales = await response.json();

        if (response.ok) {
            $("#btnSubmit").html("Log In");
            localStorage.setItem('user_id', credenciales.user_id);
            // $("#btnSubmit").prop("disabled", false);
            window.location.href = "/" + SelectRol(credenciales.rol) + "/Index";
        } else {
            $("#btnSubmit").html("Log In");
            $("#btnSubmit").prop("disabled", false);
            $(".alert").show();
            setTimeout(function () { $(".alert").hide(); }, 3000);
        }

    })

}); // fin de document.ready

function SelectRol(rol) {
    switch (rol) {
        case 1: return "Admin";
        case 2: return "Dentista";
        case 3: return "Facturador";
        case 4: return "Recepcion"
    }
}
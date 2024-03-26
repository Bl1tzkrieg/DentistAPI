
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

        if (response.status === 200) {
            $("#btnSubmit").html("Log In");
            const { statuscode, message, data } = await response.json();

            if (statuscode === 401 || statuscode === 500) {
                $('#alertalogin').text(message);
                $(".alert").show();
                setTimeout(function () {
                    $(".alert").hide();
                    $("#btnSubmit").html("Ingresar");
                    $("#btnSubmit").prop("disabled", false);
                }, 2500);
            }
            else {
                localStorage.setItem('user_id', data.user_id);
                window.location.href = "/" + SelectRol(data.rol) + "/Index";
            }
         
        }
        else {
            $('#alertalogin').text("Internal Server error 500");
            $(".alert").show();
            setTimeout(function () {
                $(".alert").hide();
                $("#btnSubmit").html("Ingresar");
                $("#btnSubmit").prop("disabled", false);
            }, 2500);
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
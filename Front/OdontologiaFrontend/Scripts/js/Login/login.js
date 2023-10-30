
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

    $("#login").submit( async function (event) {

        event.preventDefault();

        $("#btnSubmit").prop("disabled", true);
        $("#btnSubmit").html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span><span class='sr-only'></span>Loading..");

        let user = $("#user").val();
        let pass = $("#password").val();

        console.log("haciendo peticion " + user + " " + pass);

        const data = await JSON.stringify({
            CORREO: user,
            PASSWORD : pass
        });

        // peticion login
        const datos = await fetch("/Acceso/Login", { ...requestOptions, method: "POST", body: data });

        const response = await datos.json();

        localStorage.setItem("nombre","jesus");

        console.log( response )

        if (response.registrado) {
            $("#btnSubmit").html("Log In");
            // $("#btnSubmit").prop("disabled", false);
            window.location.href = "/" + response.rol + "/Index";
        } else {
            $("#btnSubmit").html("Log In");
            $("#btnSubmit").prop("disabled", false);
            $(".alert").show();
            setTimeout(function () { $(".alert").hide(); }, 3000);
            console.log("user or password invalid.")
            console.log(response.registrado);
        }
            
    })

}); // fin de document.ready




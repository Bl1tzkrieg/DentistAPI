let tabla_de_ventas;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    tabla_de_ventas = crearTablaVentas("#tbventas");

}); // fin
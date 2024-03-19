let tabla_de_compras;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    tabla_de_compras = crearTablaCompras("#tbcompras");

}); // fin
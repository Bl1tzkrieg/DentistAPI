
let tabla_de_consultas;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

    tabla_de_consultas = crearTablaConsultas("#tbconsultas", 1);

}); // fin
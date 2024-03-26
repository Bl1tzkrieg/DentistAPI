
let tabla_de_consultas;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(async function () {

    $('#alerta_error').hide();

    tabla_de_consultas = crearTablaConsultas("#tbconsultas", 1);

    $('#consultas_procesadas').text(await obtenerTotales(1))

    $('#consultas_pendientes').text(await obtenerTotales(2))

    $('#consultas_canceladas').text(await obtenerTotales(3))

}); // fin

async function obtenerTotales(tipo) {

    const response = await fetch("https://localhost:7091/api/Querys/obtener_total_consultas_por_tipo?v1=" + tipo,
        { ...requestOptions, method: "GET" });

    if (response.status === 200) {
        const total = await response.json()
        return total;
    }
    else {
        return 0;
    }
}
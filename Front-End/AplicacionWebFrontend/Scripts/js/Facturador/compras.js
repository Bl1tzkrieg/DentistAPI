let tabla_de_compras;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(async function () {

    $('#alerta_error').hide();

    tabla_de_compras = crearTablaCompras("#tbcompras");

    $('#compras_realizadas').text(await obtenerCantidadDeCompras());

    $('#gastos_totales').text(await obtenerGastos());

}); // fin

async function obtenerCantidadDeCompras() {
    const response = await fetch("https://localhost:7091/api/Shopping/obtener_cantidad_compras",
        { ...requestOptions, method: "GET" })

    if (response.status === 200) {
        const cantidadDeCompras = await response.json();
        return cantidadDeCompras;
    }

    return 0;
}

async function obtenerGastos() {
    const response = await fetch("https://localhost:7091/api/Shopping/obtener_total_gastos",
        { ...requestOptions, method: "GET" })

    if (response.status === 200) {
        const totalGastos = await response.json();
        return totalGastos;
    }

    return 0;
}
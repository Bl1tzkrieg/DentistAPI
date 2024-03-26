let tabla_de_ventas;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(async function () {

    $('#alerta_error').hide();

    tabla_de_ventas = crearTablaVentas("#tbventas");

    $('#ventas_realizadas').text(await obtenerCantidadDeVentas());

    $('#ingresos_totales').text(await obtenerIngresos());

}); // fin


async function obtenerCantidadDeVentas() {
    const response = await fetch("https://localhost:7091/api/Sales/obtener_cantidad_ventas",
        { ...requestOptions, method: "GET" })

    if (response.status === 200) {
        const cantidadDeVentas = await response.json();
        return cantidadDeVentas;
    }

    return 0;
}

async function obtenerIngresos() {
    const response = await fetch("https://localhost:7091/api/Sales/obtener_total_ingresos",
        { ...requestOptions, method: "GET" })

    if (response.status === 200) {
        const totalIngresos = await response.json();
        return totalIngresos;
    }

    return 0;
}
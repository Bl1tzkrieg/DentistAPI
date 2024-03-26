
let tablaDeConsultasPorFacturar;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(async function () {

    $('#alerta_error').hide();

    tablaDeConsultasPorFacturar = crearTablaConsultas("#tbconsultas", 3);

    $('#formularioFacturarConsulta').submit(async function (e) {
        e.preventDefault();

        let consulta_id = parseInt($('#consulta_id').val());
        let cliente_id = parseInt($('#cliente_id').val());
        let numero_factura = $('#numero_factura').val();;
        let usuario_id = parseInt(localStorage.getItem('user_id'));
        let total_pagar = parseFloat($('#monto').val());
        let tipo_pago = parseInt($('#tipo_pago').val());

        const data = await JSON.stringify({
            consulta_id,
            cliente_id,
            numero_factura,
            usuario_id,
            total_pagar,
            tipo_pago,
        });

        const response = await fetch("https://localhost:7091/api/Querys/facturar_consulta",
            { ...requestOptions, method: "POST", body: data });

        if (response.status === 201) {
            alert("La consulta ha sido facturada correctamente.");
            location.reload();
        }
        else if (response.status === 400) {
            alert("Eventualidad inesperada al intentar facturar la consulta.");
            location.reload();
        }
        else {
            alert("Error " + response.status);
            location.reload();
        }

    })

    $('#consultas_por_facturar').text(await obtenerConsultasFacturadas(1));

    $('#consultas_facturadas').text(await obtenerConsultasFacturadas(2));

})

async function obtenerConsultasFacturadas(tipo) {
    const response = await fetch("https://localhost:7091/api/Querys/obtener_consultas_por_facturar?v1=" + tipo,
        { ...requestOptions, method: "GET" });

    if (response.status === 200) {
        const cantidadDeConsultasPorFacturar = await response.json();
        return cantidadDeConsultasPorFacturar;
    }

    return 0;
}
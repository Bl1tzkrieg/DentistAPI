
let tablaDeConsultasPorFacturar;

const requestOptions = {
    method: "",
    mode: "cors",
    headers: {
        "Content-Type": "application/json"
    },
};

$(document).ready(function () {

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

        const response = await fetch("/Peticiones/FacturarConsulta",
            { ...requestOptions, method: "POST", body: data });

        if (response.ok) {
            const resp = await response.json();
            location.reload();
            alert(resp);
        }
        else {
            alert(response.status);
        }

    })

})
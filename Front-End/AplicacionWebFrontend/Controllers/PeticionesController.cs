using Antlr.Runtime.Tree;
using AplicacionWebFrontend.DTO;
using AplicacionWebFrontend.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWebFrontend.Controllers
{
    public class PeticionesController : Controller
    {
        // CONSULTAS

        // GET: ResumenConsultas
        public async Task<ActionResult> ResumenConsultas(int id)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("/api/Querys/obtener_consultas?v1=" + id);

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<List<Consultas>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> ObtenerConsulta(int id)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("/api/Querys/obtener_consulta?v1="+ id);

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<Consultas>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> ActualizarConsulta(ActualizarConsulta_DTO consulta)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Querys/actualizar_consulta", Method.Patch);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    id_consulta = consulta.consulta_id,
                    fecha_consulta = consulta.nueva_fecha,
                    consulta.descripcion
                });

                var respuesta = await api_Request.PatchAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> RegistrarConsulta(RegistrarConsulta_DTO query)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Querys/registrar_consulta", Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    query.id_cliente,
                    query.id_usuario,
                    query.descripcion,
                    query.fecha_asignacion
                });

                var respuesta = await api_Request.PostAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> FacturarConsulta(FacturarConsulta_DTO query)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Querys/facturar_consulta", Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    query.cliente_id,
                    query.numero_factura,
                    query.usuario_id,
                    query.tipo_pago,
                    query.consulta_id,
                    query.total_pagar,
                });

                var respuesta = await api_Request.PostAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Datos inválidos o incompletos.");
                }


                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // COMPRAS

        // GET: ResumenCompras
        public async Task<ActionResult> ResumenCompras()
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Shopping/obtener_compras");

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<List<Compras>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> ObtenerCompra(int id)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Shopping/obtener_compra?v1="+ id);

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<Compras>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // VENTAS

        // GET: ResumenVentas
        public async Task<ActionResult> ResumenVentas()
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Sales/obtener_ventas");

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<List<Ventas>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> ObtenerVenta( int id)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Sales/obtener_venta?v1=" + id);

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<Ventas>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // INSUMOS

        // GET: ResumenInsumos
        public async Task<ActionResult> ResumenInsumos()
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Orders/obtener_pedidos");

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<List<Pedidos>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarInsumo(RegistrarInsumo_DTO insumo )
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Orders/registrar_pedido", Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    insumo.pedido_descripcion,
                    insumo.pedido_cantidad,
                    insumo.pedido_usuario_id,
                });

                var respuesta = await api_Request.PostAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> ObtenerInsumo(int id)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Orders/obtener_pedido?v1=" + id);

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<Pedidos>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> ActualizarInsumo(ActualizarInsumo_DTO pedido)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Orders/actualizar_pedido", Method.Patch);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    pedido.pedido_id,
                    pedido.pedido_cantidad,
                    pedido.pedido_descripcion
                });

                var respuesta = await api_Request.PatchAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> FacturarInsumo(FacturarInsumo_DTO pedido)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Orders/facturar_pedido", Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    pedido.pedido_id,
                    pedido.usuario_id,
                    pedido.numero_factura,
                    pedido.monto_total
                });

                var respuesta = await api_Request.PostAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Datos inválidos o incompletos.");
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // PROVEEDORES

        // GET: ResumenProveedores
        public async Task<ActionResult> ResumenProveedores()
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://jsonplaceholder.typicode.com/posts");
                var request = new RestRequest("");

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<List<Usuarios>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // USUARIOS

        // GET: ResumenUsuarios
        public async Task<ActionResult> ResumenUsuarios(int id = -1)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091");
                var request = new RestRequest($"/api/Users/obtener_usuario" + (id <= 0 ? "" : "?v1=" + id));

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<List<Usuarios>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarUsuario(RegistroUsuario_DTO user)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Users/registrar_usuario", Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new { 
                    username = user.username.ToUpper(),
                    user.password,
                    user.rol,
                    user.empleado_id,
                });

                var respuesta = await api_Request.PostAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> ActualizarUsuario(ActualizarUsuario_DTO user)
        {
            try
            {
                var api_Request = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Users/actualizar_usuario", Method.Patch);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    username = user.username.ToUpper(),
                    user.password,
                    user.rol,
                    user.username_status
                });

                var respuesta = await api_Request.PatchAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> BuscarEmpleado( string v1 )
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/api/Employees/obtener_empleado?v1=" + v1);
                var request = new RestRequest("");

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<Empleado>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // CLIENTES

        public async Task<ActionResult> ObtenerCliente(string cedula)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/");
                var request = new RestRequest("api/Customers/obtener_cliente?v1=" + cedula);

                var respuesta = await clientJsonPlaceHolder.ExecuteGetAsync(request);

                if (respuesta.StatusCode == 0)
                {
                    throw new Exception(respuesta.ErrorMessage);
                }

                if (respuesta.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(respuesta.StatusDescription);
                }

                // Convert the response content to a JSON string
                var json = respuesta.Content;

                // Deserialize the JSON string to a list of objects
                var data = JsonConvert.DeserializeObject<Cliente>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
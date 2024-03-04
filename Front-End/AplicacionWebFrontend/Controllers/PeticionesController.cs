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
        // GET: ResumenConsultas
        public async Task<ActionResult> ResumenConsultas(int id)
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/api/Querys/obtener_consultas");
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
                var data = JsonConvert.DeserializeObject<List<Consultas>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: ResumenCompras
        public async Task<ActionResult> ResumenCompras()
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

        // GET: ResumenVentas
        public async Task<ActionResult> ResumenVentas()
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

        // GET: ResumenUsuarios
        public async Task<ActionResult> ResumenUsuarios()
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091");
                var request = new RestRequest("/api/Users");

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

        // GET: ResumenInsumos
        public async Task<ActionResult> ResumenInsumos()
        {
            try
            {
                var clientJsonPlaceHolder = new RestClient("https://localhost:7091/api/Orders/obtener_pedidos");
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
                var data = JsonConvert.DeserializeObject<List<Pedidos>>(json);

                // Return the data as a JsonResult
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

    }
}
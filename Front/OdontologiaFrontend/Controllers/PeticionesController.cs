using Newtonsoft.Json;
using OdontologiaFrontend.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace OdontologiaFrontend.Controllers
{
    public class PeticionesController : Controller
    {
        // GET: Peticiones
        public async Task<ActionResult> Compras(int id)
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
                var data = JsonConvert.DeserializeObject<List<Post>>(json);

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
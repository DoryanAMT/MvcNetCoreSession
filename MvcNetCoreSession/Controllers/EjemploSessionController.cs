using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSession.Extensions;
using MvcNetCoreSession.Helpers;
using MvcNetCoreSession.Models;
using System.Security.Cryptography.X509Certificates;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SessionMascotaCollection
            (string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>()
                    {
                        new Mascota {Nombre ="Patricio", Raza="Estrella", Edad=4},
                        new Mascota {Nombre ="Apu", Raza="Monito", Edad=10},
                        new Mascota {Nombre ="Donald", Raza="Pato", Edad=50},
                        new Mascota {Nombre ="Pluto", Raza="Perroo", Edad=60},
                    };
                    HttpContext.Session.SetObject("MASCOTAS", mascotas);
                    ViewData["MENSAJE"] = "Coleccion Mascotas almacenada";
                }else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotas = HttpContext.Session.GetObject<List<Mascota>>("MASCOTAS");
                    return View(mascotas);
                }
            }
            return View();
        }
        public IActionResult SessionMascotaObject
            (string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Olaf", Raza ="Muñeco", Edad = 19
                    };
                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"] = "Mascota como Object almacenada";
                }else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTA"] = mascota; 
                }
            }
            return View();
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar" )
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Simba", Raza = "Leon", Edad = 9
                    };
                    //  UTILIZAMOS EL HELPER PARA CONVERTIR EL OBJETO A string
                    string jsonMascota = HelperJsonSession.SerializerObject<Mascota>(mascota);
                    //  ALMACENAMOS EN SESSION EL STRING
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "Mascota JSON almacenada";
                }else if (accion.ToLower() == "mostrar")
                {
                    //  RECUPERAMOS EL STRING JSON DE MASCOTA
                    string json = HttpContext.Session.GetString("MASCOTA");
                    //  CONVERTIMOS STRING A MASCOTA
                    Mascota mascota = HelperJsonSession.DeserealizeObject<Mascota>(json);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionCollector
            (string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota {Nombre= "Nala", Raza="Leona", Edad=10},
                        new Mascota {Nombre= "Olaf", Raza="Nieve", Edad=14},
                        new Mascota {Nombre= "Rafiki", Raza="Mono", Edad=20},
                        new Mascota {Nombre= "Sebastian", Raza="Cangrejo", Edad=12},
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Colección almacenada en Session";
                    return View();
                }else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }
        public IActionResult SessionMascotas
            (string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Flounder";
                    mascota.Raza= "Pez";
                    mascota.Edad = 5;
                    //  PARA ALMACENAR OBJETOS Mascota DEBEMOS 
                    //  CONVERTIRLOS A Byte[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    //  ALMACENAMOS EL OBJETO EN SESSION MEDIANTE Set
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";

                }else if (accion.ToLower() == "mostrar")
                {
                    //  RECUPERAMOS LOS BYTES DE MASCOTA DE SESSION
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    //  CONVERTIMOS LOS BYTES A OBJETO MASCOTA
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionSimple
            (string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //  GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongDateString());
                    ViewData["MENSAJE"] = "Datos almacenados en session";

                }else if (accion.ToLower() == "mostrar")
                {
                    //  RECUPERAMOS LOS DATOS DE SESSION
                    ViewData["USUARIO"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
    }
}

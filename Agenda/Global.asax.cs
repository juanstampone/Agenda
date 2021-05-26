using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Agenda
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación        
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            List<Entidad.Contacto> listaContacto = new List<Entidad.Contacto>();
            listaContacto.Add(new Entidad.Contacto { id = 1, nombreApellido = "Juan Stampone" });
            listaContacto.Add(new Entidad.Contacto{id = 2, nombreApellido = "Diego Perez"});
            listaContacto.Add(new Entidad.Contacto { id = 3, nombreApellido = "Roberto Garcia" });
            Application["listaContacto"] = listaContacto;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;


namespace proyecto_poderosa_documento.Areas.Admin.Controllers
{
    public class AnoController : Controller
    {

        private anos anos_ = new anos();
        private  documento documento_= new documento();

        // GET: Admin/Ano
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Eliminar(int id)
        {
            int mensaje = 0;
            anos_ = anos_.verificar_relaciones(id);
            if (anos_.documento.Count > 0)
            {
                mensaje = 1;
            }
            else
            {
                anos_.id = id;
                anos_.Eliminar();
                mensaje = 2;
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            if (ModelState.IsValid)
            {
                anos_ = anos_.editar(id);
            }
            return View(anos_);
        }

        [HttpPost]
        public ActionResult Save(anos model)
        {        
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                if (anos_.validarano(model.ano) == 0)
                {
                    rm = model.Guardar();
                    return RedirectToAction("index", "ano");
                }else
                {
                    ViewBag.mensaje = "Existe este año por favor, ingrese otro. Gracias";
                }            
            }
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult GetTodoLists_ano(JqGrid jqgrid, string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            using (var ctx = new ProyectoContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                var data = (from p in ctx.anos
                            select new
                            {
                                id = p.id,
                                ano = p.ano
                            }).OrderByDescending(a => a.ano).ToList();

                int totalRecords = data.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = data
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
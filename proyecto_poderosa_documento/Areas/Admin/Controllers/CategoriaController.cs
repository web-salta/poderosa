using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace proyecto_poderosa_documento.Areas.Admin.Controllers
{
    public class CategoriaController : Controller
    {

        private categoria categoria_ = new categoria();
        // GET: Admin/Categoria
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Eliminar(int id)
        {
            int mensaje = 0;
            categoria_ = categoria_.verificar_relaciones(id);
            if (categoria_.documento.Count > 0)
            {
                mensaje = 1;
            }
            else
            {
                categoria_.id = id;
                categoria_.Eliminar();
                mensaje = 2;
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Save(int id)
        {
            if (ModelState.IsValid)
            {
                categoria_ = categoria_.editar(id);
            }
            return View(categoria_);
        }

        [HttpPost]
        public ActionResult Save(categoria model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                /*si es que la categoria non existe*/
                if (categoria_.validarcategoria(model.nombre) == 0)
                {
                    rm = model.Guardar();
                    return RedirectToAction("index", "categoria");
                }
                else
                {
                    ViewBag.mensaje = "Ya existe esta categoría por favor, ingrese otra. Gracias";
                }
            }
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult GetTodoLists_categoria(JqGrid jqgrid, string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            using (var ctx = new ProyectoContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                var data = (from p in ctx.categoria
                            select new
                            {
                                id = p.id,
                                nombre = p.nombre
                            }).OrderByDescending(a => a.id).ToList();

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
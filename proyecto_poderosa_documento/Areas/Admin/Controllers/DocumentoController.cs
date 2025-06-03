using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using proyecto_poderosa_documento.Areas.Admin.Filters;

namespace proyecto_poderosa_documento.Areas.Admin.Controllers
{
    [Autenticado]
    public class DocumentoController : Controller
    {
      
        // GET: Admin/Documento
        private anos ano_ = new anos();
        private categoria categoria_ = new categoria();
        private documento documento_ = new documento();

       private ViewDocumento Vdocumento_ = new ViewDocumento();


        ProyectoContext db = new ProyectoContext();
        public ActionResult Index()
        {
            ViewBag.lista_ano = ano_.Listarano();
            ViewBag.lista_categoria = categoria_.Listarcategoria();
            return View();
        }


        [HttpGet]
        public ActionResult Save(int id)
        {          
            if (ModelState.IsValid)
            {
                Vdocumento_ = Vdocumento_.editar(id);
            }
            return View(Vdocumento_);
        }

        [HttpPost]
        public ActionResult Save(documento model, HttpPostedFileBase nombre_archivo)
        {
            bool status = false;
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar(nombre_archivo);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult GetTodoLists_documento(JqGrid jqgrid, string sidx, string sord, int page, int rows, int ano)  //Gets the todo Lists.
        {
            using (var ctx = new ProyectoContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                var data = (from p in db.documento
                              join t in db.anos on p.anoId equals t.id
                              join c in db.categoria on p.categoriaId equals c.id
                              where(p.anoId == ano)
                              select new
                              {
                                  id = p.id,
                                  ano = p.anoId,
                                  valor_ano = t.ano.ToString(),
                                  trimestre = p.trimestre,
                                  categoriaId = p.categoriaId,
                                  nombre = c.nombre,
                                  titulo_archivo = p.titulo_archivo,
                                  nombre_archivo = p.nombre_archivo,
                                  descripcion_archivo = p.descripcion_archivo,
                                  fecha = p.fecha

                              }).ToList();


                int totalRecords = data.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

                /*---*/

               /* if (sord.ToUpper() == "ASC")
                {
                    todoListsResults = todoListsResults.OrderByDescending(s => s.id);
                    todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
                }
                else
                {
                    todoListsResults = todoListsResults.OrderBy(s => s.id);
                    todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
                }*/
                /*---*/

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

        [AcceptVerbs("Get", "Post")]
        public JsonResult buscarporano_semestre(int ano, int trimestre)
        {
            using (var db = new ProyectoContext())
            {
                var page = 1;
               // List<> rows ;
                /*es mejor enviarlo campor campo sino sale un error de json result[0]*/
             //  
                int totalRecords;
                var todoListsResults = (from p in db.documento
                                        join t in db.anos on p.anoId equals t.id
                                        join c in db.categoria on p.categoriaId equals c.id
                                        where (p.anoId == ano && p.trimestre == trimestre)
                                        orderby p.id descending
                                        select new
                                        {
                                            id = p.id,
                                            ano = p.anoId,
                                            valor_ano = t.ano,
                                            trimestre = p.trimestre,
                                            categoriaId = p.categoriaId,
                                            nombre = c.nombre,
                                            titulo_archivo = p.titulo_archivo,
                                            nombre_archivo = p.nombre_archivo,
                                            descripcion_archivo = p.descripcion_archivo,
                                            fecha = p.fecha
                                        }).ToList();

                totalRecords = todoListsResults.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)page);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows= todoListsResults,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs("Get", "Post")]
        public JsonResult buscarporano_semestre_cat(int ano, int trimestre)
        {
            using (var db = new ProyectoContext())
            {
                var page = 1;
                // List<> rows ;
                /*es mejor enviarlo campor campo sino sale un error de json result[0]*/
                //  
                int totalRecords;
                var todoListsResults = (from p in db.documento
                            join t in db.anos on p.anoId equals t.id
                            join c in db.categoria on p.categoriaId equals c.id
                            where (p.anoId == ano && p.trimestre == trimestre)
                            select new
                            {
                                id = p.id,
                                ano = p.anoId,
                                valor_ano = t.ano,
                                trimestre = p.trimestre,
                                categoriaId = p.categoriaId,
                                nombre = c.nombre,
                                titulo_archivo = p.titulo_archivo,
                                nombre_archivo = p.nombre_archivo,
                                descripcion_archivo = p.descripcion_archivo,
                                fecha = p.fecha
                            }).ToList();
                totalRecords = todoListsResults.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)page);

                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = todoListsResults,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult buscarporano(int? ano)
        {
            using (var db = new ProyectoContext())
            {
                var page = 1;
                int totalRecords;
                var todoListsResults = (from p in db.documento
                                        join t in db.anos on p.anoId equals t.id
                                        join c in db.categoria on p.categoriaId equals c.id
                                        where (p.anoId == ano)
                                        orderby p.id descending
                                        select new
                                        {
                                            id = p.id,
                                            ano = p.anoId,
                                            valor_ano = t.ano,
                                            trimestre = p.trimestre,
                                            categoriaId = p.categoriaId,
                                            nombre = c.nombre,
                                            titulo_archivo = p.titulo_archivo,
                                            nombre_archivo = p.nombre_archivo,
                                            descripcion_archivo = p.descripcion_archivo,
                                            fecha = p.fecha
                                        }).ToList();

                totalRecords = todoListsResults.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)page);

                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = todoListsResults,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult buscarporano_cat(int ano, int categoria)
        {
            using (var db = new ProyectoContext())
            {
                var page = 1;
                // List<> rows ;
                /*es mejor enviarlo campor campo sino sale un error de json result[0]*/
                //  
                int totalRecords;
                var todoListsResults = (from p in db.documento
                                        join t in db.anos on p.anoId equals t.id
                                        join c in db.categoria on p.categoriaId equals c.id
                                        where (p.anoId == ano && p.categoriaId == categoria)
                                        orderby p.id descending
                                        select new
                                        {
                                            id = p.id,
                                            ano = p.anoId,
                                            valor_ano = t.ano,
                                            trimestre = p.trimestre,
                                            categoriaId = p.categoriaId,
                                            nombre = c.nombre,
                                            titulo_archivo = p.titulo_archivo,
                                            nombre_archivo = p.nombre_archivo,
                                            descripcion_archivo = p.descripcion_archivo,
                                            fecha = p.fecha
                                        }).ToList();       
                       
                totalRecords = todoListsResults.Count();


                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)page);

                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = todoListsResults,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult Eliminar(int id)
        {
            documento_.id = id;
            /*---*/
            var registro_ = documento_.Obtener(id);
            documento_.nombre_archivo = registro_.nombre_archivo;
            /*--*/
            documento_.Eliminar();
            return Redirect("~/admin/documento/");
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult buscarporano_tri_cat(int ano, int trimestre ,int categoria)
        {
            using (var db = new ProyectoContext())
            {
                var page = 1;
                // List<> rows ;
                /*es mejor enviarlo campor campo sino sale un error de json result[0]*/
                //  
                int totalRecords;
                var todoListsResults = (from p in db.documento
                                        join t in db.anos on p.anoId equals t.id
                                        join c in db.categoria on p.categoriaId equals c.id
                                        where (p.anoId == ano && p.categoriaId == categoria && p.trimestre == trimestre)
                                        select new
                                        {
                                            id = p.id,
                                            ano = p.anoId,
                                            valor_ano = t.ano,
                                            trimestre = p.trimestre,
                                            categoriaId = p.categoriaId,
                                            nombre = c.nombre,
                                            titulo_archivo = p.titulo_archivo,
                                            nombre_archivo = p.nombre_archivo,
                                            descripcion_archivo = p.descripcion_archivo,
                                            fecha = p.fecha
                                        }).ToList();

                totalRecords = todoListsResults.Count();
                /*---*/

               /* if (sord.ToUpper() == "ASC")
                {
                    todoListsResults = todoListsResults.OrderByDescending(s => s.id);
                    todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
                }
                else
                {
                    todoListsResults = todoListsResults.OrderBy(s => s.id);
                    todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
                }*/
                /*---*/

                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)page);

                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = todoListsResults,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
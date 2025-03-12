namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Web;

    [Table("documento")]
    public partial class documento
    {
        public int id { get; set; }

        public int? anoId { get; set; }

        public int? trimestre { get; set; }

        public int? categoriaId { get; set; }

        [StringLength(900)]
        public string titulo_archivo { get; set; }

        [StringLength(900)]
        public string nombre_archivo { get; set; }

        [StringLength(900)]
        public string descripcion_archivo { get; set; }

        public DateTime? fecha { get; set; }

        public virtual anos anos { get; set; }

        public virtual categoria categoria { get; set; }

        public List<documento> Listar()
        {
            using (var context = new ProyectoContext())
            {
                return context.documento.OrderByDescending(x => x.id)
                                          .ToList();
            }
        }

        public ViewDocumento editar(int id)
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var data = (from p in ctx.documento
                                join t in ctx.anos on p.anoId equals t.id
                                join c in ctx.categoria on p.categoriaId equals c.id
                                where (p.id == id)
                                select new ViewDocumento
                                {
                                    id = p.id,
                                    ano = p.anoId,
                                    valor_ano = t.ano,
                                    trimestre = p.trimestre,
                                    categoriaId = p.categoriaId,
                                    valor_categoria = c.nombre,
                                    titulo_archivo = p.titulo_archivo,
                                    nombre_archivo = p.nombre_archivo,
                                    descripcion_archivo = p.descripcion_archivo
                                }).FirstOrDefault();
                    return data;
                }
            }
            catch (DbEntityValidationException )
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public documento Obtener(int id)
        {
            var documento_ = new documento();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    documento_ = ctx.documento.Where(x => x.id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception )
            {
                throw;
            }
            return documento_;
        }

        public anos mostrar_ano(int id)
        {
            var anos_ = new anos();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    anos_ = ctx.anos.Where(x => x.id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception )
            {
                throw;
            }
            return anos_;
        }
        

        public void Eliminar()
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var enoticia = ctx.Entry(this);
                    enoticia.State = EntityState.Deleted;


                    var relativePath = "";
                    var absolutePath = "";
                    // if (this.nombre_archivo!= "" || this.nombre_archivo != null)
                    /*si el campo esta lleno entra sino no entra*/
                    if (this.nombre_archivo != "" && this.nombre_archivo != null)
                    {
                        relativePath = "~/documentos/" + this.nombre_archivo;
                        absolutePath = HttpContext.Current.Server.MapPath(relativePath);
                        System.IO.File.Delete(absolutePath);
                    }
                   
                    ctx.SaveChanges();
                }
            }
            catch (Exception )
            {
                throw;
            }

        }


        public ResponseModel Guardar(HttpPostedFileBase nombre_archivo)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var enoticia = ctx.Entry(this);
                    enoticia.State = EntityState.Modified;
                    /*una nueva noticia*/
                    if (this.id > 0)
                    {
                        if (nombre_archivo != null)
                        {
                            string archivo_espanol = nombre_archivo.FileName;
                            nombre_archivo.SaveAs(HttpContext.Current.Server.MapPath("~/documentos/" + archivo_espanol));
                            this.nombre_archivo = archivo_espanol;
                        }
                        else
                        {
                            enoticia.Property(x => x.nombre_archivo).IsModified = false;
                        }
                    }
                    /* si ES NUEVA la noticia*/
                    else
                    {
                        if (nombre_archivo != null)
                        {
                            string archivo_es = nombre_archivo.FileName;
                            nombre_archivo.SaveAs(HttpContext.Current.Server.MapPath("~/documentos/" + archivo_es));
                            this.nombre_archivo = nombre_archivo.FileName;
                        }
                        ctx.Entry(this).State = EntityState.Added;
                    }
                    rm.SetResponse(true);
                    ctx.SaveChanges();
                }
            }
            catch (DbEntityValidationException )
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

    }
}

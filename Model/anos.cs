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

    public partial class anos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public anos()
        {
            documento = new HashSet<documento>();
        }

        public int id { get; set; }

        public int? ano { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documento> documento { get; set; }


        public anos verificar_relaciones(int id)
        {
            var anos = new anos();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    anos = ctx.anos
                                    .Include("documento")
                                    .Where(x => x.id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return anos;
        }

        public void Eliminar()
        {
            var anos = new anos();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var enoticia = ctx.Entry(this);
                    enoticia.State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<anos> Listarano()
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    return ctx.anos.OrderByDescending(x => x.ano).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public anos editar(int id)
        {
            var anos_ = new anos();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    anos_ = ctx.anos.Where(x => x.id == id)
                        .FirstOrDefault();
                }
            }
            catch (Exception )
            {
                throw;
            }
            return anos_;
        }

        public int validarano(int? ano)
        {
            int valor; 
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var fila = ctx.anos.Where(x => x.ano == ano).FirstOrDefault();

                    if (fila != null)
                    {
                        valor = 1;
                    }else
                    {
                        valor = 0;
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
            return valor;
        }

        public ResponseModel Guardar()
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
                        //this.ano = enoticia;

                    }
                    /* si ES NUEVA la noticia*/
                    else
                    {
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

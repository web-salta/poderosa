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

    [Table("categoria")]
    public partial class categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public categoria()
        {
            documento = new HashSet<documento>();
        }

        public int id { get; set; }

        [StringLength(500)]
        public string nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documento> documento { get; set; }


        public List<categoria> Listarcategoria()
        {
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    return ctx.categoria.OrderByDescending(x => x.id).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public categoria verificar_relaciones(int id)
        {
            var categoria = new categoria();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    categoria = ctx.categoria
                                    .Include("documento")
                                    .Where(x => x.id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return categoria;
        }


        public void Eliminar()
        {
            var categoria = new categoria();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var ecategoria = ctx.Entry(this);
                    ecategoria.State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception )
            {
                throw;
            }

        }

        public categoria editar(int id)
        {
            var categoria_ = new categoria();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    categoria_ = ctx.categoria.Where(x => x.id == id)
                        .FirstOrDefault();
                }
            }
            catch (Exception )
            {
                throw;
            }
            return categoria_;
        }


        public int validarcategoria(string nombre)
        {
            int valor;
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    var fila = ctx.categoria.Where(x => x.nombre == nombre).FirstOrDefault();

                    if (fila != null)
                    {
                        valor = 1;
                    }
                    else
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
                   
                    /*una nueva noticia*/
                    if (this.id > 0)
                    {
                        enoticia.State = EntityState.Modified;
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

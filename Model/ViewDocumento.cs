using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewDocumento
    {
        public int id{ get; set; }
        public int? ano { get; set; }
        public int? valor_ano { get; set; }
        public int? trimestre { get; set; }
        public int? categoriaId { get; set; }
        public string valor_categoria { get; set; }
        public string titulo_archivo { get; set; }
        public string nombre_archivo { get; set; }
        public string descripcion_archivo { get; set; }
        public DateTime? fecha { get; set; }



        public ViewDocumento editar(int id)
        {
            //var documento_ = new documento();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    // documento_ = ctx.documento.Where(x => x.id == id)
                    // .FirstOrDefault();
                    var data = new ViewDocumento();
                  data= (from p in ctx.documento
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
    }
}

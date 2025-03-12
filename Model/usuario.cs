namespace Model
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("usuario")]
    public partial class usuario
    {
        [Key]
        public int id { get; set; }

        [StringLength(250)]
        public string nom_usu { get; set; }

        [StringLength(250)]
        public string clave { get; set; }


        public ResponseModel Acceder(string Email, string Password)
        {
            var rm = new ResponseModel();

            try
            {
                using (var ctx = new ProyectoContext())
                {
                    Password = HashHelper.MD5(Password);

                    var usuario = ctx.usuario.Where(x => x.nom_usu == Email)
                                             .Where(x => x.clave == Password)
                                             .SingleOrDefault();
                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario o contraseña incorrecta");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

        public usuario Obtener(int id)
        {
            var usuario = new usuario();

            try
            {
                using (var ctx = new ProyectoContext())
                {
                    usuario = ctx.usuario.Where(x => x.id == id)
                                         .SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return usuario;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/equipaje")]
    public class EquipajeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Equipaje equipaje = new Equipaje();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT EQUI_ID, EQUI_TIPO, EQUI_PRECIO, 
                                                           EQUI_DESCRIPCION
                                                           FROM EQUIPAJE WHERE EQUI_ID = @EQUI_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EQUI_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        equipaje.EQUI_ID = sqlDataReader.GetInt32(0);
                        equipaje.EQUI_TIPO = sqlDataReader.GetString(1);
                        equipaje.EQUI_PRECIO = sqlDataReader.GetInt32(2);
                        equipaje.EQUI_DESCRIPCION = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(equipaje);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Equipaje> equipajes = new List<Equipaje>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT EQUI_ID, EQUI_TIPO, EQUI_PRECIO, 
                                                           EQUI_DESCRIPCION
                                                           FROM EQUIPAJE", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Equipaje equipaje = new Equipaje()
                        {
                            EQUI_ID = sqlDataReader.GetInt32(0),
                            EQUI_TIPO = sqlDataReader.GetString(1),
                            EQUI_PRECIO = sqlDataReader.GetInt32(2),
                            EQUI_DESCRIPCION = sqlDataReader.GetString(3)

                        };

                        equipajes.Add(equipaje);
                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(equipajes);
        }

        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Equipaje equipaje)
        {
            if (equipaje == null)
                return BadRequest();

            if (RegistrarEquipaje(equipaje))
                return Ok();
            else
                return InternalServerError();
        }

        private bool RegistrarEquipaje(Equipaje equipaje)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO EQUIPAJE (EQUI_TIPO, EQUI_PRECIO, 
                                                           EQUI_DESCRIPCION) VALUES (@EQUI_TIPO, @EQUI_PRECIO, 
                                                           @EQUI_DESCRIPCION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EQUI_TIPO", equipaje.EQUI_TIPO);
                    sqlCommand.Parameters.AddWithValue("@EQUI_PRECIO", equipaje.EQUI_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@EQUI_DESCRIPCION", equipaje.EQUI_DESCRIPCION);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();

                }

            }

            catch (Exception)
            {
                throw;
            }

            return resultado;
        }

        [HttpPut]
        public IHttpActionResult Put(Equipaje equipaje)
        {
            if (equipaje == null)
                return BadRequest();

            if (ActualizarEquipaje(equipaje))
                return Ok(equipaje);

            else

                return InternalServerError();
        }

        private bool ActualizarEquipaje(Equipaje equipaje)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE EQUIPAJE SET
                                                             EQUI_TIPO = @EQUI_TIPO,
                                                             EQUI_PRECIO = @EQUI_PRECIO,
                                                             EQUI_DESCRIPCION = @EQUI_DESCRIPCION
                                                             WHERE EQUI_ID = @EQUI_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EQUI_ID", equipaje.EQUI_ID);
                    sqlCommand.Parameters.AddWithValue("@EQUI_TIPO", equipaje.EQUI_TIPO);
                    sqlCommand.Parameters.AddWithValue("@EQUI_PRECIO", equipaje.EQUI_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@EQUI_DESCRIPCION", equipaje.EQUI_DESCRIPCION);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();

                }

            }

            catch (Exception)
            {
                throw;
            }

            return resultado;

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (EliminarEquipaje(id))
                return Ok(id);
            else
                return InternalServerError();

        }

        private bool EliminarEquipaje(int id)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE EQUIPAJE
                                                            WHERE EQUI_ID = @EQUI_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EQUI_ID", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();

                }

            }

            catch (Exception)
            {
                throw;
            }

            return resultado;

        }
    }
}

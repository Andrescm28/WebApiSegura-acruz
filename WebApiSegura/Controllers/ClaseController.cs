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
    [RoutePrefix("api/clase")]
    public class ClaseController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Clase clase = new Clase();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CLAS_ID, CLAS_TIPO, CLAS_PRECIO, 
                                                           CLAS_DESCRIPCION
                                                           FROM CLASE WHERE CLAS_ID = @CLAS_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CLAS_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        clase.CLAS_ID = sqlDataReader.GetInt32(0);
                        clase.CLAS_TIPO = sqlDataReader.GetString(1);
                        clase.CLAS_PRECIO = sqlDataReader.GetInt32(2);
                        clase.CLAS_DESCRIPCION = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(clase);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Clase> clases = new List<Clase>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CLAS_ID, CLAS_TIPO, CLAS_PRECIO, 
                                                           CLAS_DESCRIPCION
                                                           FROM CLASE", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Clase clase = new Clase()
                        {
                            CLAS_ID = sqlDataReader.GetInt32(0),
                            CLAS_TIPO = sqlDataReader.GetString(1),
                            CLAS_PRECIO = sqlDataReader.GetInt32(2),
                            CLAS_DESCRIPCION = sqlDataReader.GetString(3)

                        };

                        clases.Add(clase);
                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(clases);
        }

        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Clase clase)
        {
            if (clase == null)
                return BadRequest();

            if (RegistrarClase(clase))
                return Ok();
            else
                return InternalServerError();
        }

        private bool RegistrarClase(Clase clase)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO CLASE (CLAS_TIPO, CLAS_PRECIO, 
                                                           CLAS_DESCRIPCION) VALUES (@CLAS_TIPO, @CLAS_PRECIO, 
                                                           @CLAS_DESCRIPCION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CLAS_TIPO", clase.CLAS_TIPO);
                    sqlCommand.Parameters.AddWithValue("@CLAS_PRECIO", clase.CLAS_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@CLAS_DESCRIPCION", clase.CLAS_DESCRIPCION);

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
        public IHttpActionResult Put(Clase clase)
        {
            if (clase == null)
                return BadRequest();

            if (ActualizarClase(clase))
                return Ok(clase);

            else

                return InternalServerError();
        }

        private bool ActualizarClase(Clase clase)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE CLASE SET
                                                             CLAS_TIPO = @CLAS_TIPO,
                                                             CLAS_PRECIO = @CLAS_PRECIO,
                                                             CLAS_DESCRIPCION = @CLAS_DESCRIPCION
                                                             WHERE CLAS_ID = @CLAS_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CLAS_ID", clase.CLAS_ID);
                    sqlCommand.Parameters.AddWithValue("@CLAS_TIPO", clase.CLAS_TIPO);
                    sqlCommand.Parameters.AddWithValue("@CLAS_PRECIO", clase.CLAS_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@CLAS_DESCRIPCION", clase.CLAS_DESCRIPCION);

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

            if (EliminarClase(id))
                return Ok(id);
            else
                return InternalServerError();

        }

        private bool EliminarClase(int id)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE CLASE
                                                            WHERE CLAS_ID = @CLAS_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CLAS_ID", id);

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

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
    [RoutePrefix("api/seguro")]
    public class SeguroController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Seguro seguro = new Seguro();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT SEG_ID, SEG_TIPO, SEG_PRECIO, 
                                                           SEG_DESCRIPCION
                                                           FROM SEGURO WHERE SEG_ID = @SEG_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SEG_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        seguro.SEG_ID = sqlDataReader.GetInt32(0);
                        seguro.SEG_TIPO = sqlDataReader.GetString(1);
                        seguro.SEG_PRECIO = sqlDataReader.GetInt32(2);
                        seguro.SEG_DESCRIPCION = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(seguro);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Seguro> seguros = new List<Seguro>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT SEG_ID, SEG_TIPO, SEG_PRECIO, 
                                                           SEG_DESCRIPCION
                                                           FROM SEGURO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Seguro seguro = new Seguro()
                        {
                            SEG_ID = sqlDataReader.GetInt32(0),
                            SEG_TIPO = sqlDataReader.GetString(1),
                            SEG_PRECIO = sqlDataReader.GetInt32(2),
                            SEG_DESCRIPCION = sqlDataReader.GetString(3)

                        };

                        seguros.Add(seguro);
                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(seguros);
        }

        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Seguro seguro)
        {
            if (seguro == null)
                return BadRequest();

            if (RegistrarSeguro(seguro))
                return Ok();
            else
                return InternalServerError();
        }

        private bool RegistrarSeguro(Seguro seguro)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO SEGURO (SEG_TIPO, SEG_PRECIO, 
                                                           SEG_DESCRIPCION) VALUES (@SEG_TIPO, @SEG_PRECIO, 
                                                           @SEG_DESCRIPCION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SEG_TIPO", seguro.SEG_TIPO);
                    sqlCommand.Parameters.AddWithValue("@SEG_PRECIO", seguro.SEG_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@SEG_DESCRIPCION", seguro.SEG_DESCRIPCION);

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
        public IHttpActionResult Put(Seguro seguro)
        {
            if (seguro == null)
                return BadRequest();

            if (ActualizarSeguro(seguro))
                return Ok(seguro);

            else

                return InternalServerError();
        }

        private bool ActualizarSeguro(Seguro seguro)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE SEGURO SET
                                                             SEG_TIPO = @SEG_TIPO,
                                                             SEG_PRECIO = @SEG_PRECIO,
                                                             SEG_DESCRIPCION = @SEG_DESCRIPCION
                                                             WHERE SEG_ID = @SEG_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SEG_ID", seguro.SEG_ID);
                    sqlCommand.Parameters.AddWithValue("@SEG_TIPO", seguro.SEG_TIPO);
                    sqlCommand.Parameters.AddWithValue("@SEG_PRECIO", seguro.SEG_PRECIO);
                    sqlCommand.Parameters.AddWithValue("@SEG_DESCRIPCION", seguro.SEG_DESCRIPCION);

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

            if (EliminarSeguro(id))
                return Ok(id);
            else
                return InternalServerError();

        }

        private bool EliminarSeguro(int id)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE SEGURO
                                                            WHERE SEG_ID = @SEG_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SEG_ID", id);

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

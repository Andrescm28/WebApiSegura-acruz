using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    public class AvionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Avion> aviones = new List<Avion>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AVI_ID, AVI_CODIGO_AVION, AVI_TIPO, 
                                                           AVI_DESCRIPCION
                                                           FROM AVION", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Avion avion = new Avion()
                        {
                            AVI_ID = sqlDataReader.GetInt32(0),
                            AVI_CODIGO_AVION = sqlDataReader.GetInt32(1),
                            AVI_TIPO = sqlDataReader.GetString(2),
                            AVI_DESCRIPCION = sqlDataReader.GetString(3)

                        };

                        aviones.Add(avion);
                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(aviones);
        }

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Avion avion = new Avion();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AVI_ID, AVI_CODIGO_AVION, AVI_TIPO, 
                                                           AVI_DESCRIPCION
                                                           FROM AVION WHERE AVI_ID = @AVI_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        avion.AVI_ID = sqlDataReader.GetInt32(0);
                        avion.AVI_CODIGO_AVION = sqlDataReader.GetInt32(1);
                        avion.AVI_TIPO = sqlDataReader.GetString(2);
                        avion.AVI_DESCRIPCION = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(avion);
        }

        [HttpPost]
        [Route("post")]
        public IHttpActionResult Post(Avion avion)
        {
            if (avion == null)
                return BadRequest();

            if (RegistrarAvion(avion))
                return Ok();
            else
                return InternalServerError();
        }

        private bool RegistrarAvion(Avion avion)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO AVION (AVI_CODIGO_AVION, AVI_TIPO, 
                                                           AVI_DESCRIPCION) VALUES (@AVI_CODIGO_AVION, @AVI_TIPO, 
                                                           @AVI_DESCRIPCION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_TIPO", avion.AVI_TIPO);
                    sqlCommand.Parameters.AddWithValue("@AVI_CODIGO_AVION", avion.AVI_CODIGO_AVION);
                    sqlCommand.Parameters.AddWithValue("@AVI_DESCRIPCION", avion.AVI_DESCRIPCION);

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
        public IHttpActionResult Put(Avion avion)
        {
            if (avion == null)
                return BadRequest();

            if (ActualizarAvion(avion))
                return Ok(avion);

            else

                return InternalServerError();
        }

        private bool ActualizarAvion(Avion Avion)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE AVION SET
                                                             AVI_CODIGO_AVION = @AVI_CODIGO_AVION,
                                                             AVI_TIPO = @AVI_TIPO,
                                                             AVI_DESCRIPCION = @AVI_DESCRIPCION
                                                             WHERE AVI_ID = @AVI_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_ID", Avion.AVI_ID);
                    sqlCommand.Parameters.AddWithValue("@AVI_CODIGO_AVION", Avion.AVI_CODIGO_AVION);
                    sqlCommand.Parameters.AddWithValue("@AVI_TIPO", Avion.AVI_TIPO);
                    sqlCommand.Parameters.AddWithValue("@AVI_DESCRIPCION", Avion.AVI_DESCRIPCION);

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

            if (EliminarAvion(id))
                return Ok(id);
            else
                return InternalServerError();

        }

        private bool EliminarAvion(int id)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE AVION
                                                            WHERE AVI_ID = @AVI_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AVI_ID", id);

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
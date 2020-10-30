using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/aeropuerto")]
    public class AeropuertoController : ApiController
    {
       
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Aeropuerto> aeropuertos = new List<Aeropuerto>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AERO_ID, AERO_CODIGO_AEROPUERTO, AERO_TIPO, 
                                                           AERO_DESCRIPCION
                                                           FROM AEROPUERTO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Aeropuerto aeropuerto = new Aeropuerto()
                        {
                            AERO_ID = sqlDataReader.GetInt32(0),
                            AERO_CODIGO_AEROPUERTO = sqlDataReader.GetInt32(1),
                            AERO_TIPO = sqlDataReader.GetString(2),
                            AERO_DESCRIPCION = sqlDataReader.GetString(3)

                        };

                        aeropuertos.Add(aeropuerto);
                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(aeropuertos);
        }
        
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Aeropuerto aeropuerto = new Aeropuerto();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AERO_ID, AERO_CODIGO_AEROPUERTO, AERO_TIPO, 
                                                           AERO_DESCRIPCION
                                                           FROM AEROPUERTO WHERE AERO_ID = @AERO_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AERO_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        aeropuerto.AERO_ID = sqlDataReader.GetInt32(0);
                        aeropuerto.AERO_CODIGO_AEROPUERTO = sqlDataReader.GetInt32(1);
                        aeropuerto.AERO_TIPO = sqlDataReader.GetString(2);
                        aeropuerto.AERO_DESCRIPCION = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(aeropuerto);
        }

        [HttpPost]
        [Route("post")]
        public IHttpActionResult Post(Aeropuerto aeropuerto)
        {
            if (aeropuerto == null)
                return BadRequest();

            if (RegistrarAeropuerto(aeropuerto))
                return Ok();
            else
                return InternalServerError();
        }

        private bool RegistrarAeropuerto(Aeropuerto aeropuerto)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO AEROPUERTO (AERO_CODIGO_AEROPUERTO, AERO_TIPO, 
                                                           AERO_DESCRIPCION) VALUES (@AERO_CODIGO_AEROPUERTO, @AERO_TIPO, 
                                                           @AERO_DESCRIPCION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AERO_TIPO", aeropuerto.AERO_TIPO);
                    sqlCommand.Parameters.AddWithValue("@AERO_CODIGO_AEROPUERTO", aeropuerto.AERO_CODIGO_AEROPUERTO);
                    sqlCommand.Parameters.AddWithValue("@AERO_DESCRIPCION", aeropuerto.AERO_DESCRIPCION);

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
        public IHttpActionResult Put(Aeropuerto aeropuerto)
        {
            if (aeropuerto == null)
                return BadRequest();

            if (ActualizarAeropuerto(aeropuerto))
                return Ok(aeropuerto);

            else

                return InternalServerError();
        }

        private bool ActualizarAeropuerto(Aeropuerto aeropuerto)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE AEROPUERTO SET
                                                             AERO_CODIGO_AEROPUERTO = @AERO_CODIGO_AEROPUERTO,
                                                             AERO_TIPO = @AERO_TIPO,
                                                             AERO_DESCRIPCION = @AERO_DESCRIPCION
                                                             WHERE AERO_ID = @AERO_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AERO_ID", aeropuerto.AERO_ID);
                    sqlCommand.Parameters.AddWithValue("@AERO_CODIGO_AEROPUERTO", aeropuerto.AERO_CODIGO_AEROPUERTO);
                    sqlCommand.Parameters.AddWithValue("@AERO_TIPO", aeropuerto.AERO_TIPO);
                    sqlCommand.Parameters.AddWithValue("@AERO_DESCRIPCION", aeropuerto.AERO_DESCRIPCION);

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

            if (EliminarAeropuerto(id))
                return Ok(id);
            else
                return InternalServerError();

        }

        private bool EliminarAeropuerto(int id)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE AEROPUERTO
                                                            WHERE AERO_ID = @AERO_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AERO_ID", id);

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
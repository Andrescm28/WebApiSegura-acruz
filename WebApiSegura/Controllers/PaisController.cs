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
    public class PaisController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Pais> paises = new List<Pais>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PAI_ID, PAI_CODIGO_PAIS, PAI_TIPO, 
                                                           PAI_DESCRIPCION
                                                           FROM PAIS", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Pais pais = new Pais()
                        {
                            PAI_ID = sqlDataReader.GetInt32(0),
                            PAI_CODIGO_PAIS = sqlDataReader.GetInt32(1),
                            PAI_TIPO = sqlDataReader.GetString(2),
                            PAI_DESCRIPCION = sqlDataReader.GetString(3)

                        };

                        paises.Add(pais);
                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(paises);
        }

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Pais pais = new Pais();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PAI_ID, PAI_CODIGO_PAIS, PAI_TIPO, 
                                                           PAI_DESCRIPCION
                                                           FROM PAIS WHERE PAI_ID = @PAI_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAI_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        pais.PAI_ID = sqlDataReader.GetInt32(0);
                        pais.PAI_CODIGO_PAIS = sqlDataReader.GetInt32(1);
                        pais.PAI_TIPO = sqlDataReader.GetString(2);
                        pais.PAI_DESCRIPCION = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();

                }

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(pais);
        }

        [HttpPost]
        [Route("post")]
        public IHttpActionResult Post(Pais pais)
        {
            if (pais == null)
                return BadRequest();

            if (RegistrarPais(pais))
                return Ok();
            else
                return InternalServerError();
        }

        private bool RegistrarPais(Pais pais)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PAIS (PAI_CODIGO_PAIS, PAI_TIPO, 
                                                           PAI_DESCRIPCION) VALUES (@PAI_CODIGO_PAIS, @PAI_TIPO, 
                                                           @PAI_DESCRIPCION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAI_TIPO", pais.PAI_TIPO);
                    sqlCommand.Parameters.AddWithValue("@PAI_CODIGO_PAIS", pais.PAI_CODIGO_PAIS);
                    sqlCommand.Parameters.AddWithValue("@PAI_DESCRIPCION", pais.PAI_DESCRIPCION);

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
        public IHttpActionResult Put(Pais pais)
        {
            if (pais == null)
                return BadRequest();

            if (ActualizarPais(pais))
                return Ok(pais);

            else

                return InternalServerError();
        }

        private bool ActualizarPais(Pais pais)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE PAIS SET
                                                             PAI_CODIGO_PAIS = @PAI_CODIGO_PAIS,
                                                             PAI_TIPO = @PAI_TIPO,
                                                             PAI_DESCRIPCION = @PAI_DESCRIPCION
                                                             WHERE PAI_ID = @PAI_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAI_ID", pais.PAI_ID);
                    sqlCommand.Parameters.AddWithValue("@PAI_CODIGO_PAIS", pais.PAI_CODIGO_PAIS);
                    sqlCommand.Parameters.AddWithValue("@PAI_TIPO", pais.PAI_TIPO);
                    sqlCommand.Parameters.AddWithValue("@PAI_DESCRIPCION", pais.PAI_DESCRIPCION);

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

            if (EliminarPais(id))
                return Ok(id);
            else
                return InternalServerError();

        }

        private bool EliminarPais(int id)
        {
            bool resultado = false;

            try
            {

                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))

                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE PAIS
                                                            WHERE PAI_ID = @PAI_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PAI_ID", id);

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
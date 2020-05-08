using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class RepositorioContratoAlquiler : RepositorioBase, IRepositorioContratoAlquiler
    {
		public RepositorioContratoAlquiler(IConfiguration configuration):base(configuration)
		{

		}

		public int Alta(ContratoAlquiler ca)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO ContratoAlquiler (Monto, FechaInicio, FechaFinalizacion, InquilinoId, InmuebleId, Estado) " +
					$"VALUES (@monto, @fechaInicio, @fechaFinalizacion, @inquilinoId, @inmuebleId, @estado);" +
					$"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@monto", ca.Monto);
					command.Parameters.AddWithValue("@fechaInicio", ca.FechaInicio);
					command.Parameters.AddWithValue("@fechaFinalizacion", ca.FechaFinalizacion);
					command.Parameters.AddWithValue("@inquilinoId", ca.InquilinoId);
					command.Parameters.AddWithValue("@inmuebleId", ca.InmuebleId);
					command.Parameters.AddWithValue("@estado", ca.Estado);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					ca.Id = res;
					connection.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM ContratoAlquiler WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Modificacion(ContratoAlquiler ca)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE ContratoAlquiler SET Monto=@monto, FechaInicio=@fechaInicio, FechaFinalizacion=@fechaFinalizacion, InquilinoId=@inquilinoId, InmuebleId=@inmuebleId, Estado=@estado " +
					$"WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{

					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@monto", ca.Monto);
					command.Parameters.AddWithValue("@fechaInicio", ca.FechaInicio);
					command.Parameters.AddWithValue("@fechaFinalizacion", ca.FechaFinalizacion);
					command.Parameters.AddWithValue("@inquilinoId", ca.InquilinoId);
					command.Parameters.AddWithValue("@inmuebleId", ca.InmuebleId);
					command.Parameters.AddWithValue("@estado", ca.Estado);
					command.Parameters.AddWithValue("@id", ca.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<ContratoAlquiler> ObtenerTodos()
		{
			IList<ContratoAlquiler> res = new List<ContratoAlquiler>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT ca.Id, Monto, FechaInicio, FechaFinalizacion, InquilinoId, InmuebleId, ca.Estado, inq.Nombre, inq.Apellido , i.Direccion " +
					$" FROM ContratoAlquiler ca INNER JOIN Inmueble i ON ca.InmuebleId = i.Id " +
				    $"INNER JOIN Inquilino inq ON ca.InquilinoId = inq.Id ";
				
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						ContratoAlquiler ca = new ContratoAlquiler
						{
							Id = reader.GetInt32(0),
							Monto = reader.GetDecimal(1),
							FechaInicio = reader.GetDateTime(2),
							FechaFinalizacion = reader.GetDateTime(3),
							InquilinoId = reader.GetInt32(4),
							InmuebleId = reader.GetInt32(5),
							Estado = reader.GetBoolean(6),
							Inquilino = new Inquilino
							{
								Id = reader.GetInt32(4),
								Nombre = reader.GetString(7),
								Apellido = reader.GetString(8),
							},
							Inmueble = new Inmueble
							{
								Id = reader.GetInt32(5),
								Direccion = reader.GetString(9),
							}							
						};
						res.Add(ca);
					}
					connection.Close();
				}
			}
			return res;
		}

		public ContratoAlquiler ObtenerPorId(int id)
		{
			ContratoAlquiler ca = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT Id, Monto, FechaInicio, FechaFinalizacion, InquilinoId, InmuebleId, Estado FROM ContratoAlquiler" +
					$" WHERE Id=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						ca = new ContratoAlquiler
						{
							Id = reader.GetInt32(0),
							Monto = reader.GetDecimal(1),
							FechaInicio = reader.GetDateTime(2),
							FechaFinalizacion = reader.GetDateTime(3),
							InquilinoId = reader.GetInt32(4),
							InmuebleId = reader.GetInt32(5),
							Estado = reader.GetBoolean(6),
						};
					}
					connection.Close();
				}
			}
			return ca;
		}

		public IList<ContratoAlquiler> ObtenerPorInmuebleId(int id)
		{
			IList<ContratoAlquiler> res = new List<ContratoAlquiler>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT ca.Id, Monto, FechaInicio, FechaFinalizacion, InquilinoId, InmuebleId, ca.Estado, inq.Nombre, inq.Apellido , i.Direccion " +
					$" FROM ContratoAlquiler ca INNER JOIN Inmueble i ON ca.InmuebleId = i.Id " +
					$"INNER JOIN Inquilino inq ON ca.InquilinoId = inq.Id " +
					$"WHERE InmuebleId = @inmuebleId";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@inmuebleId", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						ContratoAlquiler ca = new ContratoAlquiler
						{
							Id = reader.GetInt32(0),
							Monto = reader.GetDecimal(1),
							FechaInicio = reader.GetDateTime(2),
							FechaFinalizacion = reader.GetDateTime(3),
							InquilinoId = reader.GetInt32(4),
							InmuebleId = reader.GetInt32(5),
							Estado = reader.GetBoolean(6),
							Inquilino = new Inquilino
							{
								Id = reader.GetInt32(4),
								Nombre = reader.GetString(7),
								Apellido = reader.GetString(8),
							},
							Inmueble = new Inmueble
							{
								Id = reader.GetInt32(5),
								Direccion = reader.GetString(9),
							}
						};
						res.Add(ca);
					}
					connection.Close();
				}
			}
			return res;
		}
		public IList<ContratoAlquiler> ObtenerTodosVigentes(DateTime fechaInicio, DateTime fechaFinal)
		{
			IList<ContratoAlquiler> res = new List<ContratoAlquiler>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT ca.Id, Monto, FechaInicio, FechaFinalizacion, InquilinoId, InmuebleId, ca.Estado, inq.Nombre, inq.Apellido , i.Direccion " +
					$" FROM ContratoAlquiler ca INNER JOIN Inmueble i ON ca.InmuebleId = i.Id " +
					$"INNER JOIN Inquilino inq ON ca.InquilinoId = inq.Id " +
					$"WHERE ca.Estado = 1" +
					$"AND((FechaInicio < @fechaInicio)AND(FechaFinalizacion > @fechaFinal))" +
					$"OR((FechaInicio BETWEEN @fechaInicio AND @fechaFinal)AND(FechaFinalizacion BETWEEN @fechaInicio AND @fechaFinal))"+
					$"OR((FechaInicio < @fechaInicio)AND(FechaFinalizacion BETWEEN @fechaInicio AND @fechaFinal))"+
					$"OR((FechaInicio BETWEEN @fechaInicio AND @fechaFinal)AND(FechaFinalizacion > @fechaFinal));";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = fechaInicio;
					command.Parameters.Add("@fechaFinal", SqlDbType.DateTime).Value = fechaFinal;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						ContratoAlquiler ca = new ContratoAlquiler
						{
							Id = reader.GetInt32(0),
							Monto = reader.GetDecimal(1),
							FechaInicio = reader.GetDateTime(2),
							FechaFinalizacion = reader.GetDateTime(3),
							InquilinoId = reader.GetInt32(4),
							InmuebleId = reader.GetInt32(5),
							Estado = reader.GetBoolean(6),
							Inquilino = new Inquilino
							{
								Id = reader.GetInt32(4),
								Nombre = reader.GetString(7),
								Apellido = reader.GetString(8),
							},
							Inmueble = new Inmueble
							{
								Id = reader.GetInt32(5),
								Direccion = reader.GetString(9),
							}
						};
						res.Add(ca);
					}
					connection.Close();
				}
			}
			return res;
		}

	}
}

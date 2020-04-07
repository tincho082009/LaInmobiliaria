using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class RepositorioPago
    {
		private readonly string connectionString;
		private readonly IConfiguration configuration;

		public RepositorioPago(IConfiguration configuration)
		{
			this.configuration = configuration;
			connectionString = configuration["ConnectionStrings:DefaultConnection"];
		}
		public int Alta(Pago p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Pago (NroPago, FechaPago, Importe, ContratoId) " +
					$"VALUES (@nroPago, @fechaPago, @importe, @contratoId);" +
					$"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nroPago", p.NroPago);
					command.Parameters.AddWithValue("@fechaPago", p.FechaPago);
					command.Parameters.AddWithValue("@importe", p.Importe);
					command.Parameters.AddWithValue("@contratoId", p.ContratoId);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					p.Id = res;
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
				string sql = $"DELETE FROM Pago WHERE Id = @id";
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
		public int Modificacion(Pago p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Pago SET NroPago=@nroPago, FechaPago=@fechaPago, Importe=@importe, ContratoId=@contratoId " +
					$"WHERE Id = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{

					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nroPago", p.NroPago);
					command.Parameters.AddWithValue("@fechaPago", p.FechaPago);
					command.Parameters.AddWithValue("@importe", p.Importe);
					command.Parameters.AddWithValue("@contratoId", p.ContratoId);
					command.Parameters.AddWithValue("@id", p.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Pago> ObtenerTodos()
		{
			IList<Pago> res = new List<Pago>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT p.Id, NroPago, FechaPago, Importe, ContratoId," +
					$" ca.Monto, ca.FechaInicio, ca.FechaFinalizacion" +
					$" FROM Pago p INNER JOIN ContratoAlquiler ca ON p.ContratoId = ca.Id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Pago p = new Pago
						{
							Id = reader.GetInt32(0),
							NroPago = reader.GetInt32(1),
							FechaPago = reader.GetDateTime(2),
							Importe = reader.GetDecimal(3),
							ContratoId = reader.GetInt32(4),
							ContratoAlquiler = new ContratoAlquiler {
								Id = reader.GetInt32(4),
								Monto = reader.GetDecimal(5),
								FechaInicio = reader.GetDateTime(6),
								FechaFinalizacion = reader.GetDateTime(7),
							}
			
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Pago ObtenerPorId(int id)
		{
			Pago p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT p.Id, NroPago, FechaPago, Importe, ContratoId, ca.Monto, ca.FechaInicio, ca.FechaFinalizacion " +
					$"FROM Pago p INNER JOIN ContratoAlquiler ca ON p.ContratoId = ca.Id" +
					$" WHERE p.Id=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Pago
						{
							Id = reader.GetInt32(0),
							NroPago = reader.GetInt32(1),
							FechaPago = reader.GetDateTime(2),
							Importe = reader.GetDecimal(3),
							ContratoId = reader.GetInt32(4),
							ContratoAlquiler = new ContratoAlquiler
							{
								Id = reader.GetInt32(4),
								Monto = reader.GetDecimal(5),
								FechaInicio = reader.GetDateTime(6),
								FechaFinalizacion = reader.GetDateTime(7),
							}
						};
					}
					connection.Close();
				}
			}
			return p;
		}
		public IList<Pago> ObtenerTodosPorContratoId(int contratoId)
		{
			IList<Pago> res = new List<Pago>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT p.Id, NroPago, FechaPago, Importe, ContratoId," +
					$" ca.Monto, ca.FechaInicio, ca.FechaFinalizacion" +
					$" FROM Pago p INNER JOIN ContratoAlquiler ca ON p.ContratoId = ca.Id" +
					$" WHERE p.ContratoId = @contratoId";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@contratoId", SqlDbType.Int).Value = contratoId;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Pago p = new Pago
						{
							Id = reader.GetInt32(0),
							NroPago = reader.GetInt32(1),
							FechaPago = reader.GetDateTime(2),
							Importe = reader.GetDecimal(3),
							ContratoId = reader.GetInt32(4),
							ContratoAlquiler = new ContratoAlquiler
							{
								Id = reader.GetInt32(4),
								Monto = reader.GetDecimal(5),
								FechaInicio = reader.GetDateTime(6),
								FechaFinalizacion = reader.GetDateTime(7),
							}

						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}
	}
}

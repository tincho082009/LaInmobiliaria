using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class RepositorioPropietario
    {
		private readonly string connectionString;
		private readonly IConfiguration configuration;

		public RepositorioPropietario(IConfiguration configuration)
			{
			this.configuration = configuration;
			connectionString = configuration["ConnectionStrings:DefaultConnection"];
		}

		public int Alta(Propietario p){
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Propietario (Dni, Nombre, Apellido, Telefono, Email) " +
					$"VALUES (@dni, @nombre, @apellido, @telefono, @email);" +
					$"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@telefono", p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email);
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
					string sql = $"DELETE FROM Propietario WHERE Id = @id";
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
			public int Modificacion(Propietario p)
			{
				int res = -1;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"UPDATE Propietario SET Dni=@dni, Nombre=@nombre, Apellido=@apellido, Telefono=@telefono, Email=@email " +
						$"WHERE Id = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{

						command.CommandType = CommandType.Text;
						command.Parameters.AddWithValue("@dni", p.Dni);
						command.Parameters.AddWithValue("@nombre", p.Nombre);
						command.Parameters.AddWithValue("@apellido", p.Apellido);
						command.Parameters.AddWithValue("@telefono", p.Telefono);
						command.Parameters.AddWithValue("@email", p.Email);
						command.Parameters.AddWithValue("@id", p.Id);
						connection.Open();
						res = command.ExecuteNonQuery();
						connection.Close();
					}
				}
				return res;
			}

		public IList<Propietario> ObtenerTodos()
		{
			IList<Propietario> res = new List<Propietario>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT Id, Dni, Nombre, Apellido, Telefono, Email" +
					$" FROM Propietario";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Propietario p = new Propietario
						{
							Id = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Nombre = reader.GetString(2),
							Apellido = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Propietario ObtenerPorId(int id)
		{
			Propietario p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT Id, Dni, Nombre, Apellido, Telefono, Email FROM Propietario" +
					$" WHERE Id=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Propietario
						{
							Id = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Nombre = reader.GetString(2),
							Apellido = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
						};
					}
					connection.Close();
				}
			}
			return p;
		}

		public Propietario ObtenerPorEmail(string email)
			{
			Propietario p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT Id, Dni, Nombre, Apellido, Telefono, Email FROM Propietario" +
					$" WHERE Email=@email";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Propietario
						{
							Id = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Nombre = reader.GetString(2),
							Apellido = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
						};
					}
					connection.Close();
				}
			}
			return p;
		}

			public IList<Propietario> BuscarPorNombre(string nombre)
			{
				List<Propietario> res = new List<Propietario>();
				Propietario p = null;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"SELECT Id, Dni, Nombre, Apellido, Telefono, Email FROM Propietario" +
						$" WHERE Nombre LIKE %@nombre% OR Apellido LIKE %@nombre";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
						command.CommandType = CommandType.Text;
						connection.Open();
						var reader = command.ExecuteReader();
						while (reader.Read())
						{
							p = new Propietario
							{
								Id = reader.GetInt32(0),
								Dni = reader.GetString(1),
								Nombre = reader.GetString(2),
								Apellido = reader.GetString(3),
								Telefono = reader.GetString(4),
								Email = reader.GetString(5),
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


using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class RepositorioFoto : RepositorioBase, IRepositorioFoto
    {
        public RepositorioFoto(IConfiguration configuration) : base(configuration)
        {

        }

        public int Alta(Foto p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Foto (Url, Tipo, InmuebleId) " +
                    $"VALUES (@url, @tipo, @inmuebleId);" +
                    $"SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@url", p.Url);
                    command.Parameters.AddWithValue("@tipo", p.Tipo);
                    command.Parameters.AddWithValue("@inmuebleId", p.InmuebleId);
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
                string sql = $"DELETE FROM Foto WHERE Id = @id";
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

        public int Modificacion(Foto p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Foto SET Url=@url, Tipo=@tipo, InmuebleId=@inmuebleId " +
                    $"WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@url", p.Url);
                    command.Parameters.AddWithValue("@tipo", p.Tipo);
                    command.Parameters.AddWithValue("@inmuebleId", p.InmuebleId);
                    command.Parameters.AddWithValue("@id", p.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public Foto ObtenerPorId(int id)
        {
            Foto f = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT f.Id, Url, f.Tipo, InmuebleId, i.Direccion" +
                    $" FROM Foto f INNER JOIN Inmueble i ON f.InmuebleId = i.Id" +
                    $" WHERE f.Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        f = new Foto
                        {
                            Id = reader.GetInt32(0),
                            Url = reader.GetString(1),
                            Tipo = reader.GetString(2),                           
                            InmuebleId = reader.GetInt32(3),
                            inmueble = new Inmueble
                            {
                                Id = reader.GetInt32(3),
                                Direccion = reader.GetString(4),
                            }
                        };
                    }
                    connection.Close();
                }
            }
            return f;
        }

        public IList<Foto> ObtenerTodos()
        {
            IList<Foto> res = new List<Foto>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT f.Id, Url, f.Tipo, InmuebleId, i.Direccion" +
                    " FROM Foto f INNER JOIN Inmueble i ON f.InmuebleId = i.Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Foto f = new Foto
                        {
                            Id = reader.GetInt32(0),
                            Url = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            InmuebleId = reader.GetInt32(3),
                            inmueble = new Inmueble
                            {
                                Id = reader.GetInt32(3),
                                Direccion = reader.GetString(4),
                            }
                        };
                        res.Add(f);
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public IList<Foto> ObtenerTodosPorInmuebleId(int inmuebleId)
        {
            IList<Foto> res = new List<Foto>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT f.Id, Url, f.Tipo, InmuebleId, i.Direccion" +
                    " FROM Foto f INNER JOIN Inmueble i ON f.InmuebleId = i.Id" +
                    $" WHERE f.InmuebleId = @inmuebleId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@inmuebleId", SqlDbType.Int).Value = inmuebleId;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Foto f = new Foto
                        {
                            Id = reader.GetInt32(0),
                            Url = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            InmuebleId = reader.GetInt32(3),
                            inmueble = new Inmueble
                            {
                                Id = reader.GetInt32(3),
                                Direccion = reader.GetString(4),
                            }
                        };
                        res.Add(f);
                    }
                    connection.Close();
                }
            }
            return res;
        }
    }
}

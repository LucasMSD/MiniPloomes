using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using System.Data.SqlClient;

namespace MiniPloomes.Repository
{
    public class ClienteRepository
    {
        private readonly SqlConnection _connection;

        public ClienteRepository(SqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<List<Cliente>> FindAllByUsuario(int usuarioId)
        {
            string query = "select * from Cliente where UsuarioId = @UsuarioId";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.Add(new SqlParameter("@UsuarioId", usuarioId));

                await command.Connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    List<Cliente> clientes = new List<Cliente>();

                    while (reader.Read())
                    {
                        clientes.Add(new Cliente()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = Convert.ToString(reader["Nome"]),
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            Created = Convert.ToDateTime(reader["Created"])
                        });
                    }

                    return clientes;
                }
            }
        }

        public async Task<Cliente?> FindById(int usuarioId, int clienteId)
        {
            string query = "select * from Cliente where UsuarioId = @UsuarioId and Id = @Id";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.Add(new SqlParameter("@UsuarioId", usuarioId));
                command.Parameters.Add(new SqlParameter("@Id", clienteId));

                await command.Connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Cliente cliente;

                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();
                    cliente =new Cliente()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = Convert.ToString(reader["Nome"]),
                        UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                        Created = Convert.ToDateTime(reader["Created"])
                    };

                    return cliente;
                }
            }
        }

        public async Task<Cliente> Insert(CreateClienteDto createCliente, int usuarioId)
        {
            string insert = @"
insert into Cliente
(Nome, UsuarioId, Created)
output inserted.* values
(@Nome, @UsuarioId, @Created)
";

            using (SqlCommand command = new SqlCommand(insert, _connection))
            {
                command.Parameters.Add(new SqlParameter("@Nome", createCliente.Nome));
                command.Parameters.Add(new SqlParameter("@UsuarioId", usuarioId));
                command.Parameters.Add(new SqlParameter("@Created", DateTime.Now));

                await command.Connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Cliente cliente;
                    reader.Read();

                    cliente = new Cliente()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = Convert.ToString(reader["Nome"]),
                        UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                        Created = Convert.ToDateTime(reader["Created"])
                    };

                    return cliente;
                }
            }
        }
    }
}

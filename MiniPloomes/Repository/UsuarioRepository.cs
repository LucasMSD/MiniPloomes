using MiniPloomes.Data.Dto;
using MiniPloomes.Data.Model;
using System.Data.SqlClient;

namespace MiniPloomes.Repository
{
    public class UsuarioRepository
    {
        private readonly SqlConnection _connection;

        public UsuarioRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Usuario>> FindAll()
        {
            string query = "select * from Usuario";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                await command.Connection.OpenAsync();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Usuario> usuarios = new List<Usuario>();

                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = Convert.ToString(reader["Nome"]),
                            Email = Convert.ToString(reader["UsuarioId"]),
                            Created = Convert.ToDateTime(reader["Created"])
                        });
                    }


                    return usuarios;
                }
            }
        }

        public async Task<Usuario?> FindById(int id)
        {
            string query = "select * from Usuario where Id = @Id";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                await command.Connection.OpenAsync();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Usuario usuario;

                    reader.Read();
                    usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = Convert.ToString(reader["Nome"]),
                        Email = Convert.ToString(reader["UsuarioId"]),
                        Created = Convert.ToDateTime(reader["Created"])
                    };

                    return usuario;
                }
            }
        }

        public async Task<Usuario> Insert(CreateUsuarioDto createUsuarioDto)
        {
            string insert = @"
insert into Usuario
(Nome, Email, Created)
output inserted.* values
(@Nome, @Email, @Created)
";

            using (SqlCommand command = new SqlCommand(insert, _connection))
            {
                command.Parameters.Add(new SqlParameter("@Nome", createUsuarioDto.Nome));
                command.Parameters.Add(new SqlParameter("@Email", createUsuarioDto.Email));
                command.Parameters.Add(new SqlParameter("@Created", DateTime.Now));

                await command.Connection.OpenAsync();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Usuario usuario;

                    reader.Read();
                    usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = Convert.ToString(reader["Nome"]),
                        Email = Convert.ToString(reader["UsuarioId"]),
                        Created = Convert.ToDateTime(reader["Created"])
                    };

                    return usuario;
                }
            }
        }
    }
}

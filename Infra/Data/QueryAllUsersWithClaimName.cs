using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        // Armazena a configuração para acesso ao banco de dados
        private readonly IConfiguration configuration;

        // Construtor que inicializa a configuração
        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Método para executar a consulta SQL
        public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
        {
            // Cria uma nova conexão SQL com a string de conexão
            var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);

            // Define a consulta SQL
            var query =
                @"select Email, ClaimValue as Name
                from AspNetUsers u inner
                join AspNetUserClaims c
                on u.id = c.UserId and ClaimType = 'Name'
                order by name
                OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            // Executa a consulta SQL de forma assíncrona e retorna os resultados
            return await db.QueryAsync<EmployeeResponse>(
                query,
                new { page, rows }
            );
        }
    }
}

using CadastrarProduto.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace CadastrarProduto.Repository
{
    public class ProdutoRepository(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("MySQLConnection");
        
        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("INSERT INTO tbProdutos (Nome, Descricao, Preco, Quantidade) VALUES (@Nome,@Descricao,@Preco,@Quantidade)", conexao);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        // Método para listar todos os clientes do banco de dados
        public IEnumerable<Produto> TodosOsProdutos()
        {
            // Cria uma nova lista para armazenar os objetos Produto
            List<Produto> Produtolist = new List<Produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'Produto'
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbProdutos", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto Produto e preenche suas propriedades com os valores da linha atual
                    Produtolist.Add(
                                new Produto
                                {
                                    Id = Convert.ToInt32(dr["id"]), // Converte o valor da coluna "id" para inteiro
                                    Nome = ((string)dr["nome"]), // Converte o valor da coluna "nome" para string
                                    Descricao = ((string)dr["descricao"]), // Converte o valor da coluna "descricao" para string
                                    Preco = Convert.ToDecimal(dr["preco"]),
                                    Quantidade = Convert.ToInt32(dr["quantidade"]),
                                });
                }
                // Retorna a lista de todos os Produtos
                return Produtolist;
            }
        }

        // Método para buscar um Produto específico pelo seu código (Id)
        public Produto ObterProduto(int Id)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'Produto' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbProduto where id=@id ", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@id", Id);

                // Cria um adaptador de dados (não utilizado diretamente para ExecuteReader)
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Declara um leitor de dados do MySQL
                MySqlDataReader dr;
                // Cria um novo objeto Produto para armazenar os resultados
                Produto Produto = new Produto();

                /* Executa o comando SQL e retorna um objeto MySqlDataReader para ler os resultados
                CommandBehavior.CloseConnection garante que a conexão seja fechada quando o DataReader for fechado*/

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // Lê os resultados linha por linha
                while (dr.Read())
                {
                    // Preenche as propriedades do objeto Produto com os valores da linha atual
                    Produto.Id = Convert.ToInt32(dr["id"]);//propriedade Id e convertendo para int
                    Produto.Nome = (string)(dr["nome"]); // propriedade Nome e passando string
                    Produto.Descricao = (string)(dr["descricao"]); //propriedade descricao e passando string
                    Produto.Preco = Convert.ToDecimal(dr["preco"]); //propriedade preco e passando int
                    Produto.Quantidade = Convert.ToInt32(dr["quantidade"]);
                }
                // Retorna o objeto Produto encontrado (ou um objeto com valores padrão se não encontrado)
                return Produto;
            }
        }
    }
}
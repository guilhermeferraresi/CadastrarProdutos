using CadastrarProduto.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace CadastrarProduto.Repository
{
    public class ProdutoRepository(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public Produto ObterProduto(string nome)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM tbProdutos WHERE Nome = @nome", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Produto produto = null;
                    if (dr.Read())
                    {
                        produto = new Produto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                            Quantidade = Convert.ToInt32(dr["Quantidade"]),
                        };
                    }
                    return produto;
                }
            }
        }
        public void AdicionarProduto(Produto produto)
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
    }
}
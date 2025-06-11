using CadastrarProduto.Models;
using MySql.Data.MySqlClient;
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Supermercado
{
    class ConectaBancoMercado
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=2bF83Jz@7;database=banco_supermercado");
        public string mensagem;

        public DataTable listaClientes()
        {
            MySqlCommand cmd = new MySqlCommand("listaClientes", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dados = new DataTable();
                da.Fill(dados);
                return dados;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro MySql:" + erro.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }
//-----------------------------------------------------------------------------------
    public bool insereCliente(Cliente c)
        {
            MySqlCommand cmd = new MySqlCommand("insereCliente", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cpfC", c.Cpf);
            cmd.Parameters.AddWithValue("nomeC", c.Nome);
            cmd.Parameters.AddWithValue("telC", c.Tel);
            cmd.Parameters.AddWithValue("ufC", c.Uf);
            cmd.Parameters.AddWithValue("cepC", c.Cep);
            cmd.Parameters.AddWithValue("cidadeC", c.Cidade);
            cmd.Parameters.AddWithValue("bairroC", c.Bairro);
            cmd.Parameters.AddWithValue("enderecoC", c.Endereco);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro MySql " + erro.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }
//-----------------------------------------------------------------------------------
    public bool deletaCliente(string cpf)
        {
            MySqlCommand cmd = new MySqlCommand("deletaCliente", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cpfremove", cpf);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro MySql " + erro.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }
//-----------------------------------------------------------------------------------
        public bool updateCliente(Cliente c, int idAltera)
        {
            MySqlCommand cmd = new MySqlCommand("updateCliente", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cpf", c.Cpf);
            cmd.Parameters.AddWithValue("nomeC", c.Nome);
            cmd.Parameters.AddWithValue("telC", c.Tel);
            cmd.Parameters.AddWithValue("ufC", c.Uf);
            cmd.Parameters.AddWithValue("cepC", c.Cep);
            cmd.Parameters.AddWithValue("cidadeC", c.Cidade);
            cmd.Parameters.AddWithValue("bairroC", c.Bairro);
            cmd.Parameters.AddWithValue("enderecoC", c.Endereco);
            cmd.Parameters.AddWithValue("id", idAltera);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro MySql " + erro.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}

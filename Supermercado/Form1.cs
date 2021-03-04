using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Supermercado
{
    public partial class sistema : Form
    {
        public sistema()
        {
            InitializeComponent();
        }

       

        private void fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCliente_Click(object sender, EventArgs e)
        {
            marcador.Height = btnAddCliente.Height;
            marcador.Top = btnAddCliente.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        } 

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

//----------------------------------------------------------------------------------------------------

        void listaDGclientes() 
        {
            ConectaBancoMercado con = new ConectaBancoMercado();
            dgCliente.DataSource = con.listaClientes();
        }

        private void sistema_Load(object sender, EventArgs e)
        {
            listaDGclientes();
        }

        private void salvaCadastro_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            c.Cpf = txtCpf.Text;
            c.Nome = txtNome.Text;
            c.Tel = txtTel.Text;
            c.Uf = txtUf.Text;
            c.Cep = txtcep.Text;
            c.Cidade = txtCid.Text;
            c.Bairro = txtBairro.Text;
            c.Endereco = txtEndereco.Text;
            ConectaBancoMercado con = new ConectaBancoMercado();
            bool res = con.insereCliente(c);
            if (res == true)
                MessageBox.Show("Inserido com sucesso :)");
            else
                MessageBox.Show("Erro: " + con.mensagem);
            listaDGclientes();
            limpaForm();
        }
        void limpaForm()
        {
            txtCpf.Clear();
            txtNome.Clear();
            txtTel.Clear();
            txtUf.Clear();
            txtcep.Clear();
            txtCid.Clear();
            txtBairro.Clear();
            txtEndereco.Clear();
            txtNome.Focus();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            (dgCliente.DataSource as DataTable).DefaultView.RowFilter = string.Format("cpfCliente like '%{0}%'", txtBusca.Text);
        }

        private void btnConfirmaRemocao_Click(object sender, EventArgs e)
        {
            int linha = dgCliente.CurrentRow.Index;
            string cpf = dgCliente.Rows[linha].Cells["cpfCliente"].Value.ToString();
            DialogResult resp = MessageBox.Show("Tem certeza que deseja excluir o cliente?", "Remover", MessageBoxButtons.YesNo);
            if (resp == DialogResult.Yes)
            {
                ConectaBancoMercado con = new ConectaBancoMercado();
                bool retorno = con.deletaCliente(cpf);
                if (retorno)
                    MessageBox.Show("Cliente excluído com sucesso :)");
                else
                    MessageBox.Show("Erro:", con.mensagem);

                listaDGclientes();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int linha = dgCliente.CurrentRow.Index;
            string cpf = dgCliente.Rows[linha].Cells["cpfCliente"].Value.ToString();
            txtAlteraCpf.Text = dgCliente.Rows[linha].Cells["cpfCliente"].Value.ToString();
            txtAlteraNome.Text = dgCliente.Rows[linha].Cells["nomeCliente"].Value.ToString();
            txtAlteraTel.Text = dgCliente.Rows[linha].Cells["telCliente"].Value.ToString();
            txtAlteraUf.Text = dgCliente.Rows[linha].Cells["ufClie"].Value.ToString();
            txtAlteraCep.Text = dgCliente.Rows[linha].Cells["cepClie"].Value.ToString();
            txtAlteraCidade.Text = dgCliente.Rows[linha].Cells["cidadeClie"].Value.ToString();
            txtAlteraBairro.Text = dgCliente.Rows[linha].Cells["bairroClie"].Value.ToString();
            txtAlteraEdereco.Text = dgCliente.Rows[linha].Cells["enderecoClie"].Value.ToString();
            tabControl1.SelectedTab = tabPage3;
        }

        private void btnAltera_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();

            c.Cpf = txtAlteraCpf.Text;
            c.Nome = txtAlteraNome.Text;
            c.Tel = txtAlteraTel.Text;
            c.Uf = txtAlteraUf.Text;
            c.Cep = txtAlteraCep.Text;
            c.Cidade = txtAlteraCidade.Text;
            c.Bairro = txtAlteraBairro.Text;
            c.Endereco = txtAlteraEdereco.Text;
            ConectaBancoMercado con = new ConectaBancoMercado();
            bool ret = con.updateCliente(c);
            if (ret == true)
                MessageBox.Show("Alterado com sucesso :)");
            else
                MessageBox.Show("Erro:", con.mensagem);
            listaDGclientes();
            tabControl1.SelectedTab = tabPage2;
        }
    }
}

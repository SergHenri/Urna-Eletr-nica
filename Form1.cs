using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Urna_Eletrônica.Form1;

namespace Urna_Eletrônica
{
    public partial class Form1 : Form
    {

        // Lista de candidatos
        private List<Candidato> candidatos = new List<Candidato>
        {
            new Candidato("Candidato 1", "Partido A", 22, "C:\\Users\\shlim\\Documents\\Documentos\\Programação Visual\\Urna eletrônica\\Urna Eletrônica\\Urna Eletrônica\\CandidatoOne.jpg"),
            new Candidato("Candidato 2", "Partido B", 13, "C:\\Users\\shlim\\Documents\\Documentos\\Programação Visual\\Urna eletrônica\\Urna Eletrônica\\Urna Eletrônica\\CandidatoTwo.jpg"),
            new Candidato("Candidato 3", "Partido C", 76, "C:\\Users\\shlim\\Documents\\Documentos\\Programação Visual\\Urna eletrônica\\Urna Eletrônica\\Urna Eletrônica\\CandidatoTre.jpg")
        };

        //Armazenar votos
        
        private List<int> votosConfirmados = new List<int>();


        



        // Classe candidato
        public class Candidato
        {
            public string Nome { get; set; }
            public string Partido { get; set; }
            public int Numero { get; set; }
            public string Foto { get; set; }
            public Candidato(string nome, string partido, int numero, string foto)
            {
                Nome = nome;
                Partido = partido;
                Numero = numero;
                Foto = foto;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void bt0_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "0";
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "1";
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "2";
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "3";
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "4";
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "5";
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "6";
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "7";
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "8";
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text += "9";
        }

        private void btcorrigir_Click(object sender, EventArgs e)
        {
            if (textBoxNumber.Text.Length > 0)
            {
                textBoxNumber.Text = textBoxNumber.Text.Substring(0, textBoxNumber.Text.Length - 1);
                textBoxNumber.Clear();
                labelNomeCandidatoOri.Text = "";
                labelNumeroCandidatoOri.Text = "";
                labelPartidoOri.Text = "";
                pictureCandidato1.Text = "";
                pictureCandidato1.Image = null;
            }
        }

        private void btpesquisarcandidato_Click(object sender, EventArgs e)
        {
            //Obter o numero do candidato digitado

            string numeroCandidato = textBoxNumber.Text;
            // Verifica se o número do candidato é válido
            if (int.TryParse(numeroCandidato, out int numero))
            {
                // Procura o candidato na lista
                Candidato candidato = candidatos.FirstOrDefault(c => c.Numero == numero);
                if (candidato != null)
                {
                    // Exibe os detalhes do candidato
                    labelNomeCandidatoOri.Text = candidato.Nome;
                    labelNumeroCandidatoOri.Text =  candidato.Numero.ToString();
                    labelPartidoOri.Text = candidato.Partido;
                    pictureCandidato1.Image = Image.FromFile(candidato.Foto);
                }
                else
                {
                    MessageBox.Show("Candidato não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Número inválido.");
            }
        }

        private void btconfirmar_Click(object sender, EventArgs e)
        {

            if (textBoxNumber.Text.Length > 0)
            {
                // Adiciona o voto à lista de votos confirmados
                if (int.TryParse(textBoxNumber.Text, out int numeroCandidato))
                {
                    votosConfirmados.Add(numeroCandidato);
                    MessageBox.Show("Voto confirmado!");
                    textBoxNumber.Clear();
                    labelNomeCandidatoOri.Text = "";
                    labelNumeroCandidatoOri.Text = "";
                    labelPartidoOri.Text = "";
                    pictureCandidato1.Text = "";
                    pictureCandidato1.Image = null;

                }
                else
                {
                    MessageBox.Show("Número inválido.");
                }
            }
            else
            {
                MessageBox.Show("Nenhum número digitado.");
            }
        }

        private void btLimparlista_Click(object sender, EventArgs e)
        {
            votosConfirmados.Clear();

            MessageBox.Show("Lista de votos limpa com sucesso!");
        }

        private void btencerrarvotacao_Click(object sender, EventArgs e)
        {
            var contagemVotos = votosConfirmados.GroupBy(voto => voto)
                                         .Select(grupo => new { Candidato = grupo.Key, TotalVotos = grupo.Count() })
                                         .OrderByDescending(resultado => resultado.TotalVotos)
                                         .ToList();

            // 2. Determinar o vencedor (o candidato com mais votos)
            var vencedor = contagemVotos.FirstOrDefault();

            // 3. Exibir os resultados
            if (vencedor != null)
            {
                // Encontrar o nome do candidato vencedor na lista de candidatos
                var candidatoVencedor = candidatos.FirstOrDefault(c => c.Numero == vencedor.Candidato);
                if (candidatoVencedor != null)
                {
                    labelParabens.Text = $"A votação foi encerrada!\nO vencedor é: {candidatoVencedor.Nome} com {vencedor.TotalVotos} votos.";

                    // Exibe os detalhes do candidato VENCEDOR
                    label9.Text = candidatoVencedor.Nome;
                    label8.Text = candidatoVencedor.Numero.ToString();
                    label7.Text = candidatoVencedor.Partido;
                    pictureCandidato2.Image = Image.FromFile(candidatoVencedor.Foto);
                }
                else
                {
                    MessageBox.Show($"A votação foi encerrada!\nVencedor (número {vencedor.Candidato}) com {vencedor.TotalVotos} votos.", "Resultado da Votação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Exibir a contagem de votos para todos os candidatos (opcional)
                string resultadosDetalhados = "Contagem de Votos:\n";
                foreach (var resultado in contagemVotos)
                {
                    var candidatoDetalhe = candidatos.FirstOrDefault(c => c.Numero == resultado.Candidato);
                    string nomeDetalhe = candidatoDetalhe != null ? candidatoDetalhe.Nome : $"Candidato {resultado.Candidato}";
                    resultadosDetalhados += $"{nomeDetalhe}: {resultado.TotalVotos} votos\n";
                }
                MessageBox.Show(resultadosDetalhados, "Detalhes da Votação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nenhum voto foi registrado.", "Resultado da Votação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // (Opcional) Limpar a lista de votos para uma nova votação
            // votosConfirmados.Clear();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}

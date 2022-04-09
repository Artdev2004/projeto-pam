using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnInformarDataNascimento_Clicked(object sender, EventArgs e)
        {
            try
            {
                // tentativa de execucao 
                string dataDigitada = await DisplayPromptAsync("Info", "Digite a data de nascimento", "OK");// f9 insere breakpoint
                DateTime dataConvertida;


                bool converteu = DateTime.TryParse(dataDigitada, new CultureInfo("pt-BR"), DateTimeStyles.None,  out dataConvertida);

                if (converteu == false)
                {
                    await DisplayAlert("Erro", "Formato da data está incorreto", "Ok");
                }
                else
                {
                    await DisplayAlert("Info", "O Formato está correto", "Ok");
                    int diasVividos = (int)DateTime.Now.Subtract(dataConvertida).TotalDays;

                    //int diasVividos2 = Convert.ToInt32(DateTime.Now.Subtract(dataConvertida).TotalDays);

                    //int diasVividos3 = int.Parse(DateTime.Now.Subtract(dataConvertida).TotalDays);
                    await DisplayAlert("Info", $"Voce ja viveu {diasVividos} dias. 0/", "Ok");
                    lblDataNascimento.Text = dataConvertida.Date.ToString("dd/MM/yyyy");
                }
                //dataConvertida = DateTime.Parse(dataDigitada); //Ex. 11/14/2020 erro de conversao
            }
            catch (Exception ex)//capturar o erro 
            {
                //await DisplayAlert("Ops", ex.Message + " - " + ex.InnerException, "Ok");
                //await DisplayAlert("Ops", $"{ ex.Message} - {ex.InnerException}", "Ok");
                string erro = string.Format("Erro {0} - Detalhes: {1}", ex.Message, ex.InnerException);
                await DisplayAlert("Ops", erro, "Ok");
            }
        }

        private async void btnOpcoes_Clicked(object sender, EventArgs e)
        {
            try
            {
                // if(lblDataNascimento.Text == String.Empty)
                // {

                //}

                if (string.IsNullOrWhiteSpace(lblDataNascimento.Text))
                    await DisplayAlert("Erro", "Voce não informou a data de nascimento", "Ok");
                else
                {
                    string resposta =
                        await DisplayActionSheet("Pergunta",
                                      "Selecione uma opção:",
                                      "Adicionar 10 dias",
                                      "Adicionar 10 meses",
                                      "Adicionar 1 ano");
                    DateTime dataNascimento = DateTime.Parse(lblDataNascimento.Text, new CultureInfo("ptr-BR"));

                    if(resposta  == "Adicionar 10 dias")
                    {
                        await DisplayAlert("Info", $"Data futura (10 dias): {dataNascimento.AddDays(10)}", "OK");
                    }
                    else if(resposta.Equals("Adicionar 10 meses"))
                    {
                        DateTime dataFutura = dataNascimento.AddMonths(10);
                        await DisplayAlert("Info", $"Data futura (10 meses): {dataNascimento}", "OK");
                    }
                    else if(resposta.Equals("Adicionar 1 ano"))
                    {
                        DateTime dataFutura = dataNascimento.AddYears(1);
                        await DisplayAlert("Info", $"Data futura (1 ano): {dataNascimento}", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Info", "Opção Inválida", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", $"{ ex.Message} - {ex.InnerException}", "Ok");
                throw;
            }
        }
    }
}

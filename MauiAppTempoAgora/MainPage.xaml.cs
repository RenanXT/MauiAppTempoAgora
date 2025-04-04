using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo ? t = await DataService.GetPrevisao(txt_cidade.Text);
                    if (t != null) {
                        string dados_prevsao = "";

                        dados_prevsao = $"Latitude: {t.lat} \n" +
                                        $"Longitude: {t.lon} \n" +
                                        $"Nascer do Sol: {t.sunrise} \n" +
                                        $"Por do Sol: {t.sunset} \n" +
                                        $"Tempo Máx: {t.temp_max} \n" +
                                        $"Tempo Min: {t.temp_min} \n" +
                                        $"Descrição: {t.description} \n" +
                                        $"Velocidade do vento: {t.speed} \n" +
                                        $"Visibilidade: {t.visibility} \n";

                        lbl_res.Text = dados_prevsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não Encontrada!";
                    }
                }
                else
                {
                    lbl_res.Text = "Preecha a cidade.";
                }
                
            }

            catch (Exception ex)
            {
                {
                    await DisplayAlert("Ops", ex.Message, "OK");
                }
            }
        }

    }
}

using RiotSimplify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RiotSimplify.Services;

namespace LoLFigures
{
	public partial class MainPage : ContentPage
	{
        public UserAccountService UserService { get; set; }

		public MainPage(RiotApiClient apiClient)
		{
			InitializeComponent();
            UserService = apiClient;
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var summonerIconPath = await UserService.GetSummonerIcon();

                SummonerIcon.Source = ImageSource.FromUri(new Uri(summonerIconPath));

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }           
        }
    }
}

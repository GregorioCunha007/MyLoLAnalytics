using RiotSimplify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RiotSimplify.Services;
using LoLFigures.ViewModels;

namespace LoLFigures
{
	public partial class MainPage : ContentPage
	{
        public UserAccountService UserService { get; set; }

		public MainPage(RiotApiClient apiClient)
		{
			InitializeComponent();
            BindingContext = new MainPageViewModel(apiClient);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MyViewModel.Init();
        }

        public MainPageViewModel MyViewModel
        {
            get { return BindingContext as MainPageViewModel; }
            set { BindingContext = value; }
        }
    }
}

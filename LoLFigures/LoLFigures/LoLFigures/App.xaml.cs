using RiotSimplify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LoLFigures
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            var riot = new RiotApiClient("RGAPI-ef0c7326-1658-4b76-a3e1-12d29f238cc8", "greggz");
            MainPage = new LoLFigures.MainPage(riot);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

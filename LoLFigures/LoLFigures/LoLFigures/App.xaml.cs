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
            var riot = new RiotApiClient("RGAPI-9df4f174-1ccc-4da6-ae88-0e167478c90b", "greggz");
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

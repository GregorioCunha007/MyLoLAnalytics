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
            await RiotApiUtils.FetchSummonerData();
            
            // Max bar height 500 
            int maxBarHeight = 500;
            double increaseSizeRate = (double) maxBarHeight / MyViewModel.TotalGamesPlayed;
            var barChart = this.FindByName<Grid>("Barchart");
            // Prepare animations
        
            Label [] counters = { Adc_Bar_Counter, Jungle_Bar_Counter, Mid_Bar_Counter, Support_Bar_Counter, Top_Bar_Counter };
            BoxView [] bars = { Adc_Bar, Jungle_Bar, Mid_Bar, Support_Bar, Top_Bar };

            await MyViewModel.Init(counters, bars, barChart);

            //var lanes = MyViewModel.MatchList.Matches.GroupBy(m => m.Position).Where(g => g.Key != "UNKNOWN").OrderBy(x => x.Key);
            //var totalLanes = lanes.Count(); 

            //for (int i = 0; i < totalLanes; ++i)
            //{
            //    var lane = lanes.ElementAt(i).ToList();
            //    var laneSize = lane.Count();

            //    animations.Add(new Animation(
            //         callback: (h) =>
            //         {
            //             bars[i].HeightRequest = Increase(h, increaseSizeRate);
            //         },
            //         start: 0,
            //         end: (laneSize * increaseSizeRate) - increaseSizeRate,
            //         easing: Easing.Linear
            //         )
            //         );

            //    animations.Add(new Animation(
            //        callback: (l) =>
            //        {
            //            counters[i].Text = IncreaseCounter((int)l).ToString();
            //        },
            //        start: 0,
            //        end: laneSize - 1,
            //        easing: Easing.Linear
            //        ));              
            //}

            //for (int i = 0; i < animations.Count; ++i)
            //{
            //    var currentAnimation = animations[i];
            //    try
            //    {

            //        if (i % 2 == 0)
            //        {
            //            currentAnimation.Commit(owner: barChart, name: "Grow" + i, rate: 30, length: 1500);
            //        }
            //        else
            //        {
            //            currentAnimation.Commit(owner: barChart, name: "Counter" + i, rate: 6, length: 1500);
            //        }

            //    }
            //    catch(Exception e)
            //    {

            //    }

            //}
        }

        public MainPageViewModel MyViewModel
        {
            get { return BindingContext as MainPageViewModel; }
            set { BindingContext = value; }
        }

        double Increase(double t, double rate)
        {
            return t + rate;
        }

        int IncreaseCounter(int l)
        {
            return l + 1;
        }
    }
}

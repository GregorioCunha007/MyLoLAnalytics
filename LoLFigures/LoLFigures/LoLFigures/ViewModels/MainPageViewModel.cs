using LoLFigures.Services;
using RiotSimplify;
using RiotSimplify.Clients;
using RiotSimplify.Dto;
using RiotSimplify.Publishers;
using RiotSimplify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoLFigures.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public int TotalGamesPlayed { get; set; }

        public MatchlistDto MatchList { get; set; }

        private Xamarin.Forms.ImageSource _summonerIcon;
        public Xamarin.Forms.ImageSource SummonerIcon
        {
            get { return _summonerIcon; }
            set { SetValue(ref _summonerIcon, value); }
        }

        private UserAccountService AccountService { get; set; }
        public MatchService MatchService { get; set; }

        public MatchSubscriber MatchSubscriber { get; set; }

        public MainPageViewModel(RiotApiClient riotApiClient)
        {
            MatchSubscriber = new MatchSubscriber();
            AccountService = riotApiClient;
            MatchService = riotApiClient;
        }

        public async Task<bool> Init(Label[] counters, BoxView[] bars, Grid barChart)
        {           
            try
            {
                // Icon 
                var currentIconPath = await AccountService.GetSummonerIcon();
                string fileName = currentIconPath.Substring(currentIconPath.LastIndexOf('/') + 1);
                Xamarin.Forms.ImageSource cachedIcon = ImageService.GetFromDisk(fileName);  

                if (cachedIcon != null)
                {
                    SummonerIcon = cachedIcon;
                }
                else
                {
                    var downloadedIcon = await ImageService.DownloadImage(currentIconPath);
                    ImageService.SaveToDisk(fileName, downloadedIcon);
                    SummonerIcon = ImageService.GetFromDisk(fileName);
                }

                MatchList = await MatchService.GetTotalMatches(10, "SOLODUO");
                TotalGamesPlayed = MatchList.totalGames;

                InitGraph(counters, bars, barChart);

                return true;
            }
            catch (Exception e)
            {
                // TBD
                return false;
            }         
        }

        private void InitGraph(Label [] counters, BoxView [] bars, Grid barChart)
        {
            List<Animation> animations = new List<Animation>();
            var lanes = MatchList.Matches.GroupBy(m => m.Position).Where(g => g.Key != "UNKNOWN").OrderBy(x => x.Key);
            var totalLanes = lanes.Count();
            double increaseSizeRate = (double)500 / TotalGamesPlayed;

            for (int i = 0; i < totalLanes; ++i)
            {
                var lane = lanes.ElementAt(i).ToList();
                var laneSize = lane.Count();
                int index = i;
                animations.Add(new Animation(
                     callback: (h) =>
                     {
                         bars[index].HeightRequest = Increase(h, increaseSizeRate);
                     },
                     start: 0,
                     end: (laneSize * increaseSizeRate) - increaseSizeRate,
                     easing: Easing.Linear
                     )
                     );

                animations.Add(new Animation(
                    callback: (l) =>
                    {
                        counters[index].Text = IncreaseCounter((int)l).ToString();
                    },
                    start: 0,
                    end: laneSize - 1,
                    easing: Easing.Linear
                    ));
            }



            for (int i = 0; i < animations.Count; ++i)
            {
                var currentAnimation = animations[i];
                try
                {

                    if (i % 2 == 0)
                    {
                        currentAnimation.Commit(owner: barChart, name: "Grow" + i, rate: 30, length: 1500);
                    }
                    else
                    {
                        currentAnimation.Commit(owner: barChart, name: "Counter" + i, rate: 6, length: 1500);
                    }

                }
                catch (Exception e)
                {

                }

            }
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

    public class MatchSubscriber
    {
        public void ReceiveMatches(object source, MatchesEventArgs matchesEventArgs)
        {
            Console.WriteLine(matchesEventArgs.Matches.Count);
        }
    }
}

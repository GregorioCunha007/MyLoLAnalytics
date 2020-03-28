using LoLFigures.Services;
using RiotSimplify.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoLFigures.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private Xamarin.Forms.ImageSource _summonerIcon;
        public Xamarin.Forms.ImageSource SummonerIcon
        {
            get { return _summonerIcon; }
            set { SetValue(ref _summonerIcon, value); }
        }

        private UserAccountService AccountService { get; set; }

        public MainPageViewModel(UserAccountService accountService)
        {
            AccountService = accountService;
        }

        public async Task<bool> Init()
        {
            try
            {
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
                
                return true;
            }
            catch (Exception e)
            {
                // TBD
                return false;
            }         
        }
    }
}

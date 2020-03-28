using RiotSimplify.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoLFigures.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public string SummonerIconPath { get; set; }

        private UserAccountService AccountService { get; set; }

        public MainPageViewModel(UserAccountService accountService)
        {
            AccountService = accountService;
        }

        public async Task<bool> Init()
        {
            try
            {
                SummonerIconPath = await AccountService.GetSummonerIcon();
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

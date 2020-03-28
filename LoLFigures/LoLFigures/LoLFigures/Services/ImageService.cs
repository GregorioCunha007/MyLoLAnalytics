using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoLFigures.Services
{
    static class ImageService
    {
        static readonly HttpClient _client = new HttpClient();
        private static ISettings AppSettings => CrossSettings.Current;

        public static Task<byte[]> DownloadImage(string imageUrl)
        {
            if (!imageUrl.Trim().StartsWith("https", StringComparison.OrdinalIgnoreCase))
                throw new Exception("iOS and Android Require Https");

            return _client.GetByteArrayAsync(imageUrl);
        }

        public static void SaveToDisk(string imageFileName, byte[] imageAsBase64String)
        {
            AppSettings.AddOrUpdateValue(imageFileName, Convert.ToBase64String(imageAsBase64String));
        }

        public static Xamarin.Forms.ImageSource GetFromDisk(string imageFileName)
        {
            var imageAsBase64String = AppSettings.GetValueOrDefault(imageFileName, string.Empty);

            if (string.IsNullOrEmpty(imageAsBase64String))
            {
                return null;
            }

            return Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(imageAsBase64String)));
        }
    }
}

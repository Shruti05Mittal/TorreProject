using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Torre.Pages
{
    public class IndexModel : PageModel
    {
        static HttpClient client = new HttpClient();
        private string getUserDetailsByUserNamePath = "https://bio.torre.co/api/bios/shruti05mittal";
        public Root UserInfo = null;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            UserInfo = new Root();
        }

        static Root GetRoot(string path)
        {
            Root root = null;
            HttpResponseMessage response =  client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
              //  root = response.Content.ReadAsStringAsync<Root>();
            }
            return root;
        }
        public void OnGet()
        {
            //var root = GetRoot(getUserDetailsByUserNamePath);
            UserInfo = GetData().Result;
        }
        public async Task<Root> GetData()
        {
            Root userinformation = new Root();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getUserDetailsByUserNamePath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userinformation = JsonConvert.DeserializeObject<Root>(apiResponse);
                }
            }
            return userinformation;
        }
    }
}

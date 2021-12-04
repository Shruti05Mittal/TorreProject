using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Torre.Pages
{
    public class SkillDetail: PageModel
    {
        public Strength strength { get; set; }
        public string ErrorMessage { get; set; }
        private string getUserDetailsByUserNamePath = "https://bio.torre.co/api/bios/shruti05mittal";
        public void OnGet(string skillname)
        {
            
            if (string.IsNullOrWhiteSpace(skillname))
                ErrorMessage = "Skill name can't be blank";
            var userdata = GetUserData();
            if (userdata == null)
                ErrorMessage = "No user found";
            if (userdata.Result == null)
                ErrorMessage = "No user found";
            if (userdata.Result.strengths == null || userdata.Result.strengths.Count == 0)
                ErrorMessage = "No skills found";
            var selectedSkill = userdata.Result.strengths.Where(s => s.name.ToLower() == skillname.ToLower());
            if (selectedSkill == null || selectedSkill.Count() == 0)
                ErrorMessage = "Selected skill not found";
            if (selectedSkill.Count() > 1)
                ErrorMessage = "Selected skill exists multiple times";
            if (string.IsNullOrWhiteSpace(ErrorMessage))
                strength = selectedSkill.FirstOrDefault();
        }
        public async Task<Root> GetUserData()
        {
            Root userinformation = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getUserDetailsByUserNamePath))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        userinformation = JsonConvert.DeserializeObject<Root>(apiResponse);
                    }
                }
            }
            return userinformation;
        }
    }
}

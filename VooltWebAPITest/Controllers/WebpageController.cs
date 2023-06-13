using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace VooltWebAPITest.Controllers
{
	[ApiController]
	[Route("api/webpage")]
	public class WebpageController : ControllerBase
	{
		private string DataFileExt = ".json";

		[HttpPost]
		public IActionResult CreateWebpage(Webpage webpage, string key)
		{
			//Check key for spaces
			if (key.Contains(' '))
			{
				return BadRequest("Key values cannot contain spaces.");
			}

			// Create path to json
			DataFileExt = key + DataFileExt;

			// Load data from the JSON file if exsists else create new page
			var webpageData = WebpageData();

			//Create HeaderBlock
			webpageData.HeaderBlock = new WebHeaderBlock();

			//Create WebHero
			webpageData.HeroBlocks = new List<WebHeroBlock>();

			//Create WebService
			webpageData.ServiceBlocks = new List<WebServiceBlock>();

			// Save the updated data to the JSON file
			SaveData(webpageData);

			//Serialize and return content
			var json = JsonSerializer.Serialize(webpageData);
			return Content(json.ToString(), "application/json");

		}

		private Webpage WebpageData()
		{
			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				return JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();
			}

			return new Webpage();
		}

		private void SaveData(Webpage data)
		{
			var json = JsonSerializer.Serialize(data);
			System.IO.File.WriteAllText(DataFileExt, json);
		}

	}
}
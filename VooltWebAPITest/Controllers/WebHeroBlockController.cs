using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace VooltWebAPITest.Controllers
{
	[ApiController]
	[Route("api/heroblock")]
	public class WebHeroBlockController : ControllerBase
	{
		private string DataFileExt = ".json";

		[HttpPost]
		public IActionResult CreateHeroBlock(WebHeroBlock id, string key)
		{
			//Check key for spaces
			if (key.Contains(' '))
			{
				return BadRequest("Key values cannot contain spaces.");
			}

			// Create path to json
			DataFileExt = key + DataFileExt;

			// Load data from the JSON file
			var webpageData = LoadData();

			// Return not found if not found
			if (webpageData == null)
			{
				return NotFound();
			}

			// Save the data to the JSON file
			SaveData(webpageData, "DefaultHero");

			return Ok();
		}

		[HttpPut]
		public IActionResult UpdateHeroBlock(List<WebHeroBlock> updatedHero, string key, string sectionID)
		{
			//Check key for spaces
			if (key.Contains(' '))
			{
				return BadRequest("Key values cannot contain spaces.");
			}

			// Create path to json
			DataFileExt = key + DataFileExt;

			// Load existing data from the JSON file
			var updateData = LoadData();

			// Return not found if not found
			if (updateData == null)
			{
				return NotFound();
			}

			// Update the Header Block data
			updateData = updatedHero;

			// Save the updated data to the JSON file
			SaveData(updateData, sectionID);

			return Ok();
		}

		[HttpDelete]
		public IActionResult DeleteHeroBlock(string key, string sectionID)
		{
			//Check key for spaces
			if (key.Contains(' '))
			{
				return BadRequest("Key values cannot contain spaces.");
			}

			// Create path to json
			DataFileExt = key + DataFileExt;

			// Load existing data from the JSON file
			var heroToDelete = LoadData();

			// Return not found if not found
			if (heroToDelete == null)
			{
				return NotFound();
			}

			// Delete the data in the JSON file
			DeleteData(sectionID);

			return Ok();
		}

		private List<WebHeroBlock>? LoadData()
		{
			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				if (jsonReturn.HeroBlocks == null)
				{
					return new List<WebHeroBlock>();
				}

				return jsonReturn.HeroBlocks;
			}

			return null;
		}
		private void SaveData(List<WebHeroBlock> data, string sectionID)
		{

			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				jsonReturn.HeroBlocks = data;

				var jsonUpdated = JsonSerializer.Serialize(jsonReturn);

				System.IO.File.WriteAllText(DataFileExt, jsonUpdated);

			}

		}
		private void DeleteData(string sectionID)
		{

			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				jsonReturn.HeroBlocks = null;

				var jsonUpdated = JsonSerializer.Serialize(jsonReturn);

				System.IO.File.WriteAllText(DataFileExt, jsonUpdated);

			}

		}


	}
}
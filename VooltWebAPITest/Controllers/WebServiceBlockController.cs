using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace VooltWebAPITest.Controllers
{
	[ApiController]
	[Route("api/serviceblock")]
	public class WebServiceBlockController : ControllerBase
	{
		private string DataFileExt = ".json";

		[HttpPost]
		public IActionResult CreateServiceBlock(WebServiceBlock id, string key)
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
		public IActionResult UpdateServiceBlock(List<WebServiceBlock> updatedService, string key, string sectionID)
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
			updateData = updatedService;

			// Save the updated data to the JSON file
			SaveData(updateData, sectionID);

			return Ok();
		}

		[HttpDelete]
		public IActionResult DeleteServiceBlock(string key, string sectionID)
		{
			//Check key for spaces
			if (key.Contains(' '))
			{
				return BadRequest("Key values cannot contain spaces.");
			}

			// Create path to json
			DataFileExt = key + DataFileExt;

			// Load existing data from the JSON file
			var serviceToDelete = LoadData();

			// Return not found if not found
			if (serviceToDelete == null)
			{
				return NotFound();
			}

			// Delete the data in the JSON file
			DeleteData(sectionID);

			return Ok();
		}

		private List<WebServiceBlock>? LoadData()
		{
			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				if (jsonReturn.ServiceBlocks == null)
				{
					return new List<WebServiceBlock>();
				}

				return jsonReturn.ServiceBlocks;
			}

			return null;
		}
		private void SaveData(List<WebServiceBlock> data, string sectionID)
		{

			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				jsonReturn.ServiceBlocks = data;

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

				jsonReturn.ServiceBlocks = null;

				var jsonUpdated = JsonSerializer.Serialize(jsonReturn);

				System.IO.File.WriteAllText(DataFileExt, jsonUpdated);

			}

		}


	}
}
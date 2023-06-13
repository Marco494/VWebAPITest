using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace VooltWebAPITest.Controllers
{
	[ApiController]
	[Route("api/headerblock")]
	public class WebHeaderBlockController : ControllerBase
	{
		private string DataFileExt = ".json";

		[HttpPost]
		public IActionResult CreateHeaderBlock(WebHeaderBlock id, string key)
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
			SaveData(webpageData, "DefaultHeader");

			//Serialize and return content
			var json = JsonSerializer.Serialize(webpageData);
			return Content(json.ToString(), "application/json");
		}

		[HttpPut]
		public IActionResult UpdateHeaderBlock(WebHeaderBlock updatedHeader, string key, string sectionID)
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
			updateData = updatedHeader;

			// Save the updated data to the JSON file
			SaveData(updateData, sectionID);

			//Serialize and return content
			var json = JsonSerializer.Serialize(updateData);
			return Content(json.ToString(), "application/json");
		}

		[HttpDelete]
		public IActionResult DeleteHeaderBlock(string key, string sectionID)
		{
			//Check key for spaces
			if (key.Contains(' '))
			{
				return BadRequest("Key values cannot contain spaces.");
			}

			// Create path to json
			DataFileExt = key + DataFileExt;

			// Load existing data from the JSON file
			var headerToDelete = LoadData();

			// Return not found if not found
			if (headerToDelete == null)
			{
				return NotFound();
			}

			// Delete the data to the JSON file
			if (DeleteData(sectionID))
			{
				return Ok();
			}
			else
			{
				return NotFound();
			};

		}

		private WebHeaderBlock? LoadData()
		{
			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				if (jsonReturn.HeaderBlock == null)
				{
					return new WebHeaderBlock();
				}

				return jsonReturn.HeaderBlock;
			}

			return null;
		}
		private bool SaveData(WebHeaderBlock data, string sectionID)
		{

			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				if (jsonReturn.HeaderBlock != null)
				{
					if (jsonReturn.HeaderBlock.ID == sectionID)
					{
						data.ID = sectionID;
						jsonReturn.HeaderBlock = data;

						var jsonUpdated = JsonSerializer.Serialize(jsonReturn);
						System.IO.File.WriteAllText(DataFileExt, jsonUpdated);

						return true;
					}
					return false;
				}
				return false;
			}
			return false;

		}
		private bool DeleteData(string sectionID)
		{

			if (System.IO.File.Exists(DataFileExt))
			{
				var json = System.IO.File.ReadAllText(DataFileExt);
				var jsonReturn = JsonSerializer.Deserialize<Webpage>(json) ?? new Webpage();

				if (jsonReturn.HeaderBlock != null){
					if (jsonReturn.HeaderBlock.ID == sectionID) {
						jsonReturn.HeaderBlock = null;

						var jsonUpdated = JsonSerializer.Serialize(jsonReturn);
						System.IO.File.WriteAllText(DataFileExt, jsonUpdated);

						return true;
					}
					return false;
				}
				return false;
			}
			return false;

		}

		
	}
}
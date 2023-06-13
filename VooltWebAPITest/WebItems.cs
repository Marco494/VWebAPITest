namespace VooltWebAPITest
{
	public class MenuItem
	{
		public string Title { get; set; }
		public string Link { get; set; }
		public List<MenuItem>? SubMenuItems { get; set; }

		//Default Values
		public MenuItem()
		{
			Title = "Default Menu Item";
			Link = string.Empty;
			SubMenuItems = null;
		}
	}
	public class ServiceItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageBase64 { get; set; }
		public List<ButtonItem> CTAButton { get; set; }

		//Default Values
		public ServiceItem()
		{
			Name = "Default Service";
			Description = "Default Description";
			ImageBase64 = string.Empty;
			CTAButton = new List<ButtonItem>();
		}
	}

	public class ButtonItem
	{
		public string Text { get; set; }
		public bool Status { get; set; }
		public string? IconBase64 { get; set; }
		public string? Event { get; set; }

		//Default Values
		public ButtonItem()
		{
			Text = "Default Button";
			Status = false;
			IconBase64 = string.Empty;
			Event = string.Empty;
		}
	}

}
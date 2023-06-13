namespace VooltWebAPITest
{
	public class WebHeroBlock
	{
		public string ID { get; set; }
		public int BlockOrder { get; set; }
		public string HeadlineText { get; set; }
		public string Description { get; set; }
		public List<ButtonItem> CTAButton { get; set; }
		public string? HeroImageBase64 { get; set; }
		public string? ImageAlignment { get; set; }
		public string? ContentAlignment { get; set; }

		//Default Values
		public WebHeroBlock()
		{
			ID = "HeroBlock1";
			BlockOrder = 1;
			HeadlineText = "Default Headline";
			Description = "Default Description";
			CTAButton = new List<ButtonItem>();
			HeroImageBase64 = string.Empty;
			ImageAlignment = string.Empty;
			ContentAlignment = string.Empty;
		}

	}
}
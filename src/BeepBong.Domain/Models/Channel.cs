using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
	public class Channel
	{
		public Channel()
		{
			Programmes = new List<Programme>();
		}

		public Guid ChannelId { get; set; }
		public string Name { get; set; }
		public string Organisation { get; set; }
		//public IFormFile Logo { get; set; }

		public ICollection<Programme> Programmes { get; set; }
	}
}
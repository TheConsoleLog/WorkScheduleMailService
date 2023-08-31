using System;
using MongoDB.Entities;

namespace AutoRoasterEmailWorker.Entities
{
	public class DayEntity : Entity
	{
		[Field("real")]
		public string Real { get; set; } = string.Empty;
		[Field("display")]
		public string Display { get; set; } = string.Empty;
		[Field("canWork")]
		public bool? CanWork { get; set; }
	}
}


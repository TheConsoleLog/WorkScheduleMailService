using System;
using MongoDB.Entities;

namespace AutoRoasterEmailWorker.Entities
{
	public class TimetableEntity : Entity
	{
		[Field("cw")]
		public int? Cw { get; set; }
		[Field("startDate")]
		public string StartDate { get; set; } = string.Empty;
        [Field("endDate")]
        public string EndDate { get; set; } = string.Empty;
		[Field("remark")]
		public string Remark { get; set; } = string.Empty;
		[Field("days")]
		public Many<DayEntity> Days { get; set; } = null!;
    }
}


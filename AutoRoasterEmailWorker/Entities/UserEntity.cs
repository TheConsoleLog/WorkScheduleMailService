using System;
using MongoDB.Entities;

namespace AutoRoasterEmailWorker.Entities
{
	public class UserEntity : Entity
	{
		[Field("email")]
		public string Email { get; set; } = string.Empty;
		[Field("password")]
		public string Password { get; set; } = string.Empty;
		[Field("timetables")]
		public Many<TimetableEntity> TimetableEntities { get; set; } = null!;
	}
}


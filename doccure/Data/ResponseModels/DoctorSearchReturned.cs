﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace doccure.Data.ResponseModels
{
	[NotMapped]
	public class DoctorSearchReturned
	{
		public string Id { get; set; }
		public int ClinicID { get; set; }
		public string FULLNAME { get; set; }
		public string Name { get; set; }
		public string SPECALITYNAME { get; set; }
		public float Price { get; set; }
		public string Address { get; set; }
		public string? image { get; set; }
		public string? PROFILEIMAGE { get; set; }
		public string Services { get; set; }
		public string Specialization { get; set; }

	}
}

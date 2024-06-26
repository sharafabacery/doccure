﻿namespace doccure.Data.Models
{
	public enum PaymentMethod
	{
		CreditCard = 1,
		Paypal = 2,
		NotPayed=3
	}
	public enum BookingStatus
	{
		Pending=1,
		Cancel=2,
		Accepted=3,
		Complete=4
	}
	public enum PatientType
	{
		NewPatient=1,
		OldPatient=2
	}
	public class Booking
	{
		public int Id { get; set; }
		public DateTime bookingDate { get; set; }
		public double price { get; set; }
		public double videocall { get; set; }
		public double total { get; set; }
		public PaymentMethod payment { get; set; }
		public int scheduletimingId { get; set; }
		public string? patientId { get; set; }
		public Applicationuser?patient { get; set; } 
		public string? doctorId { get; set; }
		public DateTime? FollowUp { get; set; }
		public BookingStatus BookingStatus { get; set; }
		public PatientType PatientType { get; set; }
		public DateTime createdDate { get; set; }
		public Applicationuser? doctor { get; set; }

		public ScheduleTiming scheduleTiming { get; set; } = null;
		public ICollection<Prescription> Prescription { get; } = new List<Prescription>(); // Collection navigation containing dependents
		public MedicalRecord MedicalRecord { get; set; }
		public Review Review { get; set; }
		public ICollection<Billing> Billing { get; } = new List<Billing>(); // Collection navigation containing dependents


	}
}

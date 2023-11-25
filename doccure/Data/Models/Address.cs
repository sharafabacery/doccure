﻿namespace doccure.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string applicationuserId { get; set; }
        public Applicationuser applicationuser { get; set; } = null;
    }
}

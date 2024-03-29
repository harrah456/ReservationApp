﻿using System.ComponentModel.DataAnnotations;

namespace ReserveApp.Data
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int? TourId { get; set; }
        public Tour? Tour { get; set; }
        public required string CustomerId { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public RoomType? RoomType { get; set; } // Enum representing Single, Double, Family Suite
        public decimal TotalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public bool Status { get; set; } = true;
        public List<Payment> Payments { get; set; } = new List<Payment>();


    }

    public enum RoomType
    {
        Single,
        Double,
        FamilySuite
    }
}

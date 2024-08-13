using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace ReservationApp.Models;

public class Reservation
{
    public ObjectId Id { get; set; }

    public ObjectId RestaurantId { get; set; }

    public string? RestaurantName { get; set; }

    [Required(ErrorMessage = "The date and time required to make reservation")]
    [Display(Name = "Date")]
    public DateTime date { get; set; }
}
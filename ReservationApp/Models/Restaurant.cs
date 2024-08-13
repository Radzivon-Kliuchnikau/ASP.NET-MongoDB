using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace ReservationApp.Models;

[Collection("restaurants")]
public class Restaurant
{
    public ObjectId Id { get; set; }

    [Required(ErrorMessage = "You must provide a name")]
    [Display(Name = "Name")]
    public string? name { get; set; }

    [Required(ErrorMessage = "You must provide a cuisine")]
    [Display(Name = "Cuisine")]
    public string? cuisine { get; set; }

    [Required(ErrorMessage = "You must add a borough of the restaurant")]
    public string? borough { get; set; }
}
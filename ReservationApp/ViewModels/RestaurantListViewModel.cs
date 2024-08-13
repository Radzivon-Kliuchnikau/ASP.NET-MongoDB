using ReservationApp.Models;

namespace ReservationApp.ViewModels;

public class RestaurantListViewModel
{
    public IEnumerable<Restaurant>? Restaurants { get; set; }
}
using ReservationApp.Models;

namespace ReservationApp.ViewModels;

public class ReservationListViewModel
{
    public IEnumerable<Reservation>? Reservations { get; set; }
}
using MongoDB.Bson;
using ReservationApp.Models;

namespace ReservationApp.Services;

public interface IReservationService
{
    IEnumerable<Reservation> GetAllReservations();

    Reservation? GetReservationById(ObjectId id);

    void EditReservation(Reservation reservation);

    void AddReservation(Reservation reservation);

    void RemoveReservation(Reservation reservation);
}
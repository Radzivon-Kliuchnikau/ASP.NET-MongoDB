using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using ReservationApp.Models;

namespace ReservationApp.Services;

public class ReservationService(ReservationDbContext context) : IReservationService
{
    public IEnumerable<Reservation> GetAllReservations()
    {
        return context.Reservations.OrderByDescending(r => r.Id).Take(25).AsNoTracking().AsEnumerable();
    }

    public Reservation? GetReservationById(ObjectId id)
    {
        return context.Reservations.AsNoTracking().FirstOrDefault(r => r.Id == id);
    }

    public void EditReservation(Reservation reservation)
    {
        var reservationToEdit = context.Reservations.FirstOrDefault(r => r.Id == reservation.Id);

        if (reservationToEdit != null)
        {
            reservationToEdit.date = reservation.date;

            context.Reservations.Update(reservationToEdit);
            
            context.ChangeTracker.DetectChanges();
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("There is no such reservation to edit");
        }
    }

    public void AddReservation(Reservation reservation)
    {
        var restaurantToReserve = context.Restaurants.FirstOrDefault(r => r.Id == reservation.RestaurantId);

        if (restaurantToReserve == null)
        {
            throw new ArgumentException("There is no such restaurant to reserve");
        }

        reservation.RestaurantName = restaurantToReserve.name;

        context.Reservations.Add(reservation);
        
        context.ChangeTracker.DetectChanges();
        Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        context.SaveChanges();
    }

    public void RemoveReservation(Reservation reservation)
    {
        var reservationToRemove = context.Reservations.FirstOrDefault(r => r.Id == reservation.Id);

        if (reservationToRemove != null)
        {
            context.Reservations.Remove(reservationToRemove);
            
            context.ChangeTracker.DetectChanges();
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("There is no such reservation to delete");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ReservationApp.Models;
using ReservationApp.Services;
using ReservationApp.ViewModels;

namespace ReservationApp.Controllers;

public class ReservationController(IReservationService reservationService, IRestaurantService restaurantService)
    : Controller
{
    public IActionResult Index()
    {
        ReservationListViewModel reservationListViewModel = new()
        {
            Reservations = reservationService.GetAllReservations()
        };

        return View(reservationListViewModel);
    }

    public IActionResult Add(ObjectId id)
    {
        var restaurantForReservation = restaurantService.GetRestaurantById(id);

        ReservationAddViewModel reservationAddViewModel = new()
        {
            Reservation = new Reservation()
        };

        reservationAddViewModel.Reservation.RestaurantName = restaurantForReservation.name;
        reservationAddViewModel.Reservation.RestaurantId = restaurantForReservation.Id;
        reservationAddViewModel.Reservation.date = DateTime.UtcNow;

        return View(reservationAddViewModel);
    }

    [HttpPost]
    public IActionResult Add(ReservationAddViewModel reservationAddViewModel)
    {
        Reservation newReservation = new()
        {
            // RestaurantName = reservationAddViewModel.Reservation.RestaurantName,
            date = reservationAddViewModel.Reservation.date,
            RestaurantId = reservationAddViewModel.Reservation.RestaurantId
        };

        reservationService.AddReservation(newReservation);

        return RedirectToAction("Index");
    }

    public IActionResult Edit(string id)
    {
        if (id == null || string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var reservationToEdit = reservationService.GetReservationById(new ObjectId(id));

        return View(reservationToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Reservation reservation)
    {
        try
        {
            var reservationToEdit = reservationService.GetReservationById(reservation.Id);

            if (reservationToEdit != null)
            {
                reservationService.EditReservation(reservation);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", $"Reservation to edit with {reservation.Id} id does not  exist");
            }
        }
        catch (Exception exception)
        {
            ModelState.AddModelError("",
                $"Updating of reservation failed, please try again. There is some error: {exception}");
        }

        return View(reservation);
    }

    public IActionResult Delete(string id)
    {
        if (id == null || string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var reservationToDelete = reservationService.GetReservationById(new ObjectId(id));

        return View(reservationToDelete);
    }

    [HttpPost]
    public IActionResult Delete(Reservation reservation)
    {
        if (reservation.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Deleting of reservation failed. Wrong Id";

            return View();
        }

        try
        {
            reservationService.RemoveReservation(reservation);
            TempData["ReservationDeleted"] = "Reservation Deleted";

            return RedirectToAction("Index");
        }
        catch (Exception exception)
        {
            ViewData["ErrorMessage"] = $"There is some error during reservation removal: {exception}";
        }

        var reservationToDelete = reservationService.GetReservationById(reservation.Id);

        return View(reservationToDelete);
    }
}
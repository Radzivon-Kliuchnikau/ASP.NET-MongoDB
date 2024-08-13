using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ReservationApp.Models;
using ReservationApp.Services;
using ReservationApp.ViewModels;

namespace ReservationApp.Controllers;

public class RestaurantController(IRestaurantService restaurantService) : Controller
{
    public IActionResult Index()
    {
        RestaurantListViewModel restaurantViewModel = new()
        {
            Restaurants = restaurantService.GetAllRestaurants()
        };

        return View(restaurantViewModel);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(RestaurantAddViewModel restaurantAddViewModel)
    {
        if (ModelState.IsValid)
        {
            Restaurant newRestaurant = new()
            {
                borough = restaurantAddViewModel.Restaurant.borough,
                cuisine = restaurantAddViewModel.Restaurant.cuisine,
                name = restaurantAddViewModel.Restaurant.name
            };
            
            restaurantService.AddRestaurant(newRestaurant);

            return RedirectToAction("Index");
        }

        return View(restaurantAddViewModel);
    }

    public IActionResult Edit(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
        {
            return NotFound();
        }

        var restaurantToEdit = restaurantService.GetRestaurantById(id);

        return View(restaurantToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Restaurant restaurant)
    {
        try
        {
            if (ModelState.IsValid)
            {
                restaurantService.EditRestaurant(restaurant);

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception exception)
        {
            ModelState.AddModelError("", $"There is an error during restaurant updating: {exception}");
        }

        return View(restaurant);
    }

    public IActionResult Delete(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
        {
            return NotFound();
        }

        var restaurantToDelete = restaurantService.GetRestaurantById(id);

        return View(restaurantToDelete);
    }

    [HttpPost]
    public IActionResult Delete(Restaurant restaurant)
    {
        if (restaurant.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Deleting the restaurant failed, wrong Id";

            return View();
        }

        try
        {
            restaurantService.RemoveRestaurant(restaurant);
            TempData["RestaurantDeleted"] = "Restaurant removed";

            return RedirectToAction("Index");
        }
        catch (Exception exception)
        {
            ViewData["ErrorMessage"] = $"There is some error during restaurant removal: {exception}";
        }

        var restaurantToDelete = restaurantService.GetRestaurantById(restaurant.Id);

        return View(restaurantToDelete);
    }
}
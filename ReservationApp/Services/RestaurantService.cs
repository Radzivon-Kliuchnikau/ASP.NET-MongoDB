using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using ReservationApp.Models;

namespace ReservationApp.Services;

public class RestaurantService(ReservationDbContext context) : IRestaurantService
{
    public IEnumerable<Restaurant> GetAllRestaurants()
    {
        return context.Restaurants.OrderByDescending(r => r.Id).Take(25).AsNoTracking().AsEnumerable();
    }

    public Restaurant? GetRestaurantById(ObjectId id)
    {
        return context.Restaurants.FirstOrDefault(r => r.Id == id);
    }

    public void AddRestaurant(Restaurant restaurant)
    {
        context.Restaurants.Add(restaurant);
        
        context.ChangeTracker.DetectChanges();
        Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        context.SaveChanges();
    }

    public void RemoveRestaurant(Restaurant restaurant)
    {
        var restaurantToRemove = context.Restaurants.Where(r => r.Id == restaurant.Id).FirstOrDefault();

        if (restaurantToRemove != null)
        {
            context.Restaurants.Remove(restaurantToRemove);
            
            context.ChangeTracker.DetectChanges();
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("There is no such restaurant to delete");
        }
    }

    public void EditRestaurant(Restaurant restaurant)
    {
        var restaurantToEdit = context.Restaurants.FirstOrDefault(r => r.Id == restaurant.Id);

        if (restaurantToEdit != null)
        {
            restaurantToEdit.borough = restaurant.borough;
            restaurantToEdit.name = restaurant.name;
            restaurantToEdit.cuisine = restaurant.cuisine;

            context.Restaurants.Update(restaurantToEdit);
            
            context.ChangeTracker.DetectChanges();
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("There is no such restaurant to edit");
        }
    }
}
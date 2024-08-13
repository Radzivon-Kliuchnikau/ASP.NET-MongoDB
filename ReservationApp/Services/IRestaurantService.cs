using MongoDB.Bson;
using ReservationApp.Models;

namespace ReservationApp.Services;

public interface IRestaurantService
{
    IEnumerable<Restaurant> GetAllRestaurants();

    Restaurant? GetRestaurantById(ObjectId id);

    void AddRestaurant(Restaurant restaurant);

    void RemoveRestaurant(Restaurant restaurant);

    void EditRestaurant(Restaurant restaurant);
}
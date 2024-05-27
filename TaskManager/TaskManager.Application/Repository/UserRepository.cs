using System.Security.Cryptography;
using System.Text;
using TaskManager.Application.Domain;
using MongoDB.Driver;

namespace TaskManager.Application.Repository;

public class UserRepository {
    private readonly IMongoCollection<User> _user;

    public UserRepository(IMongoCollection<User> user) {
        _user = user;
    }

    public void DeleteAll() {
        var filter = Builders<User>.Filter.Empty;
        _user.DeleteMany(filter);
    }

    public void CreateUser(User user) {
        if (DoesUserExistByEmail(user.Email)) {
            throw new InvalidOperationException($"User with email '{user.Email}' already exists.");
        }

        _user.InsertOne(user);
    }

    public bool DoesUserExistByEmail(string email) {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        return _user.Find(filter).Any();
    }
    
    public string HashPassword(string password) {
        using (var sha256 = SHA256.Create()) {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }
    }
    
    public Guid GetUserIdByEmail(string email) {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        var user = _user.Find(filter).FirstOrDefault();
        if (user != null) {
            return user._id;
        } else {
            throw new InvalidOperationException($"No user found with email '{email}'.");
        }
    }

    public User GetUserByEmail(string email) {
        return _user.Find<User>(u => u.Email == email).FirstOrDefault();
    }

    public bool ValidatePassword(string inputPassword, string storedHash) {
        string hashedInput = HashPassword(inputPassword);
        return hashedInput == storedHash;
    }
    
    public User GetUserById(Guid userId) {
        return _user.Find(u => u._id == userId).FirstOrDefault();
    }
    
    public void UpdateUser(Guid userId, User updatedUser) {
        var filter = Builders<User>.Filter.Eq(u => u._id, userId);
        var update = Builders<User>.Update
            .Set(u => u.Profile.Vorname, updatedUser.Profile.Vorname)
            .Set(u => u.Profile.Nachname, updatedUser.Profile.Nachname)
            .Set(u => u.Profile.Geburtsdatum, updatedUser.Profile.Geburtsdatum);
        _user.UpdateOne(filter, update);
    }
}
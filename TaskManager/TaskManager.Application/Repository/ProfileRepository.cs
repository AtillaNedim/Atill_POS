using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TaskManager.Application.Domain;

namespace TaskManager.Application.Repository {
    public class ProfileRepository {
        private readonly IMongoCollection<Profile> _profiles;
        private readonly ILogger<ProfileRepository> _logger;

        public ProfileRepository(IMongoCollection<Profile> profiles) {
            _profiles = profiles;
        }

        public Profile GetProfileById(Guid id) {
            return _profiles.Find<Profile>(profile => profile._id == id).SingleOrDefault();
        }

        public void CreateProfile(Profile profile) {
            _profiles.InsertOne(profile);
        }

        public void UpdateProfile(Profile profile) {
            var filter = Builders<Profile>.Filter.Eq(p => p._id, profile._id);
            _profiles.ReplaceOne(filter, profile);
        }
        
        public async Task<bool> DeleteProfilebyid(Guid pId) {
            try {
                var deleteResult = await _profiles.DeleteOneAsync(x => x._id == pId);
                return deleteResult.DeletedCount > 0;
            } catch (Exception ex) {
                _logger.LogError("An error occurred while deleting the profile with ID {ProfileId}: {ExceptionMessage}", pId, ex.Message);
                return false;
            }
        }

    }
}
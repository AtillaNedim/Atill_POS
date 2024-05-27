using MongoDB.Driver;
using TaskManager.Application.Domain;

namespace TaskManager.Application.Repository {
    public class ProfileRepository {
        private readonly IMongoCollection<Profile> _profiles;

        public ProfileRepository(IMongoCollection<Profile> profiles) {
            _profiles = profiles;
        }

        public Profile GetProfileById(Guid id) {
            return _profiles.Find<Profile>(profile => profile.Id == id).SingleOrDefault();
        }

        public void CreateProfile(Profile profile) {
            _profiles.InsertOne(profile);
        }

        public void UpdateProfile(Profile profile) {
            var filter = Builders<Profile>.Filter.Eq(p => p.Id, profile.Id);
            _profiles.ReplaceOne(filter, profile);
        }
    }
}
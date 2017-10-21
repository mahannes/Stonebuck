//using MongoDB.Bson;
//using MongoDB.Driver;
//using System.Collections.Generic;
//using System.Linq;

//namespace NewsApp.Models
//{
//    public class DataAccess
//    {
//        IMongoClient _client;
//        IMongoDatabase _db;

//        public DataAccess()
//        {
//            _client = new MongoClient("mongodb://localhost:27017");
//            _db = _client.GetDatabase("NewsAppDB");
//        }

//        public UserModel GetUser(int id)
//        {
//            var collection = _db.GetCollection<UserModel>("Users");
//            var filter = Builders<UserModel>.Filter.Eq("userid", id);
//            return collection.Find(filter).ToEnumerable().FirstOrDefault();
//        }

//        public UserModel GetUserByName(string name)
//        {
//            var collection = _db.GetCollection<UserModel>("Users");
//            var filter = Builders<UserModel>.Filter.Eq("name", name);
//            return collection.Find(filter).ToEnumerable().FirstOrDefault();
//        }

//        public UserModel GetUserByApplicationUserId(string applicationUserId)
//        {
//            var collection = _db.GetCollection<UserModel>("Users");
//            var filter = Builders<UserModel>.Filter.Eq("application_user_id", applicationUserId);
//            return collection.Find(filter).ToEnumerable().FirstOrDefault();
//        }


//        public void AddUser(UserModel user)
//        {
//            var collection = _db.GetCollection<UserModel>("Users");
//            collection.InsertOne(user);
//        }

//        public UserModel AddUser(ApplicationUser applicationUser)
//        {
//            var userModel = new UserModel()
//            {
//                Name = applicationUser.UserName,
//                ApplicationUserId = applicationUser.Id,
//                Categories = new List<string>()
//            };
//            var collection = _db.GetCollection<UserModel>("Users");
//            collection.InsertOne(userModel);
//            return userModel;
//        }

//        public void AddCategoryToUser(string applicationUserId, string category)
//        {
//            var filter = Builders<UserModel>.Filter.Eq("application_user_id", applicationUserId);
//            var collection = _db.GetCollection<UserModel>("Users");
//            var update = Builders<UserModel>.Update.AddToSet("Categories", category);
//            collection.UpdateOne(filter, update);
//        }
//    }
//}



﻿using UserManagement.Api.Models;
using Newtonsoft.Json;
using System.Threading;

namespace UserManagement.Api.Repositories
{
    internal static class JsonUserStorage
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "Users.json");
      

        public static List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            var jsonData = File.ReadAllText(FilePath);
            var wrapper = JsonConvert.DeserializeObject<UserWrapper>(jsonData);
            return wrapper?.Users ?? new List<User>();
        }

        public static void SaveUsers(List<User> users)
        {
            try
            {
                var wrapper = new UserWrapper
                {
                    Users = users
                };
                var jsonData = JsonConvert.SerializeObject(wrapper, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(FilePath, jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving users to file: " + ex.Message);
            }
        }

        private class UserWrapper
        {
            public List<User> Users { get; set; } = new List<User>();
        }
    }
}

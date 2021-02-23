using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            string userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");  // get the seed text from the file.

            List<AppUser> users = JsonSerializer.Deserialize<List<AppUser>>(userData);      // turn it all into objects

            foreach (var user in users)
            {
                using HMACSHA512 hmac = new HMACSHA512();                                   // get a Cryptography object
                user.UserName = user.UserName.ToLower();                                    // all usernames in stored in the Db will be lower case.
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));   // convert the password into a hash code
                user.PasswordSalt = hmac.Key;                                               // the hmac object comes with a key property that allows you to decode it the hash. 
                context.Users.Add(user);                                                    // this add the object to EF tracking
            }

            // context.Add(users);//add the entire List<> to the context at once. MAKE SURE THIS WORKS!!

            context.SaveChanges();                                                   // Async save the changes.



        }
    }
}
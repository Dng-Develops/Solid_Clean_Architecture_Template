using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitiyFramework
{
    public class EfUserDal : IUserDal
    {
        public void Add(User user)
        {
            using(var db = new ApplicationDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void Delete(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public User Get(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.Where(i => i.Id == id).FirstOrDefault();
            }
        }

        public List<User> GetAll()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.ToList();
            }
        }

        public void Update(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }
    }
}

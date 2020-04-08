using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Model;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TrailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateTrail(Trail trail)
        {
            _dbContext.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _dbContext.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _dbContext.Trails.Include(np => np.NationalPark).FirstOrDefault(myUser => myUser.Id == trailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return _dbContext.Trails.OrderBy(myUser => myUser.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            return _dbContext.Trails.Any(myUser => myUser.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool TrailExists(int id)
        {
            return _dbContext.Trails.Any(myUser => myUser.Id == id);
        }

        public bool Save()
        {
            try
            {
                return _dbContext.SaveChanges() >= 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateTrail(Trail trail)
        {
            _dbContext.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int npId)
        {
            return _dbContext.Trails.Include(np => np.NationalPark).Where(x => x.NationalParkId == npId).ToList();
        }
    }
}

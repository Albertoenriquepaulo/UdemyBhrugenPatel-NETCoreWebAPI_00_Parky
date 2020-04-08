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
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NationalParkRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _dbContext.NationalParks.FirstOrDefault(myUser => myUser.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _dbContext.NationalParks.OrderBy(myUser => myUser.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            return _dbContext.NationalParks.Any(myUser => myUser.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParkExists(int id)
        {
            return _dbContext.NationalParks.Any(myUser => myUser.Id == id);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _dbContext.Update(nationalPark);
            return Save();
        }
    }
}

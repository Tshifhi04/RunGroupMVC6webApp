using Microsoft.EntityFrameworkCore;
using MVC6.Data;
using MVC6.Interfaces;
using MVC6.Models;
using System.Diagnostics;

namespace MVC6.Repository
{
    public class RaceRepository : IRaceRepository
    {

        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context) 
        {
            _context = context;        
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {

            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            // IEnumerable is a list
            //when using async with task  have to use ToListAsync();
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i =>i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRacesByCity(string city)
        {
            //goes into Race and goes into address and searches by city
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

      

        public bool Save()
        {
            var saved = _context.SaveChanges();
            //if save is grater than 0 it returns  true means saved elsse is not saved
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}

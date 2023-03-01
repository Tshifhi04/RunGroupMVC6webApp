using Microsoft.EntityFrameworkCore;
using MVC6.Data;
using MVC6.Interfaces;
using MVC6.Models;
using System.Diagnostics;

namespace MVC6.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {

            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
           // IEnumerable is a list
            //when using async with task  have to use ToListAsync();
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            //include .Include(i =>i.Address). is important with navigation property 1 to many (foreiggn key property)
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            //goes into club and goes into address and searches by city
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            //if save is grater than 0 it returns  true means saved elsse is not saved
            return saved > 0? true:false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}

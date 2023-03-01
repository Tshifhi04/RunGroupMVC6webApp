using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC6.Data;
using MVC6.Interfaces;
using MVC6.Models;
using MVC6.Repository;

namespace MVC6.Controllers
{
    public class RaceController : Controller
    {
        //injected ....brought appDbContextT from another part
       // private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _raceRepository;

        public RaceController(ApplicationDbContext context, IRaceRepository raceRepository)
        {
            //_context = context;
            _raceRepository = raceRepository;

        }

        public async Task <IActionResult> Index() //controller
        {
            //to list needs to be called in to excecute the sql
            //has to return the list of clubs in the clubs table in the database
            IEnumerable<Race> races= await _raceRepository.GetAll(); //model

            return View(races); //view
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            //Includes are joins and must recognise why we use them coz they are expensive!!
            //Includes are joins and must recognise why we use them coz they are expensive!!
            Race races = await _raceRepository.GetByIdAsync(id);
            return View(races);
        }
    }
}

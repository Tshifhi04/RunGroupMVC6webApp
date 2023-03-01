using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MVC6.Data;
using MVC6.Interfaces;
using MVC6.Models;

namespace MVC6.Controllers
{
    public class ClubController : Controller
    {
        //injected ....brought appDbContextT from another part
       // private readonly ApplicationDbContext _context;
        private readonly IClubRepository _clubRepository;


        public ClubController(ApplicationDbContext context,IClubRepository clubRepository) 
        {
               //  _context=context;
            _clubRepository = clubRepository;
        }

        public IActionResult Create() 
        {
            return View();
        }

        public  async Task <IActionResult> Index() //controller
        {
            //to list needs to be called in to excecute the sql
            //has to return the list of clubs in the clubs table in the database
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); //model
            
            return View(clubs); //view
        }

        [HttpGet]
        public async Task <IActionResult> Detail(int id) 
        {
            //Includes are joins and must recognise why we use them coz they are expensive!!
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MVCmovieAPP.Controllers
{
	public class MoviesController : Controller
	{

        // constructor for the controller
        public MoviesController() =>
                   _context = new FakeContext();


        // GET: MoviesController
        FakeContext _context;
		
		public ActionResult Index()
		{
			
			return _context.Movie != null ?        //// Ternary expression operator
				View(_context.Movie) :
				Problem("Movie list is null.");
		}
			// GET: MoviesController/Details/5
		/*	public ActionResult Details(int id)
		{
			return View();
		}*/

		// GET: MoviesController/Create
		public ActionResult Create()
		{
			return View();           /// we just print blank form here
			                         /// by convention it takes a view from respective controller
		}

		// POST: MoviesController/Create


		[HttpPost]
		[ValidateAntiForgeryToken]//way of server finding out if the client is who he saying to be,
                                  //not any middleman 
                                  // overposting attacks



        // POST: MoviesController/Create


        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            //send back what the user entered and don't present a blank form again!
            return View(movie);
        }


        // GET: MoviesController/Details/5
        public ActionResult Details(int? id) // Binding
        {
            if (id == null || _context.Movie == null)     // checking
                return NotFound();          // defined in controller base 

            var movie = _context.Movie.Single(m => m.Id == id);
			                               // linq used to filter
            if (movie == null)
                return NotFound();

            return View(movie);    /// here view conventionally goes to details's controller ,
			                        //movies folder,
				                    //details view
        }



        // GET: MoviesController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = _context.Movie.Single(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }



        // POST: MoviesController/Edit/5
        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: MoviesController/Delete/5
        // GET: MoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = _context.Movie.Single(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
		[ValidateAntiForgeryToken]
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Movie data is null!");
            }
            var movie = _context.Movie.Single(m => m.Id == id);

            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestEmptyApp.WebUI.Models.DataContext;
using TestEmptyApp.WebUI.Models.Entities;

namespace TestEmptyApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly BankDbContext db;
        //readonly SignInManager<BankUser> signinManager;
        //readonly UserManager<BankUser> userManager;
        //public HomeController(SignInManager<BankUser> signinManager,
        //    UserManager<BankUser> userManager,
        //    BankDbContext db)
        //{
        //    this.signinManager = signinManager;
        //    this.userManager = userManager;
        //    this.db = db;
        //}
        public HomeController(BankDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("signin.html")]
        public IActionResult Signin()
        {
            return View();
        }
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("signin.html")]
        //public async Task<IActionResult> Signin(SigninFormModel model)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    var founded = await userManager.FindByNameAsync(model.UserName);
        //    //    if (founded == null)
        //    //        goto end;
        //    //    var signinResult = await signinManager.PasswordSignInAsync(founded, model.Password, true,false);
        //    //    if (signinResult.Succeeded)
        //    //    {
        //    //        return Redirect("/Cards");
        //    //    }
        //    //}
        //    //end:
        //    //ViewBag.Message = "Istifadeci adi ve ya sifre yanlisdir !";
        //    return View(model);
        //}
        public async Task<IActionResult> Cards()
        {
            var data = await db.Products.ToListAsync();
            return View(data);
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Product model)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        //[Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return View();//404
            }

            var entity = await db.Products.FirstOrDefaultAsync(b => b.Id == id && b.ExpireDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }

            return View(entity);
        }


        //[Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit([FromRoute] int id, Product model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id != model.Id || id < 1)
            {
                return BadRequest();
            }

            var entity = await db.Products.FirstOrDefaultAsync(b => b.Id == id && b.ExpireDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }

            entity.CardNumber = model.CardNumber;
            entity.ExpireDate = model.ExpireDate;
            //db.Products.Update(entity);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        //[Authorize(Policy = "admin.brands.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            //throw new Exception("Error");
            if (id < 1)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı"
                });
            }

            var entity = await db.Products.FirstOrDefaultAsync(b => b.Id == id && b.ExpireDate == null);
            if (entity == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı"
                });
            }

            //db.Brands.Remove(entity);
            await db.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Məlumat silindi"
            });
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                //ModelState.Clear();
                //ViewBag.Message = "Sizin sorğunuz qəbul edilmişdir.Tezliklə geri dönüş edəcəyik.";
                //return View();
                return Json(new
                {
                    error = false,
                    message = "Sizin sorgunuz qebul edilmisdir.Tezlikle geri donuş edeceyik."
                });
            }


            //return View(contact);
            return Json(new
            {
                error = true,
                message = "Melumatlarin dogrulugundan emin olun"
            });
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.user.Add(user);
                db.SaveChanges();
                //ModelState.Clear();
                //ViewBag.Message = "Sizin sorğunuz qəbul edilmişdir.Tezliklə geri dönüş edəcəyik.";
                //return View();
                ViewBag.Message = "Sizin qeydiyyat ugurla yerine yetirilmisdir.!";
                return View();
            }


            //return View(contact);
            return Json(new
            {
                error = true,
                message = "Melumatlarin dogrulugundan emin olun"
            });
        }
    }
}

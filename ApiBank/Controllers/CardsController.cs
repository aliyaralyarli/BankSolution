using ApiBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        readonly BankDbContext db;
        public CardsController(BankDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cards = db.Cards.ToList();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var card = db.Cards.FirstOrDefault(c => c.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public IActionResult Post(Card model)
        {
            db.Cards.Add(model);
            db.SaveChanges();
            return CreatedAtAction("Get", routeValues: new
            {
                id = model.Id,
            }, value: model);
        }

        [HttpPut]
        public IActionResult Put([FromRoute] int id,Card model)
        {
            var card= db.Cards.FirstOrDefault(c => c.Id == id);
            if (card==null)
            {
                return NotFound();
            }

            if (card!=null)
            {
                card.Id = model.Id;
                card.CardNumber = model.CardNumber;
                card.ExpireDate = model.ExpireDate;
                db.SaveChanges();
            }
            return AcceptedAtAction("Get", new
            {
                id = card.Id,
            }, card);
        }

        [HttpDelete("{id")]
        public IActionResult Delete(int id)
        {
            var card = db.Cards.FirstOrDefault(c => c.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            if (card != null)
            {
                db.Cards.Remove(card);
                db.SaveChanges();
            }
            return NoContent();
        }
    }
}

namespace RuleService.Controllers
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.OData;
    using Models;

    public sealed class RulesController : ODataController
    {
        private RuleServiceDataContext db = new RuleServiceDataContext();

        // GET: odata/Rules
        [EnableQuery]
        public IQueryable<Rule> GetRules()
        {
            return db.Rules;
        }

        // GET: odata/Rules(5)
        [EnableQuery]
        public SingleResult<Rule> GetRule([FromODataUri] int key)
        {
            return SingleResult.Create(db.Rules.Where(rule => rule.Id == key));
        }

        // PUT: odata/Rules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Rule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rule rule = await db.Rules.FindAsync(key);
            if (rule == null)
            {
                return NotFound();
            }

            patch.Put(rule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(rule);
        }

        // POST: odata/Rules
        public async Task<IHttpActionResult> Post(Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rules.Add(rule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RuleExists(rule.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(rule);
        }

        // PATCH: odata/Rules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Rule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rule rule = await db.Rules.FindAsync(key);
            if (rule == null)
            {
                return NotFound();
            }

            patch.Patch(rule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(rule);
        }

        // DELETE: odata/Rules(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Rule rule = await db.Rules.FindAsync(key);
            if (rule == null)
            {
                return NotFound();
            }

            db.Rules.Remove(rule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RuleExists(int key)
        {
            return db.Rules.Count(e => e.Id == key) > 0;
        }
    }
}

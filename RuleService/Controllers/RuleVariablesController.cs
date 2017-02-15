namespace RuleService.Controllers
{
    using System;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.OData;
    using Models;

    public class RuleVariablesController : ODataController
    {
        private RuleServiceDataContext db = new RuleServiceDataContext();

        // GET: odata/RuleVariables
        [EnableQuery]
        public IQueryable<RuleVariable> GetRuleVariables()
        {
            return db.RuleVariables;
        }

        // GET: odata/RuleVariables(5)
        [EnableQuery]
        public SingleResult<RuleVariable> GetRuleVariable([FromODataUri] int key)
        {
            return SingleResult.Create(db.RuleVariables.Where(ruleVariable => ruleVariable.Id == key));
        }

        // PUT: odata/RuleVariables(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RuleVariable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RuleVariable ruleVariable = await db.RuleVariables.FindAsync(key);
            if (ruleVariable == null)
            {
                return NotFound();
            }

            patch.Put(ruleVariable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleVariableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ruleVariable);
        }

        // POST: odata/RuleVariables
        public async Task<IHttpActionResult> Post(RuleVariable ruleVariable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RuleVariables.Add(ruleVariable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RuleVariableExists(ruleVariable.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(ruleVariable);
        }

        // PATCH: odata/RuleVariables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RuleVariable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RuleVariable ruleVariable = await db.RuleVariables.FindAsync(key);
            if (ruleVariable == null)
            {
                return NotFound();
            }

            patch.Patch(ruleVariable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleVariableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ruleVariable);
        }

        // DELETE: odata/RuleVariables(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RuleVariable ruleVariable = await db.RuleVariables.FindAsync(key);
            if (ruleVariable == null)
            {
                return NotFound();
            }

            db.RuleVariables.Remove(ruleVariable);
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

        private bool RuleVariableExists(int key)
        {
            return db.RuleVariables.Count(e => e.Id == key) > 0;
        }
    }
}

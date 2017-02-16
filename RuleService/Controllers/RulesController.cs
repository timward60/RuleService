namespace RuleService.Controllers
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using Models;
    using Repository;
    using Repository.Fake;

    public sealed class RulesController : ODataController
    {
        private IRepository _repository = FakeRepository.Instance;

        // GET: odata/Rules
        [EnableQuery]
        public IQueryable<Rule> GetRules()
        {
            return _repository.Rules;
        }

        // GET: odata/Rules(5)
        [EnableQuery]
        public SingleResult<Rule> GetRule([FromODataUri] int key)
        {
            return SingleResult.Create(_repository.Rules.Where(rule => rule.Id == key));
        }

        // PUT: odata/Rules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Rule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rule rule = await _repository.Rules.FindAsync(key);
            if (rule == null)
            {
                return NotFound();
            }

            patch.Put(rule);

            try
            {
                await _repository.SaveChangesAsync();
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

            _repository.Rules.Add(rule);

            try
            {
                await _repository.SaveChangesAsync();
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

            Rule rule = await _repository.Rules.FindAsync(key);
            if (rule == null)
            {
                return NotFound();
            }

            patch.Patch(rule);

            try
            {
                await _repository.SaveChangesAsync();
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
            Rule rule = await _repository.Rules.FindAsync(key);
            if (rule == null)
            {
                return NotFound();
            }

            _repository.Rules.Remove(rule);
            await _repository.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RuleExists(int key)
        {
            return _repository.Rules.Count(e => e.Id == key) > 0;
        }
    }
}

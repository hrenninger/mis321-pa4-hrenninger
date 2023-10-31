using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        // GET: api/Workouts
        [HttpGet]
        public List<Workouts> Get()
        {
            WorkoutsUtility utility = new WorkoutsUtility();
            return utility.GetAllWorkouts();
        }

        // GET: api/Workouts/5
        [HttpGet("{id}", Name = "Get")]
        public Workouts Get(int Id)
        {
            WorkoutsUtility utility = new WorkoutsUtility();
            List<Workouts> myWorkouts = utility.GetAllWorkouts();
            foreach(Workouts workout in myWorkouts){
                if(workout.id == Id){
                    return workout;
                }
            }
            return new Workouts();
        }

        // POST: api/Workouts
        [HttpPost]
        public void Post([FromBody] Workouts value)
        {
            Console.WriteLine(value.id);
            WorkoutsUtility utility = new WorkoutsUtility();
            utility.AddWorkout(value);
        }

        // PUT: api/Workouts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Workouts value)
        {
            Console.WriteLine(id);
            value.pinned = (value.pinned == false) ? true : false; 
            Console.WriteLine(value.pinned);
            WorkoutsUtility utility = new WorkoutsUtility();
            utility.UpdateWorkouts(value);
            
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] Workouts value)
        {
            value.deleted = (value.deleted == false) ? true : false;
            WorkoutsUtility utility = new WorkoutsUtility();
            utility.UpdateWorkouts(value);
        }
    }
}

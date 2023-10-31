using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Workouts
    {
        public int id {get;set;}
        public string exercise {get;set;}
        public double distance {get;set;}
        public string date {get;set;}
        public bool pinned{get;set;}
        public bool deleted {get;set;}
    }
}
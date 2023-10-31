using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DataAccess
{
    public class Database
    {
        private string host {get;set;}
        private string database {get;set;}
        private string username {get;set;}
        private string port {get;set;}
        private string password {get;set;}

        public string cs {get;set;}

        public Database(){
            host = "n4m3x5ti89xl6czh.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            database = "qhws8mugo09sbe8x";
            username = "a6amiqjo04oruqsk";
            port = "3306";
            password = "yfo8660uly9s4fnl";
            cs = $"server={host};user={username};database={database};port={port};password={password}";
        }
    }
}
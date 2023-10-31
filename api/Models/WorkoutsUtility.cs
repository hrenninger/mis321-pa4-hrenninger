using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using api.DataAccess;
using Microsoft.AspNetCore.SignalR.Protocol;
using MySql.Data.MySqlClient;

namespace api.Models
{
    public class WorkoutsUtility
    {

        public List<Workouts> GetAllWorkouts(){
            List<Workouts> myWorkouts = new List<Workouts>();
            Database db = new Database();
            using var con = new MySqlConnection(db.cs);
            con.Open();
            string stm = "SELECT * from workouts order by Date desc";
            using var cmd = new MySqlCommand(stm,con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()){
                Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetDouble(2)} {rdr.GetString(3)}");
                Workouts instance = new Workouts();
                instance.id = rdr.GetInt32(0);
                instance.exercise = rdr.GetString(1);
                instance.distance = rdr.GetDouble(2);
                instance.date = rdr.GetString(3);
                instance.pinned = rdr.GetBoolean(4);
                instance.deleted = rdr.GetBoolean(5);
                myWorkouts.Add(instance);
            }
            con.Close();
            return myWorkouts;
        }

        public void AddWorkout(Workouts workout){
            Database db = new Database();
            using var con = new MySqlConnection(db.cs);
            con.Open();
            string cmdText = @"INSERT INTO workouts(Exercise, Distance, Date) 
            VALUES(@exercise, @distance, @date)";
            MySqlCommand cmd = new MySqlCommand(cmdText, con);
            cmd.Parameters.AddWithValue("@exercise", workout.exercise);
            cmd.Parameters.AddWithValue("@distance", workout.distance);
            cmd.Parameters.AddWithValue("@date", workout.date);
            //cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateWorkouts(Workouts workouts){
            Database db = new Database();
            using var con = new MySqlConnection(db.cs);
            con.Open();

            string query = "UPDATE Workouts SET Pinned = @pinned, Deleted = @deleted WHERE Id =@id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@pinned", Convert.ToBoolean(workouts.pinned));
            cmd.Parameters.AddWithValue("@deleted", Convert.ToBoolean(workouts.deleted));
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(workouts.id));
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
            Console.WriteLine($"Update Complete: {workouts}");
        }
    }
}
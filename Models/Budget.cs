using System;
using System.Collections.Generic;
using System.Linq;
using budget.DB;
using MongoDB.Bson;

namespace budget.Models
{
    public class Budget
    {
        public ObjectId Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Item> Estimateds { get; set; }
        public List<Item> Performeds { get; set; }

        public Budget()
        {
            Estimateds = new List<Item>();
            Performeds = new List<Item>();
        }

        public static Budget Get(String id, String connectionString)
        {
            var manager = new DBManager<Budget>(connectionString, "Budget", "Budgets");

            return manager.RetrieveOne(x => x.Id == ObjectId.Parse(id));
        }

        public static IEnumerable<Budget> Get(DateTime start, DateTime end, String connectionString)
        {
            var manager = new DBManager<Budget>(connectionString, "Budget", "Budgets");

            return manager.RetrieveAll(x => x.Start >= start && x.End <= end);
        }

        public static void Save(List<Budget> entitites, String connectionString)
        {
            if (entitites.Count > 0)
            {
                var manager = new DBManager<Budget>(connectionString, "Budget", "Budgets");

                manager.Create(entitites);
            }
        }

        public static void Save(Budget entitity, String connectionString)
        {
            if (entitity != null)
            {
                var manager = new DBManager<Budget>(connectionString, "Budget", "Budgets");

                entitity.Estimateds = entitity.Estimateds.OrderBy(x => x.Date).ToList();

                if (entitity.Id.ToString() == "000000000000000000000000")
                    manager.Create(entitity);
                else
                    manager.Update(x => x.Id == entitity.Id, entitity);
            }
        }
    }
}
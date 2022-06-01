using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Services
{
    public class RandomService : IRandomService
    {
        private int randomNumber;

        public RandomService()
        {
            Random random = new Random();
            randomNumber = random.Next(1000000);
        }
        public int GetNumber()
        {
            return randomNumber;
        }
    }
}

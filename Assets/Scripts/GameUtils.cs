using System;
using System.Collections.Generic;
using System.Linq;

namespace Palomas
{
    public class GameUtils
    {
        public static T RandomElement<T>(ICollection<T> collection)
        {
            T randomElement;
            if (collection.Count() > 1)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, collection.Count());
                randomElement = collection.ElementAt(randomIndex);
            }
            else
            {
                randomElement = collection.FirstOrDefault();
            }

            return randomElement;
        }
    }
}

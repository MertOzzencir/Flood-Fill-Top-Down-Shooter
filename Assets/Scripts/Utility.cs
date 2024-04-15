using System;
using System.Collections;
using System.Collections.Generic;

public static class Utility 
{

    public static T[] ShuffledArray<T>(T[] array, int seed)
    {
        Random prng = new Random(seed);

        for(int i = 0; i< array.Length -1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T holder = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = holder;

        }

        return array;

    }

}

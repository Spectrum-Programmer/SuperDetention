using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class GlobalScript
{

    public enum operation
    {
        Addition = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3,
        Modulus = 4,
        
        SquareRoot = 5,
        Squared = 6,
        Log2 = 7,
        Cubed = 8,
        
        Factorial = 9,
        Fibonacci = 10,
    
        None = 11
    }

    private static readonly Sprite[] icons =  new Sprite[11];
    
    public static readonly Dictionary<operation, Sprite> iconDictionary = new();
    
    public static readonly Dictionary<string, string> wordDictionary = new();
    
    
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        string[] iconPaths = 
        {
            "Icons/Symbol1",
            "Icons/Symbol2",
            "Icons/Symbol3",
            "Icons/Symbol4",
            "Icons/Symbol5",
            "Icons/Symbol6",
            "Icons/Symbol7",
            "Icons/Symbol8",
            "Icons/Symbol9",
            "Icons/Symbol10",
            "Icons/Symbol11",
        };

        for (var i = 0; i < iconPaths.Length; i++) { icons[i] = Resources.Load<Sprite>(iconPaths[i]); }
        ShuffleIcons();
        
        string[] words = {
            "Clock",
            "Soccer",
            "Pencil",
            "Chalk",
            "Phone",
            "Bus",
            "June",
            "Coffee",
            "Basketball",
            "Diamond",
            "Witch",
            "Library",
            "Birthday",
            "Graduation",
            "Tree",
            "Stop",
            "Gum",
            "Lunch",
            "Good",
            "Ruler"
        };
        
        string[] new_words = {
            "Grumhog",
            "Plerian",
            "Brimbam",
            "Clep",
            "Amero",
            "Quindle",
            "Mellin",
            "Zorbo",
            "Rallick",
            "Dyme",
            "Wickle",
            "Insteen",
            "Oriph",
            "Vlosto",
            "Yemmir",
            "Frinka",
            "Blamadoo",
            "Limander",
            "Jerio",
            "Klump"
        };
        
        ShuffleWords(words, new_words);
    }
    
    private static void ShuffleIcons()
    {
        iconDictionary.Clear();
        
        var local_icons =  new Sprite[11];
        icons.CopyTo(local_icons, 0);
        local_icons = Shuffle(local_icons);
        for (var i = 0; i < local_icons.Length; i++)
        {
            iconDictionary.Add((operation)i, local_icons[i]);
        }
    }

    private static Sprite[] Shuffle(Sprite[] array)
    {
        var returnArray = new Sprite[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            var index = Random.Range(i, 11);
            returnArray[i] = array[index];
            array[index] = array[i];
        }
        return returnArray;
    }
    
    private static void ShuffleWords(string[] words, string[] newWords)
    {
        wordDictionary.Clear();
        
        var new_local_strings = new string[20];
        newWords.CopyTo(new_local_strings, 0);
        new_local_strings = ShuffleStrings(new_local_strings);
        
        var local_strings = new string[20];
        words.CopyTo(local_strings, 0);
        local_strings = ShuffleStrings(local_strings);
        
        for (var i = 0; i < local_strings.Length; i++)
        {
            wordDictionary.Add(local_strings[i], new_local_strings[i]);
        }
    }
    
    private static string[] ShuffleStrings(string[] array)
    {
        var returnArray = new string[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            var index = Random.Range(i, 20);
            returnArray[i] = array[index];
            array[index] = array[i];
        }
        return returnArray;
    }
}



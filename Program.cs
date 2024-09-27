namespace Challenges;

using System.Diagnostics;

/*
Problem 1
Count the number of Duplicates
Write a function that will return the count of distinct case-insensitive alphabetic characters and numeric digits that occur more than once in the input string. 
The input string can be assumed to contain only alphabets (both uppercase and lowercase) and numeric digits.

Example
"abcde" -> 0 # no characters repeats more than once
"aabbcde" -> 2 # 'a' and 'b'
"aabBcde" -> 2 # 'a' occurs twice and 'b' twice (`b` and `B`)
"indivisibility" -> 1 # 'i' occurs six times
"Indivisibilities" -> 2 # 'i' occurs seven times and 's' occurs twice
"aA11" -> 2 # 'a' and '1'
"ABBA" -> 2 # 'A' and 'B' each occur twice
*/

/*
Problem 2
If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.

Finish the solution so that it returns the sum of all the multiples of 3 or 5 below the number passed in.

Additionally, if the number is negative, return 0.

Note: If the number is a multiple of both 3 and 5, only count it once.
*/

/*
Problem 3
Write a function that takes in a string of one or more words, and returns the same string, but with all words that have five or more 
letters reversed (Just like the name of this Kata). Strings passed in will consist of only letters and spaces. Spaces will be included only when 
more than one word is present.

Examples:

"Hey fellow warriors"  --> "Hey wollef sroirraw" 
"This is a test        --> "This is a test" 
"This is another test" --> "This is rehtona test"
*/

/*
Problem 4
In this kata you are required to, given a string, replace every letter with its position in the alphabet.

If anything in the text isn't a letter, ignore it and don't return it.

"a" = 1, "b" = 2, etc.

Example
Input = "The sunset sets at twelve o' clock."
Output = "20 8 5 19 21 14 19 5 20 19 5 20 19 1 20 20 23 5 12 22 5 15 3 12 15 3 11"
*/

/*
Problem 5
Complete the method/function so that it converts dash/underscore delimited words into camel casing. 
The first word within the output should be capitalized only if the original word was capitalized 
(known as Upper Camel Case, also often referred to as Pascal case). The next words should be always capitalized.

Examples
"the-stealth-warrior" gets converted to "theStealthWarrior"

"The_Stealth_Warrior" gets converted to "TheStealthWarrior"

"The_Stealth-Warrior" gets converted to "TheStealthWarrior"
*/

public class Program
{
  public static void Main()
  {
    var result1 = CountDuplicates("indivisibility");
    Console.WriteLine($"result problem 1: {result1}");
    
    var watch1 = Stopwatch.StartNew();
    var result2v1 = CountMultiples1(1000);
    watch1.Stop();
    Console.WriteLine($"result problem 2 (v1): {result2v1} -> {watch1.ElapsedMilliseconds}ms");
    
    var watch2 = Stopwatch.StartNew();
    var result2v2 = CountMultiples2(1000);
    watch2.Stop();
    Console.WriteLine($"result problem 2 (v2): {result2v2} -> {watch2.ElapsedMilliseconds}ms");

    var result3 = ReverseWords("This is another test");
    Console.WriteLine($"result problem 3: {result3}");
    
    var result4 = ReturnAlphabetAscii("The sunset sets at twelve o' clock.");
    Console.WriteLine($"result problem 4: {result4}");

    var result5 = ConvertToCamelCase("the-stealth-warrior_a");
    Console.WriteLine($"result problem 5: {result5}");
  }

  public static int CountDuplicates(string characters)
  {
    const int BaseCount = 1;
    var duplicatesCount = 0;
    var normalized = characters.ToLower();
    var characterTracker = new Dictionary<char, int>();

    foreach (var character in normalized)
    {
      if (characterTracker.ContainsKey(character))
      {
        characterTracker[character]++;

        if (characterTracker[character] == 2) {
          duplicatesCount++;
        }
      }
      else 
      {
        characterTracker.Add(character, BaseCount);
      }
    }

    return duplicatesCount;
  }

  public static int CountMultiples1(int top)
  {
    if (top > 2) {
      int highest = top - 1;
      int sumFor3 = CalculateSumOfMultiples(3, highest);
      int sumFor5 = CalculateSumOfMultiples(5, highest);
      int sumFor15 = CalculateSumOfMultiples(15, highest);

      return sumFor5 + sumFor3 - sumFor15;
    }

    return 0;
  }

  public static int CalculateSumOfMultiples(int multiplier, int number)
  {
    var result = 0;

    if (multiplier > 1 && multiplier <= number) {
      int lowest = multiplier;
      int highest = number - (number % multiplier);
      int quantity = highest / multiplier;
      result = (highest + lowest) * quantity / 2;
    }

    return result;
  }

  public static int CountMultiples2(int top)
  {
    int result = 0;
    
    if (top < 3)
    {
      return result;
    }

    for(int i = 3; i < top; i++)
    {
      if (i % 3 == 0 || i % 5 ==0)
      {
        result += i;
      }
    }

    return result;
  }

  public static string ReverseWords(string sentence)
  {
    var newSentence = string.Empty;
    var words = sentence.Split(" ");

    foreach (var word in words)
    {
      var newWord = word.Length > 4 ? new string(word.ToCharArray().Reverse().ToArray()) : word;
      newSentence += $" {newWord}";
    }
    return newSentence.Trim();
  }

  public static string ReturnAlphabetAscii(string letters)
  {
    var result = string.Empty;

    foreach (var letter in letters.ToLower())
    {
      var ascii = (int) letter;

      if (ascii >= 97 && ascii < 122)
      {
        var position = ascii - 96;
        result += $" {position}";
      }
    }

    return result.Trim();
  }

  public static string ConvertToCamelCase(string text)
  {
    var result = string.Empty;
    var words = text.Split('-', '_');
    var head = words.FirstOrDefault();
    var tail = words.Skip(1);

    foreach (var word in tail)
    {
      var normalized = $"{char.ToUpper(word[0])}{word[1..].ToLower()}";
      result += normalized;
    }

    result = $"{head}{result}";

    return result;
  }
}
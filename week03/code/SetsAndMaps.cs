using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
{
    // Create a set to store all words for quick lookup
    var wordSet = new HashSet<string>(words);
    var pairs = new List<string>();
    var used = new HashSet<string>();

    // Check each word
    foreach (var word in words)
    {
        // Skip if we already used this word in a pair
        if (used.Contains(word))
            continue;

        // Skip words with same letters (like "aa")
        if (word[0] == word[1])
            continue;

        // Create the reverse of the word
        string reverse = "" + word[1] + word[0];

        // If the reverse exists and we haven't used it yet
        if (wordSet.Contains(reverse) && !used.Contains(reverse))
        {
            pairs.Add($"{word} & {reverse}");
            used.Add(word);
            used.Add(reverse);
        }
    }

    return pairs.ToArray();
}

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    
    public static Dictionary<string, int> SummarizeDegrees(string filename)
{
    var degrees = new Dictionary<string, int>();
    foreach (var line in File.ReadLines(filename))
    {
        var fields = line.Split(",");
        
        // Get the degree from column 4 (index 3 since we start counting at 0)
        if (fields.Length > 3)
        {
            string degree = fields[3].Trim(); // Remove any extra spaces
            
            // If this degree is already in our dictionary, add 1 to its count
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                // If it's a new degree, start counting at 1
                degrees[degree] = 1;
            }
        }
    }

    return degrees;
}

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
{
    // Remove spaces and make everything lowercase
    word1 = word1.Replace(" ", "").ToLower();
    word2 = word2.Replace(" ", "").ToLower();
    
    // If different lengths, they can't be anagrams
    if (word1.Length != word2.Length)
        return false;
    
    // Count letters in first word
    var letterCount = new Dictionary<char, int>();
    
    foreach (char letter in word1)
    {
        if (letterCount.ContainsKey(letter))
            letterCount[letter]++;
        else
            letterCount[letter] = 1;
    }
    
    // Subtract letters from second word
    foreach (char letter in word2)
    {
        if (!letterCount.ContainsKey(letter))
            return false; // Letter not in first word
            
        letterCount[letter]--;
        
        if (letterCount[letter] < 0)
            return false; // Too many of this letter
    }
    
    // Check if all counts are zero
    foreach (var count in letterCount.Values)
    {
        if (count != 0)
            return false;
    }
    
    return true;
}
    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
    using var client = new HttpClient();
    using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
    using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
    using var reader = new StreamReader(jsonStream);
    var json = reader.ReadToEnd();
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

    // Create a list to store our formatted earthquake strings
    var earthquakeList = new List<string>();
    
    // Go through each earthquake
    foreach (var feature in featureCollection.Features)
    {
        // Get the place and magnitude
        string place = feature.Properties.Place;
        double magnitude = feature.Properties.Mag;
        
        // Create the formatted string
        string earthquakeInfo = $"{place} - Mag {magnitude}";
        earthquakeList.Add(earthquakeInfo);
    }
    
    return earthquakeList.ToArray();
        
    }
}
using System;
using System.IO;
using System.Collections.Generic;

public class Ini
{
    String File { get; }
    public Dictionary<String, Dictionary<String, String>> Sections { get; private set; }
    public int SectionsLength { get; private set; }
    public int PropertiesLength { get; private set; }
    public Ini(string file)
    {
        File = file;
        Sections = new Dictionary<String, Dictionary<String, String>>();
        Sections = Load(file);
    }
    /// <summary>
    /// Gives back a Dictionary&lt;String, Dictionary&lt;String, String&gt;&gt;.
    /// </summary>
    /// <param name="file"></param>
    public Dictionary<String, Dictionary<String, String>> Load(String file)
    {
        try
        {
            StreamReader sr = new StreamReader(file);
            Dictionary<String, String> innerMap = new Dictionary<String, String>();

            String line = sr.ReadLine();

            while (line != null)
            {
                if (line.IndexOf("[") == 0 && line.IndexOf("]") == line.Length - 1)   //Sections!
                {
                    innerMap = new Dictionary<String, String>();
                    Sections.Add(line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - 1), innerMap);
                    SectionsLength++;
                }
                else    //Properties!
                {
                    if (line.Length > 1 && line.Contains("="))
                    {
                        String[] splittedLine = line.Split("=");
                        innerMap.Add(splittedLine[0], splittedLine[1]);
                        PropertiesLength++;
                    }
                }
                line = sr.ReadLine();
            }

            return Sections;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
        return null;
    }
}
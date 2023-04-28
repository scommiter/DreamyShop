// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

string input = "Manufacturer";
Regex regex = new Regex("[^A-Z]");
string output = regex.Replace(input, "");
Console.WriteLine(output);

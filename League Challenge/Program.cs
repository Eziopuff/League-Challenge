using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class LeagueRanking
{
    // A class to represent a team
    public class Team
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public Team(string name)
        {
            Name = name;
            Points = 0;
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Please input file path");

        string filePath = Console.ReadLine().ToString();

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: The file '{filePath}' does not exist.");
            return;
        }

        Console.Clear();
        Console.WriteLine("Results");
        Console.WriteLine("-------------------------------------");

        // Read all lines from the file
        var teamResults = File.ReadAllLines(filePath);

        var teams = new Dictionary<string, Team>();

        foreach (var result in teamResults)
        {
            // Parse the input string to extract the teams and their scores
            var parts = result.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var team1 = parts[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var team2 = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string team1Name = "";
            string team2Name = "";

            for (int i = 0; i < team1.Length-1; i++)
            {
                if (i == 0) { team1Name = team1[i]; } else { team1Name += " " + team1[i]; }
            }
             int team1Score = int.Parse(team1[team1.Count()-1]);

            for (int i = 0; i < team2.Length - 1; i++)
            {
                if (i == 0) { team2Name = team2[i]; } else { team2Name += " " + team2[i]; }
            }
            int team2Score = int.Parse(team2[team2.Count() - 1]);

            // Add teams to dictionary if they don't exist yet
            if (!teams.ContainsKey(team1Name))
                teams[team1Name] = new Team(team1Name);
            if (!teams.ContainsKey(team2Name))
                teams[team2Name] = new Team(team2Name);

            // Calculate points for each team based on the score
            if (team1Score > team2Score)
            {
                teams[team1Name].Points += 3; // team1 wins
            }
            else if (team2Score > team1Score)
            {
                teams[team2Name].Points += 3; // team2 wins
            }
            else
            {
                teams[team1Name].Points += 1; // draw
                teams[team2Name].Points += 1; // draw
            }
        }

        // Sort the teams first by points (descending) and then alphabetically by name
        var sortedTeams = teams.Values
            .OrderByDescending(t => t.Points)
            .ThenBy(t => t.Name)
            .ToList();

        // Output the ranking
        int rank = 1;
        for (int i = 0; i < sortedTeams.Count; i++)
        {
            var team = sortedTeams[i];
            if (i > 0 && team.Points < sortedTeams[i - 1].Points)
            {
                rank = i + 1; // Increment rank when points change
            }

            // Print team and their rank
            string ptSuffix = team.Points == 1 ? "pt" : "pts";
            Console.WriteLine($"{rank}. {team.Name}, {team.Points} {ptSuffix}");
        }

        Console.ReadLine();
    }
}
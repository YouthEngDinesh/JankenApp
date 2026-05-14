using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace Task4_JankenApp.Pages
{
    public enum Hand
    {
        Rock,        //0
        Scissors,    //1
        Paper        //2
    }
    public class IndexModel : PageModel
    {// 1. Automatically maps the selected HTML input value to this enum property
        [BindProperty]
        public Hand UserHand { get; set; }

        // Properties to store game state and pass it back to the HTML View
        public Hand? CpuHand { get; set; }           //(?): Making the Enum Nullable to handle the initial state before the game starts
                                                     //When the application first executes OnGet(), CpuHand is initialized as completely empty(null) [Razor Pages].
                                                     //the question mark transforms it into a Nullable Enum (Hand?):
        public string GameResult { get; set; }
        public string AlertClass { get; set; }

        public void OnGet()
        {
            // Initial page load setup (game hasn't started yet)
        }

        // 2. Triggered instantly when any player hand button is pressed
        public void OnPost()
        {
            // Generate a secure random choice for the CPU (0, 1, or 2)
            var random = new Random();               // Generates 0, 1, or 2    ...returns a standard integer data type (e.g., 2)      
            CpuHand = (Hand)random.Next(0, 3);       // Explicit Casting / Enumeration mapping
                                                     //If the number is 0 --->  it becomes Hand.Rock
                                                     //If the number is 1 --->  it becomes Hand.Scissors
                                                     //If the number is 2 --->  it becomes Hand.Paper

            // Run the Janken evaluation logic
            EvaluateGame();
        }

        private void EvaluateGame()
        {
            if (UserHand == CpuHand)
            {
                GameResult = "👔 It's a Tie! (Draw)_引き分け（あいこ）です。";
                AlertClass = "warning"; // Yellow alert box
            }
            else if ((UserHand == Hand.Rock && CpuHand == Hand.Scissors) ||
                     (UserHand == Hand.Scissors && CpuHand == Hand.Paper) ||
                     (UserHand == Hand.Paper && CpuHand == Hand.Rock))
            {
                GameResult = "🎉🎉🎉 Congratulations! You Win!_あなたの【勝ち】です！おめでとう！";
                AlertClass = "success"; // Green alert box
            }
            else
            {
                GameResult = "😢 Outplayed! You Lose._あなたの【負け】です。残念…。";
                AlertClass = "danger"; // Red alert box
            }
        }
    }
}

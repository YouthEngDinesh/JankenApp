using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task4_JankenApp.Pages
{
    public enum Hand
    {
        Rock,
        Scissors,
        Paper
    }
    public class IndexModel : PageModel
    {// 1. Automatically maps the selected HTML input value to this enum property
        [BindProperty]
        public Hand UserHand { get; set; }

        // Properties to store game state and pass it back to the HTML View
        public Hand? CpuHand { get; set; }
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
            var random = new Random();
            CpuHand = (Hand)random.Next(0, 3);

            // Run the Janken evaluation logic
            EvaluateGame();
        }

        private void EvaluateGame()
        {
            if (UserHand == CpuHand)
            {
                GameResult = "👔 It's a Tie! (Draw)";
                AlertClass = "warning"; // Yellow alert box
            }
            else if ((UserHand == Hand.Rock && CpuHand == Hand.Scissors) ||
                     (UserHand == Hand.Scissors && CpuHand == Hand.Paper) ||
                     (UserHand == Hand.Paper && CpuHand == Hand.Rock))
            {
                GameResult = "🎉 Congratulations! You Win!";
                AlertClass = "success"; // Green alert box
            }
            else
            {
                GameResult = "😢 Outplayed! You Lose.";
                AlertClass = "danger"; // Red alert box
            }
        }
    }
}

namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {

        private const string _MatchedTileText = " ";
        private readonly Color _MatchedTileBackgroundColor = Colors.Red;
        private readonly Color _UnmatchedTileBackgroundColor = Colors.LightBlue;

        private Button lastClicked;
        private bool findingMatch = false;
        private int matchesFound;
        private int _tenthsOfSecondsElapsed = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            SetButtonsInitialVisibility();
            InitializeAnimalButtonsText();

            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }

        private void SetButtonsInitialVisibility()
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;
        }

        private void InitializeAnimalButtonsText()
        {
            List<string> animalEmoji = [
                            "🐵", "🐵",
                "🐸", "🐸",
                "🦋", "🦋",
                "🐝", "🐝",
                "🦜", "🦜",
                "🐔", "🐔",
                "🐘", "🐘",
                "🐯", "🐯"
                        ];

            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

        private bool TimerTick()
        {
            if(!this.IsLoaded)
            {
                return false;
            }
            _tenthsOfSecondsElapsed++;

            TimeElapsed.Text = $"Time elapsed: {(_tenthsOfSecondsElapsed / 10F).ToString("0.0s")}";

            if(PlayAgainButton.IsVisible)
            {
                _tenthsOfSecondsElapsed = 0;
                return false;
            }

            return true;
               
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && !findingMatch)
                {
                    buttonClicked.BackgroundColor = _MatchedTileBackgroundColor;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(buttonClicked.Text) && (buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text))
                    {
                        matchesFound++;
                        lastClicked.Text = _MatchedTileText;
                        buttonClicked.Text = _MatchedTileText;
                    }
                    lastClicked.BackgroundColor = _UnmatchedTileBackgroundColor;
                    buttonClicked.BackgroundColor = _UnmatchedTileBackgroundColor;
                    findingMatch = false;
                }
            }

            if(matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}

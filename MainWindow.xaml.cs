using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Media;//para mudar as cores 
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }
        private void SetUpGame()
        {
            //List = armazena um conjunto de valores
            List<string> animalEmoji = new List<string>()
            {
                "🦜","🦜",
                "🦋","🦋",
                "🐛","🐛",
                "🦑","🦑",
                "🐢","🐢",
                "🐟","🐟",
                "🦖","🦖",
                "🐁","🐁",
            };
            Random random = new Random();//cria um novo gerador de números aleatorios
            foreach (TextBlock textBlock in mainGrind.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        TextBlock lastTextBlockClicked;
        bool findingMacth = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock; 
            if (findingMacth == false)  
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMacth = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility= Visibility.Hidden;
                findingMacth = false;
            }
            else
            {
                // Muda a cor de fundo dos dois blocos para vermelho (indicando erro)
                textBlock.Background = Brushes.Red;
                lastTextBlockClicked.Background = Brushes.Red;
                // Espera 0.5 segundos antes de "resetar" a cor e mostrar de novo
                DispatcherTimer errorTimer = new DispatcherTimer();
                errorTimer.Interval = TimeSpan.FromSeconds(0.5);
                errorTimer.Tick += (s, args) =>
                {
                    errorTimer.Stop();
                    // Mostra novamente os dois blocos
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                    textBlock.Visibility = Visibility.Visible;
                    // Restaura a cor original (por exemplo, branco)
                    lastTextBlockClicked.Background = Brushes.White;
                    textBlock.Background = Brushes.White;
                };
                errorTimer.Start();
                findingMacth = false;
            }
        }
        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}

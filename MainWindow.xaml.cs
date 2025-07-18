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
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
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
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

        }
    }
}

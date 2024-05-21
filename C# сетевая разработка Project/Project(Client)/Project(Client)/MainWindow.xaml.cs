using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using System.Threading;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace Project_Client_
{
    public partial class MainWindow : Window
    {
        private List<Card> cards;
        private BitmapImage backSide;
        private BitmapImage white;

        private static int port;

        private static IPAddress ipServer = IPAddress.Parse("127.0.0.1");
        private static int portServer = 8085;

        private Card strongCard;
        private int strongLear;
        private List<Card> currentCards = new List<Card>();

        private bool isFirst = false;
        private bool isTurn = false;
        private bool isMove = false;

        private List<System.Windows.Controls.Image> moves = new List<System.Windows.Controls.Image>();
        private List<System.Windows.Controls.Image> fights = new List<System.Windows.Controls.Image>();

        private int currentPlace = 0;

        private int myCount = 6;
        private int opponentCount = 6;

        private bool isMore = false;
        private List<Card> moreCards = new List<Card>();

        private bool isGame = true;
        private bool isTurnLabel = true;

        public MainWindow()
        {
            InitializeComponent();
            backSide = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Cards\\0.png"));
            white = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Cards\\White.png"));
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            button3.IsEnabled = false;

            Grid newGrid = new Grid();

            for (int i = 0; i < myCount; i++)
            {
                newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < myCount; i++)
            {
                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                newGrid.Children.Add(tImage);
                Grid.SetColumn(tImage, i);
                tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
            }
            thisGrid.Children.Add(newGrid);
            Grid.SetRow(newGrid, 2);

            myGrid = newGrid;



            Grid newGridOpponent = new Grid();

            for (int i = 0; i < opponentCount; i++)
            {
                newGridOpponent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < opponentCount; i++)
            {
                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                newGridOpponent.Children.Add(tImage);
                Grid.SetColumn(tImage, i);
            }
            thisGrid.Children.Add(newGridOpponent);
            Grid.SetRow(newGridOpponent, 0);

            opponentGrid = newGridOpponent;



            moves.Add(move1);
            moves.Add(move2);
            moves.Add(move3);
            moves.Add(move4);
            moves.Add(move5);
            moves.Add(move6);

            foreach (System.Windows.Controls.Image image in moves) image.Source = white;

            fights.Add(fight1);
            fights.Add(fight2);
            fights.Add(fight3);
            fights.Add(fight4);
            fights.Add(fight5);
            fights.Add(fight6);

            foreach (System.Windows.Controls.Image image in fights) image.Source = white;

            cards = new List<Card>()
        {
            new Card(6, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\6C.png")),
            new Card(6, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\6B.png")),
            new Card(6, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\6K.png")),
            new Card(6, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\6P.png")),

            new Card(7, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\7C.png")),
            new Card(7, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\7B.png")),
            new Card(7, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\7K.png")),
            new Card(7, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\7P.png")),

            new Card(8, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\8C.png")),
            new Card(8, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\8B.png")),
            new Card(8, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\8K.png")),
            new Card(8, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\8P.png")),

            new Card(9, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\9C.png")),
            new Card(9, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\9B.png")),
            new Card(9, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\9K.png")),
            new Card(9, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\9P.png")),

            new Card(10, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\10C.png")),
            new Card(10, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\10B.png")),
            new Card(10, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\10K.png")),
            new Card(10, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\10P.png")),

            new Card(11, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\VC.png")),
            new Card(11, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\VB.png")),
            new Card(11, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\VK.png")),
            new Card(11, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\VP.png")),

            new Card(12, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\DC.png")),
            new Card(12, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\DB.png")),
            new Card(12, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\DK.png")),
            new Card(12, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\DP.png")),

            new Card(13, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\KC.png")),
            new Card(13, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\KB.png")),
            new Card(13, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\KK.png")),
            new Card(13, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\KP.png")),

            new Card(14, 1, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\TC.png")),
            new Card(14, 2, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\TB.png")),
            new Card(14, 3, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\TK.png")),
            new Card(14, 4, new Uri(Directory.GetCurrentDirectory() + "\\Cards\\TP.png")),
        };

            ChoosePlayer startWindow = new ChoosePlayer();
            startWindow.ShowDialog();
            if (startWindow.choosed == true)
            {
                if (startWindow.is1)
                {
                    SendData("p1=true|" + Dns.GetHostAddresses(Dns.GetHostName()).First(i => i.AddressFamily.ToString() == "InterNetwork").ToString());
                    port = 8081;
                    isFirst = true;
                    isTurn = true;
                    isMove = true;
                    isTurnLabel = false;
                }
                else
                {
                    SendData("p2=true|" + Dns.GetHostAddresses(Dns.GetHostName()).First(i => i.AddressFamily.ToString() == "InterNetwork").ToString());
                    port = 8082;
                }
            }
            else { Close(); }

            ThreadPool.QueueUserWorkItem(RecieveData);
            ThreadPool.QueueUserWorkItem(Win);
            ThreadPool.QueueUserWorkItem(LabelIsTurn);
        }

        void Move()
        {
            Grid newGrid = new Grid();

            myCount--;

            for (int i = 0; i < myCount; i++)
            {
                newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            int index = 0;
            Card currentCard = null;
            if(isMove) currentCard = cards.First(i => i.Image == moves[currentPlace].Source);
            else currentCard = cards.First(i => i.Image == fights[currentPlace].Source);

            foreach (System.Windows.Controls.Image image in myGrid.Children)
            {
                if (image.Source == currentCard.Image) continue;

                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = image.Source;
                newGrid.Children.Add(tImage);
                Grid.SetColumn(tImage, index);
                tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                index++;
            }
            thisGrid.Children.Add(newGrid);
            Grid.SetRow(newGrid, 2);

            myGrid.Children.Clear();
            myGrid = newGrid;
        }

        void MoveMore(Card currentCard)
        {
            Grid newGrid = new Grid();

            myCount--;

            for (int i = 0; i < myCount; i++)
            {
                newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            int index = 0;
            moreCards.Add(currentCard);
            moves[moves.Count(i => i.Source != white)].Source = currentCard.Image;

            foreach (System.Windows.Controls.Image image in myGrid.Children)
            {
                if (image.Source == currentCard.Image) continue;

                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = image.Source;
                newGrid.Children.Add(tImage);
                Grid.SetColumn(tImage, index);
                tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                index++;
            }
            thisGrid.Children.Add(newGrid);
            Grid.SetRow(newGrid, 2);

            myGrid.Children.Clear();
            myGrid = newGrid;
        }

        void OpponentMove()
        {
            Grid newGridOpponent = new Grid();

            opponentCount--;

            for (int i = 0; i < opponentCount; i++)
            {
                newGridOpponent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < opponentCount; i++)
            {
                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = backSide;
                newGridOpponent.Children.Add(tImage);
                Grid.SetColumn(tImage, i);
            }
            thisGrid.Children.Add(newGridOpponent);
            Grid.SetRow(newGridOpponent, 0);

            opponentGrid.Children.Clear();
            opponentGrid = newGridOpponent;
        }

        int GetCards()
        {
            int need = 6 - myGrid.Children.Count;
            if (need > 0)
            {
                if (currentCards.Count < need) need = currentCards.Count;

                Grid newGrid = new Grid();

                for (int i = 0; i < need + myGrid.Children.Count; i++)
                {
                    newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }

                int index = 0;

                foreach (System.Windows.Controls.Image image in myGrid.Children)
                {
                    System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                    tImage.Source = image.Source;
                    newGrid.Children.Add(tImage);
                    Grid.SetColumn(tImage, index);
                    tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                    index++;
                }
                for (int i = 0; i < need; i++)
                {
                    System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                    tImage.Source = currentCards[i].Image;
                    newGrid.Children.Add(tImage);
                    Grid.SetColumn(tImage, index);
                    tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                    index++;
                }

                thisGrid.Children.Add(newGrid);
                Grid.SetRow(newGrid, 2);

                myGrid.Children.Clear();
                myGrid = newGrid;

                myCount += need;
                return need;
            }
            return 0;
        }

        void OpponentGetCards(int cardsCount)
        {
            Grid newGridOpponent = new Grid();

            opponentCount += cardsCount;

            for (int i = 0; i < opponentCount; i++)
            {
                newGridOpponent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < opponentCount; i++)
            {
                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = backSide;
                newGridOpponent.Children.Add(tImage);
                Grid.SetColumn(tImage, i);
            }
            thisGrid.Children.Add(newGridOpponent);
            Grid.SetRow(newGridOpponent, 0);

            opponentGrid.Children.Clear();
            opponentGrid = newGridOpponent;
        }

        int TakeCards()
        {
            Grid newGrid = new Grid();

            for (int i = 0; i < myGrid.Children.Count + moves.Where(j => j.Source != white).Count() + fights.Where(j => j.Source != white).Count(); i++)
            {
                newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            int index = 0;

            foreach (System.Windows.Controls.Image image in myGrid.Children)
            {
                if (image.Source == white) continue;

                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = image.Source;
                newGrid.Children.Add(tImage);
                Grid.SetColumn(tImage, index);
                tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                index++;
            }
            foreach (System.Windows.Controls.Image image in moves.Concat<System.Windows.Controls.Image>(fights))
            {
                if (image.Source == white) continue;
                
                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = image.Source;
                newGrid.Children.Add(tImage);
                Grid.SetColumn(tImage, index);
                tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                index++;
            }

            thisGrid.Children.Add(newGrid);
            Grid.SetRow(newGrid, 2);

            myGrid.Children.Clear();
            myGrid = newGrid;

            int newCount = index - myCount;
            myCount = index;
            return newCount;
        }

        void Win(object state)
        {
            while (true)
            {
                if (myCount == 0 && currentCards.Count == 0)
                {
                    Thread.Sleep(50);
                    SendData("win|" + isFirst.ToString());
                    break;
                }
            }
        }

        void LabelIsTurn(object state)
        {
            while (isGame)
            {
                if (isTurn != isTurnLabel)
                {
                    isTurnLabel = isTurn;

                    if (isTurnLabel)
                    {
                        Dispatcher.Invoke(new Action(() => turnLabel.Content = "Your turn"));
                        Dispatcher.Invoke(new Action(() => turnLabel.Foreground = Brushes.Green));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => turnLabel.Content = "Opponent's turn"));
                        Dispatcher.Invoke(new Action(() => turnLabel.Foreground = Brushes.Red));
                    }
                }
            }
        }

        void SortCards()
        {
            List<Card> tempCards = new List<Card>();
            foreach (System.Windows.Controls.Image tImage in myGrid.Children)
            {
                tempCards.Add(cards.First(i => i.Image == tImage.Source));
            }
            tempCards = tempCards.OrderBy(i => i.Lear * 10 + i.Value).ToList();

            Grid newGrid = new Grid();

            for (int i = 0; i < myCount; i++)
            {
                newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            int index = 0;

            foreach (System.Windows.Controls.Image image in myGrid.Children)
            {
                System.Windows.Controls.Image tImage = new System.Windows.Controls.Image();
                tImage.Source = tempCards[index].Image;
                newGrid.Children.Add(tImage);
                Grid.SetColumn(tImage, index);
                tImage.PreviewMouseLeftButtonDown += Me_PreviewMouseLeftButtonDown;
                index++;
            }
            thisGrid.Children.Add(newGrid);
            Grid.SetRow(newGrid, 2);

            myGrid.Children.Clear();
            myGrid = newGrid;
        }

        void RecieveData(object state)
        {
            while (true)
            {
                UdpClient client = new UdpClient(port);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> strings = text.Split('|').ToList();

                if (strings[0] == "deck")
                {
                    strongCard = cards.First(i => i.Value.ToString() == strings[1].Split('&')[0] && i.Lear.ToString() == strings[1].Split('&')[1]);
                    strongLear = int.Parse(strings[1].Split('&')[1]);

                    for (int j = 2; j < strings.Count; j++)
                    {
                        currentCards.Add(cards.First(i => i.Value.ToString() == strings[j].Split('&')[0] && i.Lear.ToString() == strings[j].Split('&')[1]));
                    }

                    List<Card> tempCards = new List<Card>();
                    if (isFirst)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            tempCards.Add(currentCards[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            tempCards.Add(currentCards[i + 6]);
                        }
                    }

                    tempCards = tempCards.OrderBy(i => i.Lear * 10 + i.Value).ToList();
                    for (int i = 0; i < 6; i++)
                    {
                        Dispatcher.Invoke(new Action(() => (myGrid.Children[i] as System.Windows.Controls.Image).Source = tempCards[i].Image));
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        Dispatcher.Invoke(new Action(() => (opponentGrid.Children[i] as System.Windows.Controls.Image).Source = backSide));
                    }

                    Dispatcher.Invoke(new Action(() => TrumpCard.Source = strongCard.Image));

                    currentCards = currentCards.Skip(12).ToList();
                    currentCards.Add(strongCard);

                    Dispatcher.Invoke(new Action(() => cardsCount.Text = "Count of cards: " + currentCards.Count));
                }

                else if (strings[0] == "move")
                {
                    Dispatcher.Invoke(new Action(() => moves[currentPlace].Source = cards.First(i => i.Value.ToString() == strings[2] && i.Lear.ToString() == strings[3]).Image));
                    
                    if (isTurn)
                    {
                        Dispatcher.Invoke(new Action(() => Move()));
                        Dispatcher.Invoke(new Action(() => button1.IsEnabled = false));
                        Dispatcher.Invoke(new Action(() => button2.IsEnabled = false));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => OpponentMove()));
                        Dispatcher.Invoke(new Action(() => button2.IsEnabled = true));
                    }

                    isTurn = !isTurn;
                }
                
                else if (strings[0] == "fight")
                {
                    Dispatcher.Invoke(new Action(() => fights[currentPlace].Source = cards.First(i => i.Value.ToString() == strings[2] && i.Lear.ToString() == strings[3]).Image));

                    if (isTurn)
                    {
                        Dispatcher.Invoke(new Action(() => Move()));
                        Dispatcher.Invoke(new Action(() => button1.IsEnabled = false));
                        Dispatcher.Invoke(new Action(() => button2.IsEnabled = false));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => OpponentMove()));
                        Dispatcher.Invoke(new Action(() => button1.IsEnabled = true));
                    }

                    isTurn = !isTurn;
                    currentPlace++;
                }

                else if (strings[0] == "finish")
                {
                    foreach (var image in moves) Dispatcher.Invoke(new Action(() => image.Source = white));
                    foreach (var image in fights) Dispatcher.Invoke(new Action(() => image.Source = white));

                    currentPlace = 0;

                    isTurn = !isTurn;
                    isMove = !isMove;

                    currentCards = currentCards.Skip(int.Parse(strings[1])).ToList();
                    Dispatcher.Invoke(new Action(() => cardsCount.Text = "Count of cards: " + currentCards.Count));

                    int cCount = 0;
                    if (isTurn)
                    {
                        Dispatcher.Invoke(new Action(() => OpponentGetCards(int.Parse(strings[1]))));
                        Dispatcher.Invoke(new Action(() => cCount = GetCards()));
                    }
                    else
                    {
                        cCount = 6 - opponentCount;
                        if (currentCards.Count < cCount)
                        {
                            cCount = currentCards.Count;
                        }
                        if (cCount < 0) cCount = 0;
                    }
                    currentCards = currentCards.Skip(cCount).ToList();
                    Dispatcher.Invoke(new Action(() => cardsCount.Text = "Count of cards: " + currentCards.Count));
                    if (!isTurn)
                    {
                        Dispatcher.Invoke(new Action(() => OpponentGetCards(cCount)));
                    }

                    Dispatcher.Invoke(new Action(() => SortCards()));
                }

                else if (strings[0] == "willtake")
                {
                    isTurn = !isTurn;

                    if (isTurn)
                    {
                        isMore = true;
                        Dispatcher.Invoke(new Action(() => button3.IsEnabled = true));
                    }
                }

                else if (strings[0] == "more")
                {
                    if (!isTurn)
                    {
                        Dispatcher.Invoke(new Action(() => button3.IsEnabled = true));
                        for (int i = 1; i < strings.Count; i++)
                        {
                            string str = strings[i];
                            Card tCard = cards.First(j => j.Value.ToString() == str.Split('&')[0] && j.Lear.ToString() == str.Split('&')[1]);
                            Dispatcher.Invoke(new Action(() => moves[moves.Count(j => j.Source != white)].Source = tCard.Image));
                        }
                        Dispatcher.Invoke(new Action(() => OpponentGetCards(-strings.Count + 1)));
                    }
                    isTurn = !isTurn;
                }

                else if (strings[0] == "take")
                {
                    foreach (var image in moves) Dispatcher.Invoke(new Action(() => image.Source = white));
                    foreach (var image in fights) Dispatcher.Invoke(new Action(() => image.Source = white));

                    currentPlace = 0;

                    if (!isTurn)
                    {
                        Dispatcher.Invoke(new Action(() => OpponentGetCards(int.Parse(strings[1]))));
                        Dispatcher.Invoke(new Action(() => opponentCount = opponentGrid.Children.Count));
                        int cCount = 0;
                        Dispatcher.Invoke(new Action(() => cCount = GetCards()));
                        currentCards = currentCards.Skip(cCount).ToList();
                    }
                    else
                    {
                        int t = 6 - opponentCount;
                        if (t > currentCards.Count) t = currentCards.Count;
                        if (t < 0) t = 0;
                        Dispatcher.Invoke(new Action(() => OpponentGetCards(t)));
                        Dispatcher.Invoke(new Action(() => opponentCount = opponentGrid.Children.Count));
                        currentCards = currentCards.Skip(t).ToList();
                    }
                    Dispatcher.Invoke(new Action(() => cardsCount.Text = "Count of cards: " + currentCards.Count));

                    isTurn = !isTurn;

                    Dispatcher.Invoke(new Action(() => SortCards()));
                }

                else if (strings[0] == "win")
                {
                    Dispatcher.Invoke(new Action(() => button1.IsEnabled = false));
                    Dispatcher.Invoke(new Action(() => button2.IsEnabled = false));

                    isGame = false;
                    isTurn = false;

                    if (isFirst.ToString() == strings[1])
                    {
                        Dispatcher.Invoke(new Action(() => turnLabel.Content = "You Win!"));
                        Dispatcher.Invoke(new Action(() => turnLabel.Foreground = Brushes.Green));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => turnLabel.Content = "You Lose!"));
                        Dispatcher.Invoke(new Action(() => turnLabel.Foreground = Brushes.Red));
                    }
                    Dispatcher.Invoke(new Action(() => turnLabel.FontSize = 15));
                }

                client.Close();
            }
        }

        void SendData(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            UdpClient client = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(ipServer, portServer);
            client.Send(bytes, bytes.Length, ipEnd);
            client.Close();
        }

        private void Me_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Card thisCard = cards.First(i => i.Image == ((System.Windows.Controls.Image)sender).Source);

            if (!isMore)
            {
                if (isTurn)
                {
                    if (isMove && currentPlace < 6)
                    {
                        if (moves.Count(i => i.Source == white) == 6 || cards.Where(i => moves.Select(j => j.Source).Contains(i.Image) || fights.Select(j => j.Source).Contains(i.Image)).ToList().Select(i => i.Value).Contains(thisCard.Value))
                        {
                            SendData("move|" + isFirst.ToString() + "|" + thisCard.Value + "|" + thisCard.Lear);
                        }
                    }
                    else if (currentPlace < 6)
                    {
                        Card enemyCard = cards.First(i => i.Image == moves[currentPlace].Source);

                        if (enemyCard.Lear == thisCard.Lear && thisCard.Value > enemyCard.Value || thisCard.Lear == strongLear && enemyCard.Lear != strongLear)
                        {
                            SendData("fight|" + isFirst.ToString() + "|" + thisCard.Value + "|" + thisCard.Lear);
                        }
                    }
                }
            }
            else
            {
                int empty = moves.Count(i => i.Source == white);
                if (fights.Count(i => i.Source == white) > opponentCount)
                {
                    empty -= fights.Count(i => i.Source == white) - opponentCount;
                }
                if (empty > 0 && cards.Where(i => moves.Select(j => j.Source).Contains(i.Image) || fights.Select(j => j.Source).Contains(i.Image)).ToList().Select(i => i.Value).Contains(thisCard.Value))
                {
                    MoveMore(thisCard);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int cCount = GetCards();
            SendData("finish|" + cCount);
            button1.IsEnabled = false;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SendData("willtake");
            button2.IsEnabled = false;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (isMove)
            {
                isMore = false;
                button3.IsEnabled = false;
                string text = "more";

                foreach (var card in moreCards)
                {
                    text += "|" + card.Value + "&" + card.Lear;
                }
                moreCards.Clear();

                SendData(text);
            }
            else
            {
                int cCount = TakeCards();
                SendData("take|" + cCount);
                button3.IsEnabled = false;
            }
        }
    }

    class Card
    {
        public int Value { get; private set; }
        public int Lear { get; private set; }
        public BitmapImage Image { get; private set; }
        public Card(int value, int lear, Uri image)
        {
            Value = value;
            Lear = lear;
            Image = new BitmapImage(image);
        }
    }
}
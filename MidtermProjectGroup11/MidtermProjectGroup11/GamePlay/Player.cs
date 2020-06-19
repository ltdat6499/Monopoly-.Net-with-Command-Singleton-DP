using MidtermProjectGroup11.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermProjectGroup11.GamePlay
{
    public class Player
    {

        #region Properties

        private string _name = "";
        private int _money = 1000;
        private int _prisonBreak = 0;
        private int _prisonTimeCount = 0;
        private bool _isALive = true;
        private int _rollCounter = 0;
        private int _currentPosition = 1;
        private int _dice1, _dice2;
        private PictureBox _playerToken;
        
        #endregion

        #region Getter Setter
        public string Name { get => _name; set => _name = value; }
        public int Money { get => _money; set => _money = value; }
        public int PrisonBreak { get => _prisonBreak; set => _prisonBreak = value; }
        public bool IsALive { get => _isALive; set => _isALive = value; }
        public int RollCounter { get => _rollCounter; set => _rollCounter = value; }
        public int CurrentPosition { get => _currentPosition; set => _currentPosition = value; }
        public PictureBox PlayerToken { get => _playerToken; }
        public int Dice1 { get => _dice1; }
        public int Dice2 { get => _dice2; }
        public int PrisonTimeCount { get => _prisonTimeCount; set => _prisonTimeCount = value; }
        #endregion

        #region Constructor
        public Player(string name, PictureBox playerToken)
        {
            _name = name;
            _playerToken = playerToken;
        }
        #endregion

        #region Method
        public Tuple<int, int> Roll()
        {
            _dice1 = RandomTool.Instance.Rand(1, 13);
            _dice2 = RandomTool.Instance.Rand(1, 13);
            if (_dice1 == _dice2)
            {
                RollCounter++;
            }
            else if (RollCounter >= 3)
            {
                RollCounter = 0;
                return null;
            }
            else
            {
                RollCounter = 0;
            }
            return new Tuple<int, int>(_dice1, _dice2);
        }

        public int Move()
        {
            int dice1 = _dice1;
            int dice2 = _dice2;

            if (_currentPosition == 41)
            {
                if (dice1 == dice2)
                {
                    _prisonTimeCount = 0;
                    Transport(11);
                    return 11;
                }
                else if (_prisonTimeCount >= 3)
                {
                    _prisonTimeCount = 0;
                    Transport(11);
                    return 11;
                }
                else
                {
                    PrisonTimeCount++;
                    return 0;
                }
            }

            if (_currentPosition + dice1 + dice2 > 40)
            {
                int passTheGoPiece = 40 - _currentPosition;
                Transport(dice1 + dice2 - passTheGoPiece);
                if (_currentPosition > 1)
                {
                    GetSalaryToPassTheGo();
                    return _currentPosition;
                }
                else
                {
                    return _currentPosition;
                }
            }
            else
            {
                Transport(_currentPosition + dice1 + dice2);
                if (_currentPosition == 31)
                {
                    Transport(41);
                }
                return _currentPosition;
            }
        }

        public void Move(int destinationPosition)
        {
            CurrentPosition = destinationPosition;
        }

        private void GetSalaryToPassTheGo()
        {
            
            _money += 200;
        }
        public void PayForFree()
        {
            _money -= 500;
            Transport(11);
        }
        public void GetFree()
        {
            _prisonBreak = 0;
            Transport(11);
        }
        private void Transport(int position)
        {
            _currentPosition = position;
        }

        public bool YesNoQuestion(string message, Bitmap image)
        {
            bool result = false;
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 600;
            prompt.Text = message;
            prompt.MaximizeBox = prompt.MinimizeBox = false;
            prompt.AutoSize = false;
            prompt.Icon = null;
            prompt.ShowIcon = false;
            prompt.FormBorderStyle = FormBorderStyle.Fixed3D;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Size = new Size(500, 500);
            panel.BackgroundImage = image;
            panel.BackgroundImageLayout = ImageLayout.Zoom;

            Button denition = new Button() { Text = "No", Left = 350, Width = 100, Top = 500, Height = 50 };
            Button confirmation = new Button() { Text = "Yes", Left = 50, Width = 100, Top = 500, Height = 50 };
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = denition;

            confirmation.Click += (sender, e) => {
                result = true;
                prompt.Close();
            };
            denition.Click += (sender, e) => {
                result = false;
                prompt.Close();
            };

            prompt.Controls.Add(panel);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(denition);
            prompt.ShowDialog();

            return result;
        }
        public void YesNoQuestionChance(bool isChance, int index)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 600;
            prompt.MaximizeBox = prompt.MinimizeBox = false;
            prompt.AutoSize = false;
            prompt.Icon = null;
            prompt.ShowIcon = false;
            prompt.FormBorderStyle = FormBorderStyle.Fixed3D;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Size = new Size(500, 500);
            if (isChance)
            {
                panel.BackgroundImage = new Bitmap(Path.Combine(Application.StartupPath, @"Images", "Monopoly", "Chances", index + ".png"));
            }
            else
            {
                panel.BackgroundImage = new Bitmap(Path.Combine(Application.StartupPath, @"Images", "Monopoly", "Chest", index + ".png"));
            }
            panel.BackgroundImageLayout = ImageLayout.Zoom;

            Button confirmation = new Button() { Text = "OK", Top = 500, Height = 50 };
            confirmation.Dock = DockStyle.Bottom;
            prompt.AcceptButton = confirmation;

            confirmation.Click += (sender, e) => {
                prompt.Close();
            };

            prompt.Controls.Add(panel);
            prompt.Controls.Add(confirmation);
            prompt.ShowDialog();
        }
        public void AttentionForSomethingElse(string fileName)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 600;
            prompt.MaximizeBox = prompt.MinimizeBox = false;
            prompt.AutoSize = false;
            prompt.Icon = null;
            prompt.ShowIcon = false;
            prompt.FormBorderStyle = FormBorderStyle.Fixed3D;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Size = new Size(500, 500);
            panel.BackgroundImage = new Bitmap(Path.Combine(Application.StartupPath, fileName));
            panel.BackgroundImageLayout = ImageLayout.Zoom;

            Button confirmation = new Button() { Text = "OK", Top = 500, Height = 50};
            confirmation.Dock = DockStyle.Bottom;
            prompt.AcceptButton = confirmation;

            confirmation.Click += (sender, e) => {
                prompt.Close();
            };

            prompt.Controls.Add(panel);
            prompt.Controls.Add(confirmation);
            prompt.ShowDialog();
        }

        public void EndYesYesQuestion()
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 600;
            prompt.Text = "🎆🎆BOOOOOOOOOOOOM🎆🎆 Good Bye :))";
            prompt.MaximizeBox = prompt.MinimizeBox = false;
            prompt.AutoSize = false;
            prompt.Icon = null;
            prompt.ShowIcon = false;
            prompt.FormBorderStyle = FormBorderStyle.Fixed3D;
            prompt.StartPosition = FormStartPosition.CenterScreen;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Size = new Size(500, 500);
            panel.BackgroundImage = new Bitmap(Path.Combine(Application.StartupPath, @"Images", "Extra", "dead.png"));
            panel.BackgroundImageLayout = ImageLayout.Zoom;

            Button denition = new Button() { Text = "YESSS", Left = 350, Width = 100, Top = 500, Height = 50 };
            Button confirmation = new Button() { Text = "yess", Left = 50, Width = 100, Top = 500, Height = 50 };
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = denition;

            confirmation.Click += (sender, e) => {
                prompt.Close();
            };
            denition.Click += (sender, e) => {
                prompt.Close();
            };

            prompt.Controls.Add(panel);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(denition);
            prompt.ShowDialog();
        }
        #endregion
    }
}

using MidtermProjectGroup11.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidtermProjectGroup11.GamePlay;
using MidtermProjectGroup11.Enum;

namespace MidtermProjectGroup11.Command
{
    public class GamePlayCommand : ICommand
    {
        private readonly GamePlayControll _gamePlay;
        private readonly GamePlayAction _playerAction;
        private readonly int _position;

        public GamePlayCommand(GamePlayControll gamePlay, GamePlayAction playerAction)
        {
            _gamePlay = gamePlay;
            _playerAction = playerAction;
        }
        public GamePlayCommand(GamePlayControll gamePlay, GamePlayAction playerAction, int position)
        {
            _gamePlay = gamePlay;
            _playerAction = playerAction;
            _position = position;
        }
        public void ExecuteAction()
        {
            if (_playerAction == GamePlayAction.MoveForMainPlayer)
            {
                _gamePlay.MoveForMainPlayer();
            }
            else if (_playerAction == GamePlayAction.SetCurrentPlayer)
            {
                _gamePlay.SetCurrentPlayer();
            }
            else if (_playerAction == GamePlayAction.SetListPiece)
            {
                _gamePlay.SetListPiece();
            }
            else if (_playerAction == GamePlayAction.SetListPlayer)
            {
                _gamePlay.SetListPlayer();
            }
            else
            {
                _gamePlay.IsEndGame();
            }
        }
    }
}

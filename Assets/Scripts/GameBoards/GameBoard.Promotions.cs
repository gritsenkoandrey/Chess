using OnlineChess.Enums;
using OnlineChess.Figures;
using OnlineChess.Utils;

namespace OnlineChess.GameBoards
{
    public partial class GameBoard
    {
        private void InstantiatePromotions()
        {
            _promotions[FigureType.WhiteQueen] = InstantiateFigures(FigureType.WhiteQueen, 2, 9);
            _promotions[FigureType.WhiteRock] = InstantiateFigures(FigureType.WhiteRock, 3, 9);
            _promotions[FigureType.WhiteBishop] = InstantiateFigures(FigureType.WhiteBishop, 4, 9);
            _promotions[FigureType.WhiteKnight] = InstantiateFigures(FigureType.WhiteKnight, 5, 9);

            _promotions[FigureType.BlackQueen] = InstantiateFigures(FigureType.BlackQueen, 2, -2);
            _promotions[FigureType.BlackRock] = InstantiateFigures(FigureType.BlackRock, 3, -2);
            _promotions[FigureType.BlackBishop] = InstantiateFigures(FigureType.BlackBishop, 4, -2);
            _promotions[FigureType.BlackKnight] = InstantiateFigures(FigureType.BlackKnight, 5, -2);
            
            ShowPromotionsFigures();
        }
        
        private void ShowPromotionsFigures(FigureType type = FigureType.None)
        {
            foreach (IFigure figures in _promotions.Values)
            {
                figures.SetActive(false);
                figures.SetLayer(Layers.Promotions);
            }

            if (type == FigureType.WhitePawn)
            {
                _promotions[FigureType.WhiteQueen].SetActive(true);
                _promotions[FigureType.WhiteRock].SetActive(true);
                _promotions[FigureType.WhiteBishop].SetActive(true);
                _promotions[FigureType.WhiteKnight].SetActive(true);
            }
            else if (type == FigureType.BlackPawn)
            {
                _promotions[FigureType.BlackQueen].SetActive(true);
                _promotions[FigureType.BlackRock].SetActive(true);
                _promotions[FigureType.BlackBishop].SetActive(true);
                _promotions[FigureType.BlackKnight].SetActive(true);
            }
        }
    }
}
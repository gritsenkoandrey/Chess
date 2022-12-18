using OnlineChess.Scripts.Enums;

namespace OnlineChess.Scripts.DragDrop
{
    public interface IDragAndDrop
    {
        public void Action();
        public void ChangeState(State state);
    }
}
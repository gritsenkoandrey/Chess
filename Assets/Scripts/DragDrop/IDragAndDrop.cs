using OnlineChess.Enums;

namespace OnlineChess.DragDrop
{
    public interface IDragAndDrop
    {
        public void Action();
        public void ChangeState(State state);
    }
}
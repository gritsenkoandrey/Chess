using Enums;

namespace DragDrop
{
    public interface IDragAndDrop
    {
        public void Action();
        public void ChangeState(State state);
    }
}
using Enums;

namespace Interfaces
{
    public interface IDragAndDrop
    {
        public void Action();
        public void ChangeState(State state);
    }
}
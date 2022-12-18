using OnlineChess.Scripts.BoardProgressData;

namespace OnlineChess.Scripts.Services.PersistentProgress
{
    public interface IProgress
    {
    }

    public interface IProgressReader : IProgress
    {
        public void Read(BoardProgress progress);
    }
    
    public interface IProgressWriter : IProgress
    {
        public void Write(BoardProgress progress);
    }
}
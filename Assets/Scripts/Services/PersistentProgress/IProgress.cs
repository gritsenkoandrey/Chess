using OnlineChess.BoardProgressData;

namespace OnlineChess.Services.PersistentProgress
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
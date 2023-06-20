namespace Interface
{
    public interface IGameEndScreenController: IGameMenuInputController
    {
        void OpenResultScreen(bool isWin);
    }
}
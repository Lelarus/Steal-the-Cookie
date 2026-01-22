namespace Game.Code.Infrastructure.SM
{
    public interface IState
    {
        void Enter(object param = null);
        void Update();
        void Exit(object param = null);
    }
}
namespace Assets.Scripts.GameSystems
{
    public interface ICheckpointable
    {
        void SaveCheckpoint();
        void RestoreCheckpoint();
    }
}
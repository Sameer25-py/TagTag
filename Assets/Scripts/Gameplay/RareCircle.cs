namespace Gameplay
{
    public class RareCircle : IInteractable
    {
        public bool Apply(Brain brain)
        {
            if (!brain) return false; 
            if (!brain.TryGetComponent(out InfectedCollider _)) return false;
            if (!brain.TryGetComponent(out Character character)) return false;
            character.UnInfectCharacter();
            BrainManager.InfectRandomBrain(brain);
            return true;
        }
    }
}
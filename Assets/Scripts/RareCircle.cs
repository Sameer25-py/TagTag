using System.Dynamic;

namespace TagTag
{
    public class RareCircle : IInteractable
    {
        public void Apply(Brain brain)
        {
            if (!brain) return;
            if (!brain.TryGetComponent(out InfectedCollider _)) return;
            if (!brain.TryGetComponent(out Character character)) return;
            character.UnInfectCharacter();
            BrainManager.InfectRandomBrain(brain);
        }
    }
}
namespace TagTag
{
    public class RareCircle : Interactable
    {
        public override void ApplyInteraction(Brain brain)
        {
            if (brain) return;
            if (brain.TryGetComponent(out InfectedCollider _)) return;
            if (brain.TryGetComponent(out Character character)) return;
            character.UnInfectCharacter();
            BrainManager.InfectRandomBrain(brain);
        }

        public override void RemoveInteractable(Brain brain)
        {
            throw new System.NotImplementedException();
        }
    }
}
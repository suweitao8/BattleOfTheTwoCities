namespace GameMain
{
    public class PlayerEntityData : EntityData
    {
        public int PlayerIndex { get; set; }
        public int CharacterId { get; set; }
        
        public PlayerEntityData(int entityId, int typeId, int playerIndex, int characterId) : base(entityId, typeId)
        {
            this.PlayerIndex = playerIndex;
            this.CharacterId = characterId;
        }
    }
}
namespace GameMain
{
    public class TileBoxEntity : Entity
    {
        public TileBoxEntityData Data { get; private set; }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            // 数据
            Data = userData as TileBoxEntityData;
            
            // 应用数据
            transform.position = Data.Position;
        }
    }
}